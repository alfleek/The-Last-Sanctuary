using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public bool canLook = true;

    public float mouseXSens = 1f, mouseYSens = 1f;
    public float padXSens = 1f, padYSens = 1f;
                   
    private string currentControlScheme = "Keyboard&Mouse";

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void SetControlScheme(string scheme)
    {
        currentControlScheme = scheme;
    }

    public void SetLookEnabled(bool enabled)
    {
        canLook = enabled;

        if (!enabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        if (!canLook || cam == null) return;

        //Apply sensitivity per control scheme
        var isPad = currentControlScheme == "Gamepad";
        float xSens = isPad ? padXSens : mouseXSens;
        float ySens = isPad ? padYSens : mouseYSens;

        //Only apply deltaTime to sticks, but not to mouse delta
        float dt = isPad ? Time.deltaTime : 1f;

        xRotation -= (input.y * dt) * ySens;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (input.x * dt) * xSens);
    }
}
