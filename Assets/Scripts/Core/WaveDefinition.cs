using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Combat/Wave")]
    public class WaveDefinition : ScriptableObject
    {
        [Range(0,1)] public float StartTimeNormalized;
        [Range(0,1)] public float EndTimeNormalized = 1;
        public EnemyDefinition[] Enemies;
        public AnimationCurve SpawnRateOverTime = AnimationCurve.EaseInOut(0, 1, 1, 0.3f);
    }
}
