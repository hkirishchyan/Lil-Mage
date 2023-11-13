using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _playerAnimator;
        
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int CastSpell = Animator.StringToHash("CastSpell");

        private PlayerComponentManager _playerComponentManager;
        private InputManager _inputManager;

        [Inject]
        public void Construct(PlayerComponentManager playerComponentManager, InputManager inputManager)
        {
            _playerComponentManager = playerComponentManager;
            _inputManager = inputManager;
        }

        private void OnEnable()
        {
            _playerComponentManager.PlayerAbilityManager.OnAttackPerformed += PerformAttack;
        }

        private void Update()
        {
            _playerAnimator.SetFloat(Vertical,Mathf.Abs(_inputManager.MovementDirection().y));
        }

        private void PerformAttack()
        {
            AttackAnimation(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid AttackAnimation(CancellationToken token)
        {
            await SetLayerWeight(1, 0.2f, token);
            _playerAnimator.SetTrigger(CastSpell);
            await UniTask.WaitForSeconds(_playerAnimator.GetCurrentAnimatorStateInfo(1).length, cancellationToken: token);
            await SetLayerWeight(0, 0.2f, token);
        }

        private async UniTask SetLayerWeight(float value, float time, CancellationToken token)
        {
            await DOTween.To(() => _playerAnimator.GetLayerWeight(1), x => 
                _playerAnimator.SetLayerWeight(1, x), value, time).ToUniTask(cancellationToken:token);
        }

        private void OnDisable()
        {
            _playerComponentManager.PlayerAbilityManager.OnAttackPerformed -= PerformAttack;
        }
    }
}