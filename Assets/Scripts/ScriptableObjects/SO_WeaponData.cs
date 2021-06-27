using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    public int amountOfAttacks { get; protected set; }
    public float[] movementSpeed { get; protected set; }
    public float[] movementForceX { get; protected set; }
    public float[] movementForceY { get; protected set; }
    public float[] decelerationFactor { get; protected set; }

}
