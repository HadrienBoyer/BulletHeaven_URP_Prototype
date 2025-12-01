using UnityEngine;

namespace TappyTale
{
    [RequireComponent(typeof(SphereCollider))]
    public class ExperienceOrb : MonoBehaviour
    {
        public int XP = 5;
        public float MagnetRange = 6f;
        Transform _player;

        void Start()
        {
            _player = GameObject.FindWithTag("Player")?.transform;
        }

        void Update()
        {
            if (_player == null) return;
            var dist = Vector3.Distance(transform.position, _player.position);
            if (dist < MagnetRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.position, Time.deltaTime * 12f);
            }
            else
            {
                transform.Rotate(Vector3.up, 90f * Time.deltaTime, Space.World);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var loop = FindObjectOfType<GameLoopController>();
                if (loop) loop.GainXP(XP);
                Destroy(gameObject);
            }
        }
    }
}
