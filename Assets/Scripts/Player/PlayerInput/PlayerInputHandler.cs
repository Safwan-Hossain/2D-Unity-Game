using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera mainCamera;
    
    public Vector2 RawMovementVector { get; private set; }
    public Vector2 RawDashDirectionVector{ get; private set; }
    //public Vector2Int DashDirectionVector { get; private set; } ************************
    public Vector2 DashDirectionVector { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool[] AttackInputs { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float dashInputStartTime;


    private void Start()
    {
        int count = Enum.GetValues(typeof(AttackInput)).Length;
        AttackInputs = new bool[count];

        playerInput = GetComponent<PlayerInput>();
        mainCamera = Camera.main;
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementVector = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementVector * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementVector * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInputStop = false;
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInputStop = false;
            DashInput = true;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionVector = context.ReadValue<Vector2>();
        RawDashDirectionVector = mainCamera.ScreenToWorldPoint((Vector3) RawDashDirectionVector) - transform.position;
        DashDirectionVector = Vector2Int.RoundToInt(RawDashDirectionVector.normalized);
        DashDirectionVector = RawDashDirectionVector; // ***************************** delete this
    }
    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int) AttackInput.PRIMARY] = true;
        }
        else if (context.canceled)
        {
            AttackInputs[(int)AttackInput.PRIMARY] = false;
        }
    }
    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)AttackInput.SECONDARY] = true;
        }
        else if (context.canceled)
        {
            AttackInputs[(int)AttackInput.SECONDARY] = false;
        }
    }
    public void UseJump()
    {
        JumpInput = false;
    }
    public void UseDash()
    {
        DashInput = false;
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time > jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time > dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}

public enum AttackInput {
    PRIMARY = 0,
    SECONDARY = 1
}

