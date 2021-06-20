using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Default Values")]
    public float defaultDrag = 0f;

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

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 0.1f;

    [Header("Wall Jump State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashCooldown = 1f;
    public float dashHoldTime = 1f;
    public float dashTimeScale = 0.2f;
    public float dashTime = 0.2f;
    public float dashVelocity = 100f;
    public float dashDrag = 10f;
    public float dashYMultiplier = 0.3f;
    public float dashDistance = 10f;
       
}
