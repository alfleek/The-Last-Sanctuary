using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private PlayerControls.OnFootActions onFoot;

    private PlayerMotor motor;
    public PlayerLook look;


    void Awake()
    {
        playerControls = new PlayerControls();
        onFoot = playerControls.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        playerInput = GetComponent<PlayerInput>();
        playerInput.onControlsChanged += OnControlsChanged;

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
    }

    private void OnControlsChanged(PlayerInput obj)
    {
        look.SetControlScheme(obj.currentControlScheme);
    }

    void Update()
    {
        motor.ProcessMove(onFoot.Move.ReadValue<Vector2>());
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
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
