using UnityEngine;

namespace TappyTale
{
    public class AutoShooter : MonoBehaviour
    {
        public Transform Muzzle;
        public WeaponDefinition Weapon;
        public GameObject ProjectilePrefab;
        public bool Auto = true;
        float _timer;

        public void TriggerOnce() => Fire();

        void Update()
        {
            if (!Auto || Weapon == null) return;
            _timer += Time.deltaTime;
            var interval = 1f / Mathf.Max(0.01f, Weapon.FireRate);
            if (_timer >= interval)
            {
                _timer -= interval;
                Fire();
            }
        }

        void Fire()
        {
            if (ProjectilePrefab == null || Muzzle == null || Weapon == null) return;
            for (int i = 0; i < Mathf.Max(1, Weapon.ProjectilesPerShot); i++)
            {
                var go = Object.Instantiate(ProjectilePrefab, Muzzle.position, Quaternion.identity);
                var dir = Quaternion.Euler(0, Random.Range(-Weapon.SpreadAngle, Weapon.SpreadAngle), 0) * Muzzle.forward;
                var p = go.GetComponent<Projectile>();
                p.Init(dir.normalized * Weapon.ProjectileSpeed, Weapon.Damage, gameObject);
            }
        }
    }
}
