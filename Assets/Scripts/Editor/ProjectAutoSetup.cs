#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TappyTale;

/// <summary>
/// Auto-creates URP pipeline, ScriptableObjects, prefabs (primitives), and minimal scenes.
/// Runs on first import.
/// </summary>
[InitializeOnLoad]
public static class ProjectAutoSetup
{
    static ProjectAutoSetup()
    {
        EditorApplication.delayCall += TrySetup;
    }

    static string MarkerPath => "Assets/Resources/ScriptableObjects/__project_initialized.txt";

    static void TrySetup()
    {
        if (System.IO.File.Exists(MarkerPath)) return;

        EnsureFolders();
        EnsureURP();
        CreateScriptableObjects();
        CreatePrefabs();
        CreateScenesAndBuildSettings();

        System.IO.File.WriteAllText(MarkerPath, "ok");
        AssetDatabase.Refresh();
        Debug.Log("[AutoSetup] Project initialised.");
    }

    static void EnsureFolders()
    {
        System.IO.Directory.CreateDirectory("Assets/Resources/ScriptableObjects");
        System.IO.Directory.CreateDirectory("Assets/Prefabs");
        System.IO.Directory.CreateDirectory("Assets/Scenes");
        System.IO.Directory.CreateDirectory("Assets/Materials");
    }

    static void EnsureURP()
    {
        // Create a simple URP asset and assign to GraphicsSettings
        var urpAsset = ScriptableObject.CreateInstance<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>();
        AssetDatabase.CreateAsset(urpAsset, "Assets/URP_Asset.asset");
        GraphicsSettings.defaultRenderPipeline = urpAsset;
        QualitySettings.renderPipeline = urpAsset;
    }

    static void CreateScriptableObjects()
    {
        var gameConfig = ScriptableObject.CreateInstance<GameConfig>();
        AssetDatabase.CreateAsset(gameConfig, "Assets/Resources/ScriptableObjects/GameConfig.asset");

        var run = ScriptableObject.CreateInstance<RunSettings>();
        AssetDatabase.CreateAsset(run, "Assets/Resources/ScriptableObjects/RunSettings.asset");

        var stats = ScriptableObject.CreateInstance<PlayerStats>();
        AssetDatabase.CreateAsset(stats, "Assets/Resources/ScriptableObjects/PlayerStats.asset");

        var wpn = ScriptableObject.CreateInstance<WeaponDefinition>();
        AssetDatabase.CreateAsset(wpn, "Assets/Resources/ScriptableObjects/PlayerWeapon.asset");

        // Enemies set
        for (int i = 0; i < 3; i++)
        {
            var e = ScriptableObject.CreateInstance<EnemyDefinition>();
            e.MaxHP = 10 + 10 * i;
            e.MoveSpeed = 3.0f + i * 0.5f;
            e.FireRate = 0.8f + i * 0.3f;
            e.ProjectileSpeed = 10 + i * 2;
            e.XPReward = 8 + 4 * i;
            AssetDatabase.CreateAsset(e, $"Assets/Resources/ScriptableObjects/Enemy_{i+1}.asset");
        }

        // Economy
        var soft = ScriptableObject.CreateInstance<CurrencyDefinition>(); soft.Id = "coins"; soft.DisplayName = "Coins"; soft.IsPremium = false;
        var hard = ScriptableObject.CreateInstance<CurrencyDefinition>(); hard.Id = "gems";  hard.DisplayName = "Gems";  hard.IsPremium = true;
        AssetDatabase.CreateAsset(soft, "Assets/Resources/ScriptableObjects/Currency_Soft.asset");
        AssetDatabase.CreateAsset(hard, "Assets/Resources/ScriptableObjects/Currency_Premium.asset");

        var cos = ScriptableObject.CreateInstance<CosmeticItem>(); cos.Id="cos_basic_red"; cos.DisplayName="Red Suit"; cos.PrimaryColor = Color.red; cos.PriceCurrency = soft; cos.PriceAmount=500;
        AssetDatabase.CreateAsset(cos, "Assets/Resources/ScriptableObjects/Cosmetic_Red.asset");

        var iap = ScriptableObject.CreateInstance<IAPProduct>(); iap.StoreId="com.tappytale.gems.small"; iap.DisplayName="Small Gem Pack"; iap.Description="Grants 80 gems"; iap.GrantPremiumCurrency=80;
        AssetDatabase.CreateAsset(iap, "Assets/Resources/ScriptableObjects/IAP_SmallGems.asset");

        var eco = ScriptableObject.CreateInstance<EconomyConfig>();
        eco.SoftCurrency = soft; eco.PremiumCurrency = hard;
        eco.IAPCatalog = new IAPProduct[]{ iap };
        eco.Cosmetics = new CosmeticItem[]{ cos };
        AssetDatabase.CreateAsset(eco, "Assets/Resources/ScriptableObjects/Economy.asset");

        AssetDatabase.SaveAssets();
    }

    static void CreatePrefabs()
    {
        // Projectile prefab (Sphere with collider)
        var proj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Object.DestroyImmediate(proj.GetComponent<SphereCollider>());
        var col = proj.AddComponent<SphereCollider>(); col.isTrigger = true; col.radius = 0.25f;
        proj.transform.localScale = Vector3.one * 0.3f;
        proj.name = "Projectile";
        proj.AddComponent<Projectile>();
        PrefabUtility.SaveAsPrefabAsset(proj, "Assets/Prefabs/Projectile.prefab");
        Object.DestroyImmediate(proj);

        // Player prefab (Capsule + Aim + Shooter + AoE + Health)
        var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        var cc = player.AddComponent<CharacterController>();
        cc.height = 2f; cc.radius = 0.5f;
        var h = player.AddComponent<Health>(); h.MaxHP = 100;
        var pc = player.AddComponent<PlayerController>();
        var aimPivot = new GameObject("AimPivot").transform; aimPivot.SetParent(player.transform); aimPivot.localPosition = new Vector3(0,1,0);
        pc.AimPivot = aimPivot;

        var muzzle = new GameObject("Muzzle").transform; muzzle.SetParent(aimPivot); muzzle.localPosition = new Vector3(0,1.0f,1.0f);
        var shooter = player.AddComponent<AutoShooter>();
        shooter.Muzzle = muzzle; shooter.Auto = true;
        pc.Shooter = shooter;

        var aoeGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        aoeGO.name = "AoE"; Object.DestroyImmediate(aoeGO.GetComponent<SphereCollider>());
        var aoeCol = aoeGO.AddComponent<SphereCollider>(); aoeCol.isTrigger = true; aoeCol.radius = 2f;
        var aoe = aoeGO.AddComponent<AoEController>();
        aoeGO.transform.SetParent(player.transform); aoeGO.transform.localPosition = Vector3.zero;
        pc.AoE = aoe;

        PrefabUtility.SaveAsPrefabAsset(player, "Assets/Prefabs/Player.prefab");
        Object.DestroyImmediate(player);

        // Enemy prefab (Cube + Health + EnemyController + Shooter)
        var enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
        enemy.name = "Enemy";
        var eh = enemy.AddComponent<Health>(); eh.MaxHP = 20;
        var enc = enemy.AddComponent<EnemyController>();
        var epivot = new GameObject("AimPivot").transform; epivot.SetParent(enemy.transform); epivot.localPosition = new Vector3(0,0.5f,0);
        enc.AimPivot = epivot;
        var emuzzle = new GameObject("Muzzle").transform; emuzzle.SetParent(epivot); emuzzle.localPosition = new Vector3(0,0.5f,0.8f);
        var es = enemy.AddComponent<AutoShooter>(); es.Muzzle = emuzzle; es.Auto = true;
        enc.Shooter = es;

        PrefabUtility.SaveAsPrefabAsset(enemy, "Assets/Prefabs/Enemy.prefab");
        Object.DestroyImmediate(enemy);

        // Experience orb prefab
        var orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        orb.name = "XP_Orb";
        var orbCol = orb.GetComponent<SphereCollider>(); orbCol.isTrigger = true; orbCol.radius = 0.4f;
        orb.AddComponent<ExperienceOrb>();
        PrefabUtility.SaveAsPrefabAsset(orb, "Assets/Prefabs/XP_Orb.prefab");
        Object.DestroyImmediate(orb);
    }

    static void CreateScenesAndBuildSettings()
    {
        CreateScene(SceneNames.Bootstrap, root => {
            root.AddComponent<SceneBootstrap>();
        });

        CreateScene(SceneNames.MainMenu, root => {
            root.AddComponent<MainMenuController>();
        });

        CreateScene(SceneNames.CutScene, root => {
            root.AddComponent<CutSceneController>();
        });

        CreateScene(SceneNames.Arena, root => {
            var loop = root.AddComponent<GameLoopController>();
            loop.Config = Resources.Load<GameConfig>("ScriptableObjects/GameConfig");
            loop.RunSettings = Resources.Load<RunSettings>("ScriptableObjects/RunSettings");
            loop.PlayerStats = Resources.Load<PlayerStats>("ScriptableObjects/PlayerStats");
            loop.PlayerWeapon = Resources.Load<WeaponDefinition>("ScriptableObjects/PlayerWeapon");
            loop.Enemies = new EnemyDefinition[] {
                Resources.Load<EnemyDefinition>("ScriptableObjects/Enemy_1"),
                Resources.Load<EnemyDefinition>("ScriptableObjects/Enemy_2"),
                Resources.Load<EnemyDefinition>("ScriptableObjects/Enemy_3")
            };
            loop.PlayerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            loop.EnemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");
            loop.ProjectilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Projectile.prefab");
            loop.ExperienceOrbPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/XP_Orb.prefab");

            var spawner = root.AddComponent<Spawner>();
            loop.Spawner = spawner;
        });

        CreateScene(SceneNames.Victory, root => {
            var end = root.AddComponent<EndScreenController>(); end.Victory = true;
        });

        CreateScene(SceneNames.Death, root => {
            var end = root.AddComponent<EndScreenController>(); end.Victory = false;
        });

        CreateScene(SceneNames.Pause, root => {
            root.AddComponent<PauseController>();
        });

        // Build settings
        var guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        var scenes = new EditorBuildSettingsScene[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            scenes[i] = new EditorBuildSettingsScene(path, true);
        }
        System.Array.Sort(scenes, (a,b)=> string.Compare(a.path, b.path));
        EditorBuildSettings.scenes = scenes;
        EditorSceneManager.OpenScene("Assets/Scenes/" + SceneNames.Bootstrap + ".unity", OpenSceneMode.Single);
    }

    static void CreateScene(string name, System.Action<GameObject> onCreateRoot)
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var root = new GameObject(name + "_Root");
        onCreateRoot?.Invoke(root);
        EditorSceneManager.SaveScene(scene, $"Assets/Scenes/{name}.unity");
    }
}
#endif
