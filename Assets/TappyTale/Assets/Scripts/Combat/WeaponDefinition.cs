using UnityEngine;

namespace GiscardPunk.ARPG
{
    [CreateAssetMenu(
        fileName = "NewWeaponDefinition",
        menuName = "GiscardPunk/Combat/Weapon Definition")]
    public class WeaponDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string Id = "weapon_default";
        public string DisplayName = "Weapon";

        [Header("Damage")]
        public float BaseDamage = 10f;
        public DamageType DamageType = DamageType.Physical;

        [Header("Timing")]
        [Tooltip("Time between two attacks in seconds.")]
        public float AttackCooldown = 0.4f;

        [Header("Hit detection")]
        [Tooltip("Attack radius used by the default AttackHitbox.")]
        public float AttackRadius = 1.2f;

        [Tooltip("Layers that can be hit by this weapon.")]
        public LayerMask HitMask;
    }
}
