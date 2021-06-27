using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    
    private int xInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        isAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;

        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (shouldCheckFlip)
        {
            core.Movement.CheckIfFlip(xInput);
        }

        if (setVelocity)
        {
            core.Movement.SetVelocityX(velocityToSet * core.Movement.FacingDirection);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;

        weapon.InitializeWeapon(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        core.Movement.SetVelocityX(velocity * core.Movement.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }

    public void ApplyImpulse(float forceX, float forceY)
    {
        //core.Movement.ApplyImpulseX(force * core.Movement.FacingDirection);
        core.Movement.ApplyImpulse(forceX * core.Movement.FacingDirection, forceY);
    }

    public void SetFlipCheck(bool boolean)
    {
        shouldCheckFlip = boolean; 
    }

    public void SetVelocityCheck(bool boolean)
    {
        setVelocity = boolean;
    }
}
