using UnityEngine;

namespace TappyTale
{
    public static class CosmeticService
    {
        public static void ApplyCosmetic(GameObject target, CosmeticItem item)
        {
            if (target == null || item == null) return;
            var rends = target.GetComponentsInChildren<MeshRenderer>(true);
            foreach (var r in rends)
            {
                var mat = new Material(r.sharedMaterial);
                mat.color = item.PrimaryColor;
                r.sharedMaterial = mat;
            }
        }
    }
}
