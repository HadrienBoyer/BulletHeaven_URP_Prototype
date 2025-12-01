using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/IAP/Product")]
    public class IAPProduct : ScriptableObject
    {
        public string StoreId; // Apple/Google ID
        public string DisplayName = "New Product";
        public string Description = "Product Description: Add details here about what this product offers.";
        public bool IsConsumable = true;
        public int GrantPremiumCurrency = 100; // Amount of premium currency granted   
    }
}
