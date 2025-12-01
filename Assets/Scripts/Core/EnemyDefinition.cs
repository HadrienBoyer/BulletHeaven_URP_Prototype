using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Enemies/Enemy")]
    public class EnemyDefinition : ScriptableObject
    {
        public int MaxHP = 20;
        public float MoveSpeed = 3.5f;
        public float FireRate = 1.2f;
        public float ProjectileSpeed = 12f;
        public int XPReward = 10;
    }
}
