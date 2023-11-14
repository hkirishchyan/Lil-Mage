using System;
using System.Collections.Generic;
using Player.Abilities;
using UI;
using UnityEngine;
using Utilities;
using Zenject;

namespace Player
{
    public class PlayerAbilityManager : MonoBehaviour
    {
        [SerializeField] private Transform _spellPoint;
        [SerializeField] private AbilityView _abilityView;
        [SerializeField] private List<AbilityScriptable> _playerAbilities;
        [SerializeField] private int _selectedIndex;
        [SerializeField] private float _attackDelay = 0.5f;
   
        private float _lastAttackTime;
        private InputManager _inputManager;
        private event Action _onAttackPerformed;
        public event Action OnAttackPerformed { add => _onAttackPerformed += value; remove => _onAttackPerformed -= value; }

        [Inject]
        public void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Start()
        {
            _abilityView.UpdateImage(_playerAbilities[_selectedIndex].Visual);
        }

        private void Update()
        {
            if (Time.time - _lastAttackTime >= _attackDelay 
                && _inputManager.Attack().WasPerformedThisFrame())
            {
                _lastAttackTime = Time.time;
                Instantiate(_playerAbilities[_selectedIndex].Ability, _spellPoint.transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
                _onAttackPerformed?.Invoke();
            }

            SwitchAbilityInput();
        }

        private void SwitchAbilityInput()
        {
            var switchSpell = _inputManager.SwitchSpell();
            if (switchSpell.WasPerformedThisFrame())
            {
              
                if (switchSpell.ReadValue<float>()>0)
                {
                    SwitchAbility(false);
                }
                
                if (switchSpell.ReadValue<float>() < 0)
                {
                    SwitchAbility(true);
                }
            }
        }

        private void SwitchAbility(bool increment)
        {
            _selectedIndex = increment ? ++_selectedIndex : --_selectedIndex;
            _selectedIndex = _playerAbilities.GetRingIndex(_selectedIndex);
            _abilityView.UpdateImage(_playerAbilities[_selectedIndex].Visual);
        }
    }
}