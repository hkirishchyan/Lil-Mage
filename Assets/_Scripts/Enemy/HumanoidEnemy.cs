using System;
using Enemy.States;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class HumanoidEnemy : AEnemy
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private float _attackDelay = 1f;
        [SerializeField] private int _damage = 10;
        [SerializeField, ReadOnly] private string _stateName;

        private AHealth _attackTarget;
        private float _lastAttackTime;
        private Action<AEnemy> _returnToPool;
        private IState<HumanoidEnemy> _currentState = new ChaiseState();
        
        public override void Initialize(AHealth attackTarget)
        {
            _attackTarget = attackTarget;
        }

        private void Start()
        {
            _stateName = _currentState.GetType().Name;
            _agent.isStopped = false;
        }
        
        private void Update()
        {
            _currentState = _currentState.DoState(this);
            _stateName = _currentState.GetType().Name;
        }
        
        public void ChasePlayer()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_attackTarget.transform.position);
        }

        public override void Attack()
        {
            _agent.isStopped = true;
            if (Time.time - _lastAttackTime >= _attackDelay)
            {
                _lastAttackTime = Time.time;
                _attackTarget.TakeDamage(_damage);
            }
        }

        public bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, _attackTarget.transform.position) <= _attackRange;
        }

        public override void InitializePool(Action<AEnemy> returnAction)
        {
            _returnToPool = returnAction;
        }

        public override void ReturnToPool()
        {
            _returnToPool?.Invoke(this);
        }
        
        public override void Die()
        {
            gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            ReturnToPool();
        }
    }
}