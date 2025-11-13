using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private Weapon equippedWeapon;
    public PlayerControls.OnFootActions onFoot;

    private PlayerMotor motor;
    public PlayerLook look;

    private Action<InputAction.CallbackContext> holdStartHandler;
    private Action<InputAction.CallbackContext> holdCancelHandler;
    private Action<InputAction.CallbackContext> altHoldStartHandler;
    private Action<InputAction.CallbackContext> altHoldCancelHandler;


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

    public void EquipWeapon(Weapon wpn)
    {
        UnbindWeaponInputs();

        equippedWeapon = wpn;

        holdStartHandler        = ctx => { if (equippedWeapon) equippedWeapon.HoldAttackStart(); };
        holdCancelHandler       = ctx => { if (equippedWeapon) equippedWeapon.HoldAttackStop(); };
        altHoldStartHandler     = ctx => { if (equippedWeapon) equippedWeapon.AltHoldAttackStart(); };
        altHoldCancelHandler    = ctx => { if (equippedWeapon) equippedWeapon.AltHoldAttackStop(); };
 
        onFoot.Attack.started       += holdStartHandler;
        onFoot.Attack.canceled      += holdCancelHandler;
        onFoot.AltFire.started      += altHoldStartHandler;    
        onFoot.AltFire.canceled     += altHoldCancelHandler;
    }

    public void UnequipWeapon()
    {
        UnbindWeaponInputs();
        equippedWeapon = null;
    }

    private void UnbindWeaponInputs()
    {
        if (holdStartHandler != null)       onFoot.Attack.started   -= holdStartHandler;
        if (holdCancelHandler != null)      onFoot.Attack.canceled -= holdCancelHandler;
        if (altHoldStartHandler != null)    onFoot.AltFire.started   -= altHoldStartHandler;
        if (altHoldCancelHandler != null)   onFoot.AltFire.canceled  -= altHoldCancelHandler;

        holdStartHandler = null;
        holdCancelHandler = null;
        altHoldStartHandler = null;
        altHoldCancelHandler = null;
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
