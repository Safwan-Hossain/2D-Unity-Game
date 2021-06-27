using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, StateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJump();
        player.JumpState.ResetNumOfJumpsLeft();
        core.Movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        core.Movement.CheckIfFlip(wallJumpDirection);
        player.JumpState.DecreaseNumOfJumpsLeft();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //player.Animator.SetBool("inAir", true);
        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void determineWallJumpDirection(bool isOnWall)
    {
        if (isOnWall)
        {
            wallJumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = core.Movement.FacingDirection;
        }
    }
}
