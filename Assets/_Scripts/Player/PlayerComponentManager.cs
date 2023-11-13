using UnityEngine;

namespace Player
{
    public class PlayerComponentManager : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerAnimationManager _playerAnimationManager;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerAbilityManager _playerAbilityManager;

        public PlayerHealth PlayerHealth => _playerHealth;
        public PlayerAnimationManager PlayerAnimationManager => _playerAnimationManager;
        public PlayerController PlayerController => _playerController;
        public PlayerAbilityManager PlayerAbilityManager => _playerAbilityManager;
    }
}