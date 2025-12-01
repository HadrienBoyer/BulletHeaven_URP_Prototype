using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Config/RunSettings")]
    public class RunSettings : ScriptableObject
    {
        public int Seed = 12345;
        public int MaxConcurrentEnemies = 30;
        public float SpawnRadius = 25f;
        public float SpawnIntervalStart = 1.5f;
        public float SpawnIntervalEnd = 0.35f;
    }
}
