using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    string resourcePath = "_InputSystem/Controls";

    public static InputManager Instance { get; private set; }

    // Input Action Asset
    private InputActionAsset inputActions { get; set; }

    // Action Map Reference
    string actionMapName = "General";

    // Action Name Referencing
    public string _Move { get; private set; } = "Movement";
    public string _Look { get; private set; } = "Look";
    public string _Jump { get; private set; } = "Jump";
    public string _Sprint { get; private set; } = "Sprint";
    public string _Crouch { get; private set; } = "Crouch";
    public string _Interact { get; private set; } = "Interact";
    public string _Pause { get; private set; } = "Pause";

    // Actions
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private InputAction _interactAction;
    private InputAction _pauseAction;

    public bool jumpTriggered { get; private set; } = false;
    public bool crouchTriggered { get; private set; } = false;
    public bool interactTriggered { get; private set; } = false;

    public Vector2 _MoveInput { get; private set; }
    public Vector2 _LookInput { get; private set; }
    public bool _JumpInput { get; private set; }
    public float _SprintValue { get; private set; }
    public bool _CrouchInput { get; private set; }
    public bool _InteractInput { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Debug.Log($"Instance was created on {Instance.name}");

        LoadInputActions();
        RegisterInputActions();

        DontDestroyOnLoad(gameObject);
    }
    private void LoadInputActions()
    {
        // Load InputActionAsset from Resources folder
        inputActions = Resources.Load<InputActionAsset>(resourcePath);

        if (inputActions == null) Debug.Log("Input actions weren't found.");
        else
        {
            Debug.Log($"Input actions were found at {inputActions.name}");
        }
    }
    private void RegisterInputActions()
    {
        _moveAction = inputActions.FindActionMap(actionMapName).FindAction(_Move);
        _lookAction = inputActions.FindActionMap(actionMapName).FindAction(_Look);
        _jumpAction = inputActions.FindActionMap(actionMapName).FindAction(_Jump);
        _sprintAction = inputActions.FindActionMap(actionMapName).FindAction(_Sprint);
        _crouchAction = inputActions.FindActionMap(actionMapName).FindAction(_Crouch);
        _pauseAction = inputActions.FindActionMap(actionMapName).FindAction(_Pause);
        _interactAction = inputActions.FindActionMap(actionMapName).FindAction(_Interact);

        _moveAction.performed += _ => _MoveInput = _.ReadValue<Vector2>();
        _moveAction.canceled += _ => _MoveInput = Vector2.zero;

        _lookAction.performed += _ => _LookInput = _.ReadValue<Vector2>();
        _lookAction.canceled += _ => _LookInput = Vector2.zero;

        _jumpAction.performed += _ => jumpTriggered = true;
        _jumpAction.canceled += _ => jumpTriggered = false;

        _sprintAction.performed += _ => _SprintValue = _.ReadValue<float>();
        _sprintAction.canceled += _ => _SprintValue = 0f;

        _crouchAction.performed += _ => crouchTriggered = true;
        _crouchAction.canceled += _ => crouchTriggered = false;

        _interactAction.performed += _ => interactTriggered = true;
        _interactAction.canceled += _ => interactTriggered = false;

        _pauseAction.performed += _ => CanvasManager.Instance.TogglePauseMenu();
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
        _jumpAction.Enable();
        _sprintAction.Enable();
        _crouchAction.Enable();
        _pauseAction.Enable();
        _interactAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
        _jumpAction.Disable();
        _sprintAction.Disable();
        _crouchAction.Disable();
        _pauseAction.Disable();
        _interactAction.Disable();
    }
}
