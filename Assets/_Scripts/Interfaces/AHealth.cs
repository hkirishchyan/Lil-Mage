using System;
using UnityEngine;

public abstract class AHealth : MonoBehaviour, IDamageable
{
    [SerializeField, Range(0f, 1000f)] private float _maxHealth = 100;
    [SerializeField, Range(0f, 10f)] private float _defence = 10;
    
    private event Action<float, float> _onStatsChange;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth { get; protected set; }
    
    
    public event Action<float, float> OnStatsChange { add => _onStatsChange += value; remove => _onStatsChange -= value; }
    
    protected void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage * 1/(_defence+1);
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        if (CurrentHealth <= 0) Die();
        UpdateHealth();
    }

    protected void UpdateHealth()
    {
        _onStatsChange?.Invoke(CurrentHealth,MaxHealth);
    }
    
    protected abstract void Die();
}