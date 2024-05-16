using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CameraLockState
{
    None,
    Locked
}

public class FirstPersonCamera : MonoBehaviour
{
    public static FirstPersonCamera Instance {  get; private set; }

    [SerializeField] private Camera _mainCamera;
    private InputManager inputManager;
    private PlayerSettings playerSettings;

    private float maxAngleClamp = 90;

    private float verticalRotation;

    private CameraLockState _cameraLockState;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _mainCamera = Camera.main.GetComponent<Camera>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        playerSettings = GameObject.Find("PlayerSettings").GetComponent<PlayerSettings>();

        _cameraLockState = CameraLockState.None;
    }

    private void Update()
    {
        if(_cameraLockState == CameraLockState.Locked)
        {
            return;
        }

        HandleRotation();
    }

    public CameraLockState GetCameraLockState()
    {
        return _cameraLockState;
    }

    public void SetCameraLockState(CameraLockState newState)
    {
        _cameraLockState = newState;
    }

    private void HandleRotation()
    {
        float xRot = inputManager._LookInput.x * playerSettings._horizontalSensitivity;
        transform.Rotate(0, xRot, 0);

        verticalRotation -= inputManager._LookInput.y * playerSettings._verticalSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxAngleClamp, maxAngleClamp);
        _mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    public void EnableInput(bool isEnabled)
    {
        if (!isEnabled)
        {
            SetCameraLockState(CameraLockState.Locked);
        }
        else if(_cameraLockState == CameraLockState.Locked)
        {
            SetCameraLockState(CameraLockState.None);
        }
    }
}
