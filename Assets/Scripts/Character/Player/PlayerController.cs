// MIT License – Created by VishnuGameDesign, 2025

using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    /// <summary>
    /// Entry point for the player character.
    /// Initializes movement logic, sets up input bindings, and relays input events via IPlayerInputEvents.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour, IPlayerInputEvents
    {
        [field: Header("Components")]
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public InputActionAsset PlayerControls { get; private set; }

        [field: Header("Data")]
        [field: SerializeField] public PlayerDataAsset PlayerDataAsset { get; private set; }
        
        [Header("Input Actions")]
        [SerializeField] private InputMappingRefs _inputMappingRefs;
            
        // input actions
        private InputAction _moveAction;
        private InputAction _interactAction;

        // delegates
        public event MoveEventDelegate OnMove;
        public event InteractEventDelegate OnInteract;

        private PlayerData _playerData = null;
        private PlayerMovement _playerMovement = null;

        private void OnValidate()
        {
            if (Rigidbody == null)
            {
                Rigidbody = GetComponent<Rigidbody2D>();
                Rigidbody.gravityScale = 0;
                Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        protected void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>() ?? GetComponentInChildren<PlayerMovement>();

            if (!_playerMovement)
            {
                GameObject playerGO = new GameObject("PlayerMovementComponent", typeof(PlayerMovement));
                playerGO.transform.SetParent(transform);
                playerGO.transform.localPosition = Vector3.zero;
                _playerMovement = playerGO.GetComponent<PlayerMovement>();
            }
            
            _playerData = new PlayerData(PlayerDataAsset);
            _playerMovement.Init(Rigidbody,this, _playerData);

            if (_inputMappingRefs != null)
            {
                _moveAction = _inputMappingRefs.moveAction;
                _interactAction = _inputMappingRefs.interactAction; 
            }
        }
        
        private void OnEnable()
        {
            RegisterInputActions();

            _moveAction.Enable();
            _interactAction.Enable();
        }

        private void OnDisable()
        {
            DeregisterInputActions();
            
            _moveAction.Disable();
            _interactAction.Disable();
        }

        private void RegisterInputActions()
        {
            // move action contexts
            _moveAction.performed += Move;
            _moveAction.canceled += Move;

            // interact action contexts
            _interactAction.performed += Interact;
            _interactAction.canceled += Interact;
        }

        private void DeregisterInputActions()
        {
            _moveAction.performed -= Move;
            _moveAction.canceled -= Move;

            _interactAction.performed -= Interact;
            _interactAction.canceled -= Interact;
        }

        private void Move(InputAction.CallbackContext context)
        {
            var moveInput = context.ReadValue<Vector2>();
            OnMove?.Invoke(moveInput);
        }

        private void Interact(InputAction.CallbackContext context)
        {
            var interactInput = context.ReadValueAsButton();
            OnInteract?.Invoke(interactInput);
        }
    }
}