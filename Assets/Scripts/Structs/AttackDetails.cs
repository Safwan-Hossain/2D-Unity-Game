using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackDetails
{
    public string attackName;
    public float movementSpeed;
    public float movementForceX;
    public float movementForceY;
    public float decelerationFactor;
    public float damageAmount;
}
