using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Cosmetics/Item")]
    public class CosmeticItem : ScriptableObject
    {
        public string Id;
        public string DisplayName;
        public Color PrimaryColor = Color.white;
        public Color SecondaryColor = Color.black;
        public CurrencyDefinition PriceCurrency;
        public int PriceAmount;
    }
}
