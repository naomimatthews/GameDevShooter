using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement movement;
    private PlayerCamera playerCamera;

    [SerializeField] Guns gun;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        movement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();

        onFoot.Jump.performed += ctx => movement.Jump();

     //   onFoot.Shoot.performed += _ => gun.Shoot();
    }

    private void FixedUpdate()
    {
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerCamera.ProcessLook(onFoot.PlayerLook.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
