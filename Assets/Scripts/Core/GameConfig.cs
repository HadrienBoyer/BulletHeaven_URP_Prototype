using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Run")]
        [Range(30, 600)] public int RunDurationSeconds = 180;
        public AnimationCurve DifficultyOverTime = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Player")]
        public float BaseMoveSpeed = 6f;
        public float DashSpeed = 20f;
        public float DashDuration = 0.15f;
        public float FireRate = 4f; // shots per second
        public float ProjectileSpeed = 22f;
        public float BaseAoERadius = 2f;
        public float AoEPerXP = 0.02f;

        [Header("Enemies")]
        public float BaseEnemyMoveSpeed = 3.5f;
        public float EnemyFireRate = 1.2f;
        public float EnemyProjectileSpeed = 12f;
    }
}
