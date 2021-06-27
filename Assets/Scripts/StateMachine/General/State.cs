using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected StateMachine stateMachine;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;


    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;

    }

    public virtual void Exit()
    {
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
