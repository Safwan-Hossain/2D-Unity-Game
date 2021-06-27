using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_WeaponData
{
    [SerializeField] private AttackDetails[] attackDetails;

    public AttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }
    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;
        movementSpeed = new float[amountOfAttacks];
        movementForceX = new float[amountOfAttacks];
        movementForceY = new float[amountOfAttacks];
        decelerationFactor = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
            movementForceX[i] = attackDetails[i].movementForceX;
            movementForceY[i] = attackDetails[i].movementForceY;
            decelerationFactor[i] = attackDetails[i].decelerationFactor;
        }
    }
}

