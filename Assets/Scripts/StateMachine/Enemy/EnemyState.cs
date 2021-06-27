using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Core core;
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected SO_EnemyData enemyData;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string animatorBoolName;

    public EnemyState(Enemy enemy, SO_EnemyData enemyData, string animatorBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = enemy.StateMachine;
        this.enemyData = enemyData;
        this.animatorBoolName = animatorBoolName;
        core = enemy.Core;
    }

    public virtual void Enter()
    {
        DoChecks();
        enemy.Animator.SetBool(animatorBoolName, true);
        startTime = Time.time;
        Debug.Log("Current State entered:  " + animatorBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        enemy.Animator.SetBool(animatorBoolName, false);
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
