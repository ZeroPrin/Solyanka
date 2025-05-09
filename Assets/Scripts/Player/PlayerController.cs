﻿using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController _charController;
    [SerializeField] private Animator _animator;

    [Header("Params")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _target;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _verticalAngleLimit = 80f;
    [SerializeField] private float _sphereRadius = 10f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    private float _currentVerticalAngle = 0f;
    private float _currentHorizontalAngle = 0f;
    private Vector3 _velocity;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            if (_cameraTransform != null) _cameraTransform.gameObject.SetActive(false);
            enabled = false;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleCameraRotation();
        HandleBodyRotation();
        UpdateTargetPosition();
        HandleMovement();
        HandleJump();
    }

    private void HandleCameraRotation()
    {
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _currentVerticalAngle -= mouseY;
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, -_verticalAngleLimit, _verticalAngleLimit);
        _cameraTransform.localRotation = Quaternion.Euler(_currentVerticalAngle, 0f, 0f);
    }

    private void HandleBodyRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        _currentHorizontalAngle += mouseX;
        transform.rotation = Quaternion.Euler(0f, _currentHorizontalAngle, 0f);
    }

    private void UpdateTargetPosition()
    {
        _target.position = _cameraTransform.position + _cameraTransform.forward * _sphereRadius;
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float speed = move.magnitude;
        _animator.SetFloat("Speed", speed);

        _charController.Move(move * moveSpeed * Time.deltaTime);

        if (_charController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -0.5f;
        }
        else
        {
            _velocity.y += gravity * Time.deltaTime;
        }

        _charController.Move(_velocity * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _charController.isGrounded)
        {
            _animator.SetTrigger("JumpTrigger");
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}