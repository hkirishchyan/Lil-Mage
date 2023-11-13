using System;
using Enemy.States;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
namespace Enemy
{
    public abstract class AEnemy : MonoBehaviour, ObjectPool.IPoolable<AEnemy>
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private float _attackDelay = 1f;
        [SerializeField] private int _damage = 10;
        [SerializeField, ReadOnly] private string _stateName;

        private float _lastAttackTime;
        private Action<AEnemy> _returnToPool;
        private PlayerComponentManager _player;
        private IState<AEnemy> _currentState = new ChaiseState();

        public virtual void Initialize(PlayerComponentManager player)
        {
            _stateName = _currentState.GetType().Name;
            _agent.isStopped = false;
            _player = player;
        }

        private void Update()
        {
            _currentState = _currentState.DoState(this);
            _stateName = _currentState.GetType().Name;
        }

        public void ChasePlayer()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);
        }

        public void AttackPlayer()
        {
            _agent.isStopped = true;
            if (Time.time - _lastAttackTime >= _attackDelay)
            {
                Attack();
            }
        }

        public bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, _player.transform.position) <= _attackRange;
        }

        private void Attack()
        {
            _lastAttackTime = Time.time;
            _player.PlayerHealth.TakeDamage(_damage);
        }

        public void Initialize(Action<AEnemy> returnAction)
        {
            _returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            _returnToPool?.Invoke(this);
        }
        
        public virtual void OnDie()
        {
            gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            ReturnToPool();
        }
    }
}