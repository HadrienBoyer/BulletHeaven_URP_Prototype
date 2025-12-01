using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace TappyTale
{
    public class GameLoopController : MonoBehaviour
    {
        public static float RunNormalized { get; private set; }

        public GameConfig Config;
        public RunSettings RunSettings;
        public PlayerStats PlayerStats;
        public WeaponDefinition PlayerWeapon;
        public EnemyDefinition[] Enemies;

        [Header("Prefabs")]
        public GameObject PlayerPrefab;
        public GameObject EnemyPrefab;
        public GameObject ProjectilePrefab;
        public GameObject ExperienceOrbPrefab;

        [Header("Runtime")]
        public Transform Player;
        public Spawner Spawner;
        public CameraRig Cam;

        // UI
        Canvas _hud;
        Slider _hp;
        Slider _xp;
        TMP_Text _timer;

        float _timerSec;
        bool _ended;

        void Awake()
        {
            // Build minimal scene at runtime if needed (ground, camera, UI)
            EnsureGround();
            EnsureCamera();
            EnsureUI();

            // Seed RNG
            RNGService.Reseed(RunSettings.Seed + System.DateTime.Now.Minute + System.DateTime.Now.Second);

            // Player
            var p = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            p.tag = "Player";
            Player = p.transform;
            var pc = p.GetComponent<PlayerController>();
            pc.Stats = PlayerStats;
            pc.Config = Config;
            pc.Weapon = PlayerWeapon;
            pc.Shooter.ProjectilePrefab = ProjectilePrefab;
            pc.Shooter.Weapon = PlayerWeapon;

            var aoe = p.transform.Find("AoE");
            pc.AoE = aoe.GetComponent<AoEController>();

            // Spawner
            Spawner.Player = Player;
            Spawner.Enemies = Enemies;
            Spawner.RunSettings = RunSettings;

            // Reset stats
            PlayerStats.ResetRun();

            _timerSec = Config.RunDurationSeconds;
            RunNormalized = 0f;
        }

        void EnsureGround()
        {
            if (GameObject.Find("Ground") != null) return;
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.localScale = new Vector3(5, 1, 5);
            var mr = ground.GetComponent<MeshRenderer>();
            var mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = new Color(0.13f, 0.14f, 0.17f, 1f);
            mr.sharedMaterial = mat;
        }

        void EnsureCamera()
        {
            var cam = Camera.main;
            if (cam == null)
            {
                var go = new GameObject("Main Camera");
                cam = go.AddComponent<Camera>();
                go.tag = "MainCamera";
            }
            Cam = cam.gameObject.GetComponent<CameraRig>();
            if (Cam == null) Cam = cam.gameObject.AddComponent<CameraRig>();
            Cam.Offset = new Vector3(0, 18, -12);
            Cam.FollowLerp = 0.12f;
        }

        void EnsureUI()
        {
            if (FindObjectOfType<Canvas>()) return;
            var canvasGO = new GameObject("HUD");
            _hud = canvasGO.AddComponent<Canvas>();
            _hud.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            var timerGO = new GameObject("Timer");
            timerGO.transform.SetParent(canvasGO.transform);
            var txt = timerGO.AddComponent<TextMeshProUGUI>();
            txt.fontSize = 28; txt.alignment = TextAlignmentOptions.Center;
            txt.rectTransform.anchorMin = new Vector2(0.5f, 1); txt.rectTransform.anchorMax = new Vector2(0.5f, 1);
            txt.rectTransform.pivot = new Vector2(0.5f, 1); txt.rectTransform.anchoredPosition = new Vector2(0, -30);
            _timer = txt;

            _hp = CreateBar(canvasGO.transform, "HP", new Vector2(0, 1), new Vector2(0.4f, 1), new Vector2(10, -10), new Color(0.9f, 0.2f, 0.2f, 1));
            _xp = CreateBar(canvasGO.transform, "XP", new Vector2(0, 1), new Vector2(0.4f, 1), new Vector2(10, -40), new Color(0.2f, 0.7f, 1f, 1));
        }

        Slider CreateBar(Transform parent, string name, Vector2 min, Vector2 max, Vector2 pos, Color fill)
        {
            var root = new GameObject(name);
            root.transform.SetParent(parent);
            var bg = root.AddComponent<Image>();
            bg.color = new Color(1, 1, 1, 0.1f);
            var rt = bg.rectTransform;
            rt.anchorMin = min; rt.anchorMax = max; rt.pivot = new Vector2(0, 1);
            rt.anchoredPosition = pos; rt.sizeDelta = new Vector2(0, 20);

            var fillGO = new GameObject("Fill");
            fillGO.transform.SetParent(root.transform);
            var fillImg = fillGO.AddComponent<Image>();
            fillImg.color = fill;
            var fillRT = fillImg.rectTransform;
            fillRT.anchorMin = new Vector2(0, 0); fillRT.anchorMax = new Vector2(1, 1);
            fillRT.offsetMin = new Vector2(2, 2); fillRT.offsetMax = new Vector2(-2, -2);

            var slider = root.AddComponent<Slider>();
            slider.targetGraphic = bg;
            slider.fillRect = fillRT;
            slider.minValue = 0; slider.maxValue = 1; slider.value = 1;
            return slider;
        }

        void Update()
        {
            if (_ended) return;

            _timerSec -= Time.deltaTime;
            RunNormalized = 1f - Mathf.Clamp01(_timerSec / Config.RunDurationSeconds);
            _timer.text = System.TimeSpan.FromSeconds(Mathf.Max(0, _timerSec)).ToString(@"m\:ss");

            // Update UI
            var playerHealth = FindObjectOfType<PlayerController>()?.GetComponent<Health>();
            if (playerHealth)
            {
                _hp.value = Mathf.InverseLerp(0, playerHealth.MaxHP, playerHealth.CurrentHP);
                if (playerHealth.CurrentHP <= 0) End(false);
            }
            _xp.value = Mathf.InverseLerp(0, PlayerStats.XPPerLevel, PlayerStats.CurrentXP);

            if (_timerSec <= 0) End(true);

            // if (Keyboard.current.escapeKey.wasPressedThisFrame)
            // {
            //     SceneManager.LoadScene(SceneNames.Pause, LoadSceneMode.Single);
            // }
        }

        public void GainXP(int amount)
        {
            if (PlayerStats.AddXP(amount, out var leveled))
            {
                if (leveled) FindObjectOfType<CameraRig>()?.Shake(0.35f, 0.25f);
            }
        }

        void End(bool victory)
        {
            _ended = true;
            AnalyticsService.Track("run_end", ("victory", victory), ("time", (int)(Config.RunDurationSeconds - _timerSec)));
            SceneManager.LoadScene(victory ? SceneNames.Victory : SceneNames.Death);
        }
    }
}
