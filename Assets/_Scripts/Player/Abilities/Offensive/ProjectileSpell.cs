using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player.Abilities.Offensive
{
    public class ProjectileSpell : AAbility
    {
        [SerializeField] private float _spellSpeed = 10f;
        [SerializeField] private float _spellExplosionForce = 1f;
        [SerializeField] private float _spellRadius = 1f;
        [SerializeField] private GameObject _collisionEffect;
        [SerializeField] private Rigidbody _setRigidbody;

        public static event Action SpellHit;

        private void Start()
        {
            _setRigidbody.AddForce(transform.forward * _spellSpeed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            OnProjectileCollision(other);
        }

        private void OnProjectileCollision(Collision other)
        {
            Vector3 collisionPoint = other.contacts[0].point;
            Vector3 collisionNormal = other.contacts[0].normal;
            
            GameObject collisionEffect = Instantiate(_collisionEffect, collisionPoint, Quaternion.FromToRotation(Vector3.up, -collisionNormal));
            SpellHit?.Invoke();
            PushRigidbodies(collisionPoint, collisionNormal);
            Destroy(collisionEffect,1f);
            Destroy(gameObject);
        }
        
        
        private void PushRigidbodies(Vector3 collisionPoint, Vector3 collisionNormal)
        {
            Collider[] colliders = Physics.OverlapSphere(collisionPoint, _spellRadius);
            foreach (Collider internalCollider in colliders)
            {
                if (internalCollider.TryGetComponent(out Rigidbody pushRigidbody))
                {
                    pushRigidbody.AddForceAtPosition(collisionNormal * _spellExplosionForce, collisionPoint, ForceMode.Impulse);
                }

                if (internalCollider.TryGetComponent(out IDamageable damageable) &&
                    internalCollider.gameObject.layer != 3)
                {
                    damageable.TakeDamage(Value);
                }
                
                if (internalCollider.TryGetComponent(out NavMeshAgent navMeshAgent))
                {
                    Vector3 forceDirection = (internalCollider.transform.position - collisionPoint).normalized;
                    navMeshAgent.velocity = forceDirection * _spellExplosionForce * 3f;
                }
            }
        }
    }
}