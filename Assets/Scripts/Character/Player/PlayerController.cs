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
        [Header("Components")]
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public InputActionAsset PlayerControls { get; private set; }

        [Header("Data")]
        [field: SerializeField] public PlayerDataAsset PlayerDataAsset { get; private set; }
            
        // input actions
        private InputAction _moveAction;
        private InputAction _interactAction;
        private const string MoveActionName = "Move";
        private const string InteractActionName = "Interact";

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

        private void Awake()
        {
            GameObject playerGO = new GameObject("PlayerMovementComponent", typeof(PlayerMovement));
            playerGO.transform.SetParent(transform);
            playerGO.transform.localPosition = Vector3.zero;
            _playerMovement = playerGO.GetComponent<PlayerMovement>();
            
            _playerData = new PlayerData(PlayerDataAsset);
            _playerMovement.Init(Rigidbody,this, _playerData);
            
            _moveAction = PlayerControls.FindAction(MoveActionName);
            _interactAction = PlayerControls.FindAction(InteractActionName); 
            RegisterInputActions();
        }
        
        private void OnEnable()
        {
            _moveAction.Enable();
            _interactAction.Enable();
        }

        private void OnDisable()
        {
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

