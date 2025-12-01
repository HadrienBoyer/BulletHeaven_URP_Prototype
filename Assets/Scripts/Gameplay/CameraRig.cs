using UnityEngine;

namespace TappyTale
{
    public class CameraRig : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset = new Vector3(0, 20, -10);
        public float FollowLerp = 0.15f;
        Vector3 _vel;

        void LateUpdate()
        {
            if (!Target) return;
            var dst = Target.position + Offset;
            transform.position = Vector3.SmoothDamp(transform.position, dst, ref _vel, FollowLerp);
            transform.LookAt(Target.position);
        }

        public void Shake(float intensity = 0.2f, float duration = 0.2f)
        {
#if TWEEN_DOTWEEN
            DG.Tweening.DOTween.Kill("camshake");
            var start = transform.position;
            DG.Tweening.DOVirtual.Float(0, duration, duration, t =>
            {
                var j = Random.insideUnitSphere * intensity;
                j.y *= 0.5f;
                transform.position = start + j;
            }).SetTarget("camshake").OnComplete(()=> transform.position = start);
#else
            // simple impulse
            transform.position += Random.insideUnitSphere * intensity * 0.25f;
#endif
        }
    }
}
