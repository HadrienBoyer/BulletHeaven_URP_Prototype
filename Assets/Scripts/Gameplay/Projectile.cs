using UnityEngine;

namespace TappyTale
{
    [RequireComponent(typeof(SphereCollider))]
    public class Projectile : MonoBehaviour
    {
        Vector3 _vel;
        int _damage;
        GameObject _owner;
        float _life = 4f;

        public void Init(Vector3 velocity, int damage, GameObject owner)
        {
            _vel = velocity;
            _damage = damage;
            _owner = owner;
        }

        void Update()
        {
            transform.position += _vel * Time.deltaTime;
            _life -= Time.deltaTime;
            if (_life <= 0) Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody && other.attachedRigidbody.gameObject == _owner) return;
            var h = other.GetComponentInParent<Health>();
            if (h != null)
            {
                h.Damage(_damage);
#if TWEEN_DOTWEEN
                DG.Tweening.DOVirtual.DelayedCall(0f, () => {});
#endif
                Destroy(gameObject);
            }
        }
    }
}
