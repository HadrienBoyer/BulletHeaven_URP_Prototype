using UnityEngine;

namespace TappyTale
{
    public class Health : MonoBehaviour
    {
        public int MaxHP = 100;
        public int CurrentHP;
        public System.Action OnDeath;

        void Awake() => CurrentHP = MaxHP;

        public void Damage(int amount)
        {
            if (CurrentHP <= 0) return;
            CurrentHP -= amount;
            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
                OnDeath?.Invoke();
                Destroy(gameObject);
            }
        }

        public void Heal(int amount)
        {
            CurrentHP = Mathf.Min(MaxHP, CurrentHP + amount);
        }
    }
}
