using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField] private AHealth _stats;
        [SerializeField] private Image _healthBar;

        private void OnEnable()
        {
            _stats.OnStatsChange += SetVisual;
        }

        private void SetVisual(float health, float maxHealth)
        {
            _healthBar.fillAmount = health / maxHealth;
        }
        
        private void OnDisable()
        {
            _stats.OnStatsChange -= SetVisual;
        }
    }
}