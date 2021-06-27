using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    //Input Variables
    protected int xInput;
    protected int yInput;
    private bool jumpInput;
    private bool dashInput;
    private bool primaryAttackInput;
    private bool secondaryAttackInput;

    //Check Variables
    protected bool isTouchingCeiling;
    private bool isGrounded;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.isGrounded;
        isTouchingCeiling = core.CollisionSenses.isTouchingCeiling;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetNumOfJumpsLeft();
        player.DashState.ResetDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        primaryAttackInput = player.InputHandler.AttackInputs[(int)AttackInput.PRIMARY];
        secondaryAttackInput = player.InputHandler.AttackInputs[(int)AttackInput.SECONDARY];


        if (primaryAttackInput && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (secondaryAttackInput && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if (isGrounded && xInput == 0)
        {
            core.Movement.SetVelocityX(0);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
