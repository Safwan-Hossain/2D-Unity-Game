using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{

    public PlayerCrouchIdleState(Player player, StateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Animator.SetBool("standUp", false);
        core.Movement.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
        
        
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0)
        {
            stateMachine.ChangeState(player.CrouchMoveState);
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            player.Animator.SetBool("standUp", true);
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
