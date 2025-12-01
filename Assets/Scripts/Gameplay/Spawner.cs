using UnityEngine;
using System.Collections.Generic;

namespace TappyTale
{
    public class Spawner : MonoBehaviour
    {
        public EnemyDefinition[] Enemies;
        public GameObject EnemyPrefab;
        public RunSettings RunSettings;
        public Transform Player;

        float _spawnTimer;
        List<GameObject> _alive = new List<GameObject>();

        public void ResetAll()
        {
            foreach (var e in _alive) if (e) GameObject.Destroy(e);
            _alive.Clear();
        }

        void Update()
        {
            if (Player == null || Enemies == null || Enemies.Length == 0) return;
            _spawnTimer -= Time.deltaTime;
            var t = Mathf.Clamp01(GameLoopController.RunNormalized);
            var interval = Mathf.Lerp(RunSettings.SpawnIntervalStart, RunSettings.SpawnIntervalEnd, t);
            if (_spawnTimer <= 0 && _alive.Count < RunSettings.MaxConcurrentEnemies)
            {
                _spawnTimer = interval;
                var idx = RNGService.WeightedIndex(1, 0.6f, 0.3f); // progressively tougher if you add more entries
                idx = Mathf.Clamp(idx, 0, Enemies.Length - 1);
                var angle = Random.value * Mathf.PI * 2f;
                var pos = Player.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * RunSettings.SpawnRadius;
                var go = Instantiate(EnemyPrefab, pos, Quaternion.identity);
                var ec = go.GetComponent<EnemyController>();
                ec.Definition = Enemies[idx];
                _alive.Add(go);
            }
        }
    }
}
