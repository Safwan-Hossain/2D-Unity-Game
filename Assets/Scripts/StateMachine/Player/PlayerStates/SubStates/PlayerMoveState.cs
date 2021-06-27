﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        core.Movement.CheckIfFlip(xInput);
        core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

        if (isExitingState) { return; }

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (yInput == -1)
        {
            stateMachine.ChangeState(player.CrouchMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
