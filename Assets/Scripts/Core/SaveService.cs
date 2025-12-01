using System.IO;
using UnityEngine;

namespace TappyTale
{
    /// <summary> Very small JSON save for cosmetics/currencies/XPs between runs. </summary>
    public static class SaveService
    {
        private static string Path => System.IO.Path.Combine(Application.persistentDataPath, "save.json");

        [System.Serializable]
        public class SaveData
        {
            public int SoftCurrency;
            public int PremiumCurrency;
            public string[] OwnedCosmetics = new string[0];
        }

        public static SaveData Data { get; private set; } = new SaveData();

        public static void Load()
        {
            try
            {
                if (File.Exists(Path))
                {
                    var json = File.ReadAllText(Path);
                    Data = JsonUtility.FromJson<SaveData>(json);
                }
                else
                {
                    Data = new SaveData();
                }
            }
            catch { Data = new SaveData(); }
        }

        public static void Save()
        {
            try
            {
                var json = JsonUtility.ToJson(Data, prettyPrint: true);
                File.WriteAllText(Path, json);
            }
            catch { /* ignore */ }
        }
    }
}
