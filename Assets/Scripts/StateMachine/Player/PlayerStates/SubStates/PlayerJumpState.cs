using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int numOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
        numOfJumpsLeft = playerData.numberOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJump();
        core.Movement.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        numOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        return (numOfJumpsLeft > 0);
    }

    public void ResetNumOfJumpsLeft()
    {
        numOfJumpsLeft = playerData.numberOfJumps;
    }

    public void DecreaseNumOfJumpsLeft()
    {
        numOfJumpsLeft--;
    }
}
