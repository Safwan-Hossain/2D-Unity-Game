using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0f);
        if (core.CollisionSenses.isTouchingCeiling)
        {
            stateMachine.ChangeState(player.CrouchIdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExitingState) { return; }

        if (xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (yInput == -1)
        {
            stateMachine.ChangeState(player.CrouchIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
