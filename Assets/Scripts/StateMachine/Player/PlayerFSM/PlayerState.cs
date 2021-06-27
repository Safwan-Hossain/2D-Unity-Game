using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core core;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string animatorBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animatorBoolName = animatorBoolName;
        core = player.Core;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animatorBoolName, true);
        startTime = Time.time;
        Debug.Log("Current State entered:  " + animatorBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animatorBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishedTrigger()
    {
        isAnimationFinished = true;
    }
}
