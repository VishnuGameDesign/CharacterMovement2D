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

        [Header("Data")] 
        [SerializeField] public PlayerDataAsset _playerData;
        
        [Header("Input Actions")]
        [SerializeField] private InputMappingRefs _inputMappingRefs;
            
        // input actions
        private InputAction _moveAction;
        private InputAction _interactAction;

        // delegates
        public event MoveEventDelegate OnMove;
        public event InteractEventDelegate OnInteract;

        private PlayerMovement _playerMovement = null;

        private void OnValidate()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected void Awake()
        {
            if (Rigidbody)
            {
                Rigidbody.gravityScale = 0;
                Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            CreateMovementHandler();
            if(!_playerMovement)
                return;
            
            InitiateHandlers();
            AssignInputActions();
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

        private void CreateMovementHandler()
        {
            _playerMovement = GetComponent<PlayerMovement>() ?? GetComponentInChildren<PlayerMovement>();
            if (!_playerMovement)
            {
                var movementHandler = CreateHandlers<PlayerMovement>("PlayerMovementComponent");
                if(movementHandler)
                    _playerMovement = movementHandler;
            }
        }

        private void InitiateHandlers()
        {
            _playerMovement.Init(Rigidbody, this, _playerData);
        }
        
        private T CreateHandlers<T>(string gameObjectName) where T : class
        {
            var newGameObject = new GameObject(gameObjectName, typeof(T));
            newGameObject.transform.SetParent(transform);
            newGameObject.transform.localPosition = Vector3.zero;
            return newGameObject.GetComponent<T>();
        }

        private void AssignInputActions()
        {
            if (_inputMappingRefs == null) 
                return;
            
            _moveAction = _inputMappingRefs.moveAction;
            _interactAction = _inputMappingRefs.interactAction;
        }

        private void RegisterInputActions()
        {
            _moveAction.performed += Move;
            _moveAction.canceled += Move;

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