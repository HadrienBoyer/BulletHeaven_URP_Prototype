using UnityEngine;

namespace TappyTale
{
    [CreateAssetMenu(menuName = "TappyTale/Progression/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public int MaxHP = 100;
        public int CurrentHP;
        public int Level = 1;
        public int CurrentXP = 0;
        public int XPPerLevel = 100;

        public void ResetRun()
        {
            CurrentHP = MaxHP;
            Level = 1;
            CurrentXP = 0;
        }

        public bool AddXP(int amount, out bool leveledUp)
        {
            leveledUp = false;
            CurrentXP += amount;
            if (CurrentXP >= XPPerLevel)
            {
                CurrentXP -= XPPerLevel;
                Level++;
                leveledUp = true;
                return true;
            }
            return false;
        }
    }
}
