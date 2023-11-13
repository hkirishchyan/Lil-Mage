using UnityEngine;

namespace Player.Abilities.Defensive
{
    public class HealSpell : AAbility
    {
        private void Start()
        {
            Heal();
            Destroy(gameObject,1f);
        }

        private void Heal()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);
            foreach (Collider pushColliders in colliders)
            {
                if (pushColliders.TryGetComponent(out IHealable healable))
                {
                   healable.Heal(Value);
                }
            }
        }
    }
}