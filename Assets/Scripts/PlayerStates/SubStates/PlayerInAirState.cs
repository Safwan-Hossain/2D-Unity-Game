using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input Variables
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool dashInput;
    private bool primaryAttackInput;
    private bool secondaryAttackInput;

    //Check Variables
    private bool isGrounded;
    private bool isOnWall;
    private bool isOnWallBack;
    private bool wasOnWall;
    private bool wasOnWallBack;
    private bool isOnLedge;
    private bool ledgeIsValid;

    //Other Variables
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        wasOnWall = isOnWall;
        wasOnWallBack = isOnWallBack;

        isGrounded = core.CollisionSenses.isGrounded;
        isOnWall = core.CollisionSenses.isOnWall;
        isOnWallBack = core.CollisionSenses.isOnWallBack;
        isOnLedge = core.CollisionSenses.isOnLedge;

        if (isOnWall && !isOnLedge)
        {
            ledgeIsValid = core.CollisionSenses.isLedgeValid;
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if (!wallJumpCoyoteTime && !isOnWall && !isOnWallBack && (wasOnWall || wasOnWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        isOnWall = false;
        isOnWallBack = false;
        wasOnWall = false;
        wasOnWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        jumpInputStop = player.InputHandler.JumpInputStop;
        jumpInput = player.InputHandler.JumpInput;
        xInput = player.InputHandler.NormInputX;
        dashInput = player.InputHandler.DashInput;
        primaryAttackInput = player.InputHandler.AttackInputs[(int)AttackInput.PRIMARY];
        secondaryAttackInput = player.InputHandler.AttackInputs[(int)AttackInput.SECONDARY];

        CheckJumpMultiplier();

        if (primaryAttackInput)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (secondaryAttackInput)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        } 
        else if (isOnWall && !isOnLedge && ledgeIsValid)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && (isOnWall || isOnWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isOnWall = core.CollisionSenses.isOnWall;
            player.WallJumpState.determineWallJumpDirection(isOnWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isOnWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (dashInput && player.DashState.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            core.Movement.CheckIfFlip(xInput);
            core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
            player.Animator.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            player.Animator.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.jumpMultiplier);
                isJumping = false;
            }
            else if (core.Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseNumOfJumpsLeft();
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true;
    }

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
        
    }
    public void StopWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = false;
    }
    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }


    public void SetIsJumping()
    {
        isJumping = true;
    }
}
