using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnWallState : PlayerState
{
    protected bool jumpInput;
    protected bool isGrounded;
    protected bool isOnWall;
    protected bool isOnLedge;
    protected bool ledgeIsValid;
    protected int xInput;

    public PlayerOnWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.isGrounded;
        isOnWall = core.CollisionSenses.isOnWall;
        isOnLedge = core.CollisionSenses.isOnLedge;

        if (isOnWall && !isOnLedge)
        {
            ledgeIsValid = core.CollisionSenses.isLedgeValid;
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumpInput = player.InputHandler.JumpInput;
        xInput = player.InputHandler.NormInputX;

        if (jumpInput)
        {
            player.WallJumpState.determineWallJumpDirection(isOnWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isOnWall || xInput == -core.Movement.FacingDirection)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isOnWall && !isOnLedge && ledgeIsValid)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
