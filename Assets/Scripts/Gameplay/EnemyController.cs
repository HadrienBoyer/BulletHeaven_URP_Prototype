using UnityEngine;

namespace TappyTale
{
    [RequireComponent(typeof(Health))]
    public class EnemyController : MonoBehaviour
    {
        public EnemyDefinition Definition;
        public Transform AimPivot;
        public AutoShooter Shooter;
        public GameObject ExperienceOrbPrefab;

        Transform _player;
        Health _health;

        void Awake()
        {
            _health = GetComponent<Health>();
            _health.MaxHP = Definition.MaxHP;
            _health.CurrentHP = _health.MaxHP;
            _health.OnDeath += OnDeath;
        }

        void Start()
        {
            var p = GameObject.FindWithTag("Player");
            if (p) _player = p.transform;
        }

        void Update()
        {
            if (_player == null) return;
            var dir = (_player.position - transform.position); dir.y = 0;
            transform.position += dir.normalized * Definition.MoveSpeed * Time.deltaTime;
            AimPivot.forward = Vector3.Lerp(AimPivot.forward, dir.normalized, 0.2f);
        }

        void FixedUpdate()
        {
            if (Shooter && _player)
            {
                Shooter.Muzzle.forward = ( _player.position - Shooter.Muzzle.position ).normalized;
            }
        }

        void OnDeath()
        {
            if (ExperienceOrbPrefab)
            {
                var go = Instantiate(ExperienceOrbPrefab, transform.position, Quaternion.identity);
                go.GetComponent<ExperienceOrb>().XP = Definition.XPReward;
            }
        }
    }
}
