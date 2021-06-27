using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class SO_EnemyData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Crouch States")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 1.128f;
    public float standColliderHeight = 2.125f;

    [Header("Jump State")]
    public float jumpVelocity = 14f;
    public int numberOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float jumpMultiplier = 0.5f;
}
