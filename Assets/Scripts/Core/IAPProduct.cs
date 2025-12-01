using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/IAP/Product")]
    public class IAPProduct : ScriptableObject
    {
        public string StoreId; // Apple/Google ID
        public string DisplayName;
        public string Description;
        public bool IsConsumable = true;
        public int GrantPremiumCurrency = 0;
    }
}
