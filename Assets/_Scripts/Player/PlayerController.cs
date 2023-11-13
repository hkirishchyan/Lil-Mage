using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private CharacterController _movementController;
        [SerializeField, Range(0,10)] private float _movementSpeed = 2f;
        [SerializeField, Range(0, 15)] private float _rotationSpeed = 10f;
        
        [Header("GroundCheck")]
        [SerializeField] private float _sphereCastRadius;
        [SerializeField] private float _groundCheckDistance;
        [SerializeField] private float _groundEpsilon;
        
        private  Vector3 _moveDirection = Vector3.zero;
        private InputManager _inputManager;

        [Inject]
        public void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Update()
        {
            HandleMovementInput();
            HandleRotationInput();
            Gravity();
        }

        private void HandleMovementInput()
        {
            float vertical = _inputManager.MovementDirection().y;
            Vector3 movementDirection = transform.forward * vertical;
            _movementController.Move(movementDirection * (_movementSpeed * Time.deltaTime));
        }
        private void HandleRotationInput()
        {
            float rotationInput =_inputManager.MovementDirection().x;
            transform.Rotate(Vector3.up, rotationInput * _rotationSpeed * Time.deltaTime * 10f);
        }

        private void Reset()
        {
            if (!_movementController)
            {
                _movementController = GetComponent<CharacterController>();
            }
        }

        private void Gravity()
        {
            if (IsGrounded()) _moveDirection.y = -0.5f;
            _moveDirection.y -= 9.8f * Time.deltaTime;
            _movementController.Move(_moveDirection * Time.deltaTime);
        }
        
        private bool IsGrounded()
        {
            var ray = new Ray(transform.position + Vector3.up * _groundEpsilon, Vector3.down);
            return Physics.SphereCast(ray, _sphereCastRadius, _groundCheckDistance);
        }
    }
}