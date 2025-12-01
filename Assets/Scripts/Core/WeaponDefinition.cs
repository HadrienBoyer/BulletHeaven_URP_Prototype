using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Combat/Weapon")]
    public class WeaponDefinition : ScriptableObject
    {
        public float FireRate = 4f;
        public float ProjectileSpeed = 22f;
        public int Damage = 10;
        public float SpreadAngle = 3f;
        public int ProjectilesPerShot = 1;
    }
}
