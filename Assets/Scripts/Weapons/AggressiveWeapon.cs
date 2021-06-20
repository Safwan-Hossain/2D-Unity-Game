using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;
     
    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData) weaponData;
        }
        else
        {
            Debug.Log("No aggressive weapon data/ wrong data for weapon");
        }
    }
    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckForDamageables();
    }

    private void CheckForDamageables()
    {
        AttackDetails attackDetails = aggressiveWeaponData.AttackDetails[attackCounter];
        foreach (IDamageable item in detectedDamageables)
        {
            item.Damage(attackDetails.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageables.Add(damageable);
        }

    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageables.Remove(damageable);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddToDetected(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveFromDetected(collision);
    }
}

