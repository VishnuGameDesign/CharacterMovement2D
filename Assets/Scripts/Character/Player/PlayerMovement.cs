// MIT License – Created by VishnuGameDesign, 2025

using UnityEngine;

namespace Character.Player
{
    /// <summary>
    /// Handles 2D movement logic and ground checking based on player input.
    /// Inherits from CharacterMovementBase and is initialized via dependency injection.
    /// </summary>
    public class PlayerMovement : CharacterMovementBase
    {
        // components received in the Init function 
        private Rigidbody2D _rigidbody;
        private IPlayerInputEvents _playerInputs; 
        private PlayerData _playerData;
        
        public void Init(Rigidbody2D rb, IPlayerInputEvents playerInputs, PlayerData playerData)
        {
            _rigidbody = rb;
            _playerInputs = playerInputs;
            _playerData = playerData;

            _playerInputs.OnMove += SetMoveInput;
        }

        private void OnEnable()
        {
            if(_playerInputs != null)
                _playerInputs.OnMove += SetMoveInput;
        }
        
        private void OnDisable()
        { 
            if(_playerInputs != null)
                _playerInputs.OnMove -= SetMoveInput;
        }

        private void FixedUpdate()
        {
            IsGrounded = GroundCheck(); // only checks if "EnableGrouchCheck" is set to true
            Vector2 targetPosition = MoveInput * (_playerData.moveSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_rigidbody.position + targetPosition);
        }

        private void SetMoveInput(Vector2 moveInput)
        {
            moveInput = Vector2.ClampMagnitude(moveInput, 1);
            if (moveInput != Vector2.zero)
            {
                moveInput.Normalize();
            }

            float rightDir = 180.0f;
            float leftDir = 0.0f;
            
            switch (moveInput.x)
            {
                case > 0:
                    SetLookDirection(rightDir);
                    break;
                case < 0:
                    SetLookDirection(leftDir);
                    break;
            }

            MoveInput = moveInput;
        }

        private void SetLookDirection(float direction)
        {
            _rigidbody.transform.localRotation = Quaternion.Euler(0f, direction, 0f);
        }
         
        private bool GroundCheck()
        {
            if (!_playerData.enableGroundCheck)
                return false;

            Vector2 origin = transform.position + _playerData.groundCheckOriginOffset;
            Vector2 direction = Vector2.down;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _playerData.groundCheckDistance,
                _playerData.groundLayerMask);
            
            Debug.DrawRay(origin, direction * _playerData.groundCheckDistance, Color.red);
            if(!hit.collider)
                return false;
            
            return ((1 << gameObject.layer) & _playerData.groundLayerMask) == 0;
        }
    }
}