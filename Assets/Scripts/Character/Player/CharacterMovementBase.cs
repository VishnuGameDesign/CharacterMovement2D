// MIT License – Created by VishnuGameDesign, 2025

using UnityEngine;

namespace Character.Player
{
    /// <summary>
    /// Abstract base class that holds core movement state such as input, grounded status, etc.
    /// Intended to be inherited by movement behavior classes (e.g., PlayerMovement).
    /// </summary>
    public abstract class CharacterMovementBase : MonoBehaviour
    {
        private Vector2 _moveInput = Vector2.zero;
        private bool _isGrounded = false;
        private Vector2 _groundNormal = Vector2.zero;
        
        public Vector2 MoveInput { get => _moveInput; set => _moveInput = value;}
        public bool IsGrounded { get => _isGrounded; set => _isGrounded = value;}
        public Vector2 GroundNormal { get => _groundNormal; set => _groundNormal = value;}
    }
}