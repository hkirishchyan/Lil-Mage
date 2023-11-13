using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealth : AHealth, IHealable
    {
        public void Heal(int healAmount)
        {
            if (CurrentHealth < MaxHealth)
            {
                CurrentHealth += healAmount;
                CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
            }
            UpdateHealth();
        }

        protected override void Die()
        {
            SceneManager.LoadScene(0);
        }
    }
}