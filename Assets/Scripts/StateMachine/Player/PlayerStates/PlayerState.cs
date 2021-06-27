using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    protected Core core;
    protected Player player;
    protected PlayerData playerData;

    private string animatorBoolName;

    public PlayerState(Player player, StateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(stateMachine)
    {
        this.player = player;
        this.playerData = playerData;
        this.animatorBoolName = animatorBoolName;
        core = player.Core;
    }


    public override void Enter()
    {
        base.Enter();

        DoChecks();
        player.Animator.SetBool(animatorBoolName, true);
        Debug.Log("Current State entered:  " + animatorBoolName);
    }

    public override void Exit()
    {
        player.Animator.SetBool(animatorBoolName, false);

        base.Exit();
    }
}
