using UnityEngine;
using UnityEngine.InputSystem;

namespace TappyTale
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerStats Stats;
        public WeaponDefinition Weapon;
        public GameConfig Config;
        public Transform AimPivot;
        public AutoShooter Shooter;
        public AoEController AoE;

        CharacterController _cc;
        PlayerControls _controls;
        Vector2 _move;
        Vector2 _aim;
        float _dashTimer;
        bool _isDashing;

        void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _controls = new PlayerControls();
            _controls.Gameplay.Move.performed += ctx => _move = ctx.ReadValue<Vector2>();
            _controls.Gameplay.Move.canceled += ctx => _move = Vector2.zero;
            _controls.Gameplay.Aim.performed  += ctx => _aim = ctx.ReadValue<Vector2>();
            _controls.Gameplay.Fire.performed += ctx => Shooter.TriggerOnce();
            _controls.Gameplay.Dash.performed += ctx => TryDash();
        }

        void OnEnable() => _controls.Enable();
        void OnDisable() => _controls.Disable();

        void Update()
        {
            var move = new Vector3(_move.x, 0, _move.y);
            var speed = _isDashing ? Config.DashSpeed : Config.BaseMoveSpeed;
            _cc.SimpleMove(move * speed);

            // Aim to mouse world or gamepad stick
            Vector3 aimDir;
            if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.05f)
            {
                aimDir = new Vector3(_aim.x, 0, _aim.y);
            }
            else
            {
                var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (new Plane(Vector3.up, Vector3.zero).Raycast(ray, out var enter))
                {
                    var hitPoint = ray.GetPoint(enter);
                    aimDir = (hitPoint - transform.position);
                    aimDir.y = 0;
                }
                else aimDir = Vector3.forward;
            }
            if (aimDir.sqrMagnitude > 0.001f)
            {
                AimPivot.forward = Vector3.Slerp(AimPivot.forward, aimDir.normalized, 0.3f);
            }

            if (_isDashing)
            {
                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0) _isDashing = false;
            }

            // Update AoE radius by XP
            if (AoE) AoE.SetRadius(Config.BaseAoERadius + Stats.Level * Config.AoEPerXP + (Stats.CurrentXP * Config.AoEPerXP * 0.1f));
        }

        void TryDash()
        {
            if (_isDashing) return;
            _isDashing = true;
            _dashTimer = Config.DashDuration;
#if TWEEN_DOTWEEN
            DG.Tweening.DOTween.Kill(this);
            transform.localScale = Vector3.one;
            transform.DOScale(1.15f, 0.08f).SetLoops(2, DG.Tweening.LoopType.Yoyo);
#endif
        }
    }
}
