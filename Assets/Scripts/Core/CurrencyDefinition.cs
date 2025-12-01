using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Economy/Currency")]
    public class CurrencyDefinition : ScriptableObject
    {
        public string Id;
        public string DisplayName;
        public bool IsPremium;
    }
}
