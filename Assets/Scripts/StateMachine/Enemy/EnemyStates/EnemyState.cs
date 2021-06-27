using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    protected Core core;
    protected Enemy enemy;
    protected SO_EnemyData enemyData;

    private string animatorBoolName;

    public EnemyState(Enemy enemy, StateMachine stateMachine, SO_EnemyData enemyData, string animatorBoolName) : base(stateMachine)
    {
        this.enemy = enemy;
        this.enemyData = enemyData;
        this.animatorBoolName = animatorBoolName;
        core = enemy.Core;
    }


    public override void Enter()
    {
        base.Enter();

        DoChecks();
        enemy.Animator.SetBool(animatorBoolName, true);
        Debug.Log("Current State entered:  " + animatorBoolName);
    }

    public override void Exit()
    {
        enemy.Animator.SetBool(animatorBoolName, false);

        base.Exit();
    }
}
