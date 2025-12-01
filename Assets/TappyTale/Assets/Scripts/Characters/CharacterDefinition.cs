using UnityEngine;

namespace GiscardPunk.ARPG
{
    [CreateAssetMenu(
        fileName = "NewCharacterDefinition",
        menuName = "GiscardPunk/Characters/Character Definition")]
    public class CharacterDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string Id = "character_default";
        public string DisplayName = "Character";

        [Header("Core Stats")]
        public float MaxHealth = 100f;
        public float MoveSpeed = 4f;

        [Header("Dodge")]
        public float DodgeDistance = 3f;
        public float DodgeDuration = 0.2f;

        [Header("Combat")]
        public WeaponDefinition StartingWeapon;
    }
}
