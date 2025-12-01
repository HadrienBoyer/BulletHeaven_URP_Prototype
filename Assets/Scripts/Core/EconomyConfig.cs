using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Economy/Config")]
    public class EconomyConfig : ScriptableObject
    {
        public CurrencyDefinition SoftCurrency;
        public CurrencyDefinition PremiumCurrency;
        public IAPProduct[] IAPCatalog;
        public CosmeticItem[] Cosmetics;
    }
}
