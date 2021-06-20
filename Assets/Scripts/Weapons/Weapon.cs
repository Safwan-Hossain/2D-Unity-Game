using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected Animator weaponAnimator;
    [SerializeField]
    protected SO_WeaponData weaponData;

    protected PlayerAttackState attackState;
    protected int attackCounter;

    protected virtual void Awake()
    {
        weaponAnimator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState attackState)
    {
        this.attackState = attackState;
    }

    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishedTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        attackState.SetPlayerVelocity(0f);
    }
    public virtual void AnimationTurnOnFlipTrigger()
    {
        attackState.SetFlipCheck(true);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        attackState.SetFlipCheck(false);
    }

    public virtual void AnimationActionTrigger() {
    }
    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }

        weaponAnimator.SetBool("attack", true);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        weaponAnimator.SetBool("attack", false);
        attackCounter++;
        gameObject.SetActive(false);
    }
}
