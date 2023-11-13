using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : AHealth
    {
        [SerializeField] private GameObject _deathEffect;
        protected override void Die()
        {
            Instantiate(_deathEffect,transform.position,Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}