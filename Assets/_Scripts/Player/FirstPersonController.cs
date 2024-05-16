using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{

    [Header("Movement Variables")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float sprintMultipler = 2f;

    private float _currentSpeed { get; set; }

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private bool _canJump { get; set; }
    [Tooltip("Time delay between jumps.")]
    [SerializeField] private float _jumpDelay = 5.0f;

    private float _gravity = -9.81f;

    private Vector3 currentMovement;

    // Coroutines
    private Coroutine JumpDelayCoroutine;

    // Component References
    private CharacterController characterController { get; set; }
    private InputManager inputManager { get; set; }

    private void Awake()
    {
        if(characterController == null)
        {
            bool hasCharacterController = GetComponent<CharacterController>() != null;
            if(hasCharacterController)
            {
                characterController = GetComponent<CharacterController>();
            }
            else
            {
                Debug.LogWarning($"Character Controller component doesn't exist on target object {this.gameObject.name}");
            }
        }

        if(inputManager == null)
        {
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJumps();
    }

    private void HandleMovement()
    {
        float _speed = 0;
        bool grounded = GetComponent<CharacterController>().isGrounded == true;

        if(!grounded && inputManager._SprintValue > 0)
        {
            _speed = defaultSpeed;
        }
        else
        {
            _speed = defaultSpeed * (inputManager._SprintValue > 0 ? sprintMultipler : 1f);
        }

        Vector3 iDir = new Vector3(inputManager._MoveInput.x, 0f, inputManager._MoveInput.y);
        Vector3 wDir = transform.TransformDirection(iDir);
        wDir.Normalize();

        currentMovement.x = wDir.x * _speed;
        currentMovement.z = wDir.z * _speed;

        HandleJumps();

        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HandleJumps()
    {
        if(characterController.isGrounded)
        {
            if (_canJump && JumpDelayCoroutine != null) JumpDelayCoroutine = null;

            currentMovement.y = -0.5f;
            if (inputManager.jumpTriggered && _canJump) currentMovement.y = _jumpForce;
        }
        else
        {
            currentMovement.y += _gravity * Time.deltaTime;
            _canJump = false;
            if (JumpDelayCoroutine != null) return;
            else { JumpDelayCoroutine = StartCoroutine(DelayJump()); }
        }
    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSecondsRealtime(_jumpDelay);
        _canJump = true;
    }
}
