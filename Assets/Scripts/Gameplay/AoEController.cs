using UnityEngine;

namespace TappyTale
{
    [RequireComponent(typeof(SphereCollider))]
    public class AoEController : MonoBehaviour
    {
        SphereCollider _col;
        public int DamagePerTick = 1;
        public float TickInterval = 0.5f;
        float _timer;

        void Awake()
        {
            _col = GetComponent<SphereCollider>();
            _col.isTrigger = true;
        }

        public void SetRadius(float r)
        {
            _col.radius = Mathf.Max(0.1f, r);
            transform.localScale = Vector3.one * (r * 2f);
        }

        void OnTriggerStay(Collider other)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = TickInterval;
                var h = other.GetComponentInParent<Health>();
                if (h != null && other.transform.root.tag != "Player")
                    h.Damage(DamagePerTick);
            }
        }
    }
}
