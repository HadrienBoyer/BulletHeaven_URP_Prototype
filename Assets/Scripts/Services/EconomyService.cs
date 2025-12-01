using System.Linq;
using UnityEngine;

namespace TappyTale
{
    public static class EconomyService
    {
        public static EconomyConfig Config;

        public static int Soft
        {
            get => SaveService.Data.SoftCurrency;
            set { SaveService.Data.SoftCurrency = Mathf.Max(0, value); SaveService.Save(); }
        }

        public static int Premium
        {
            get => SaveService.Data.PremiumCurrency;
            set { SaveService.Data.PremiumCurrency = Mathf.Max(0, value); SaveService.Save(); }
        }

        public static bool OwnsCosmetic(string id) => SaveService.Data.OwnedCosmetics.Contains(id);
        public static void GrantCosmetic(string id)
        {
            if (OwnsCosmetic(id)) return;
            var list = SaveService.Data.OwnedCosmetics.ToList();
            list.Add(id);
            SaveService.Data.OwnedCosmetics = list.ToArray();
            SaveService.Save();
        }
    }
}
