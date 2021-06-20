using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Variables


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform ledgeCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float playerHeight;
    [SerializeField] private float ledgeCheckDistance;

    [SerializeField] private LayerMask whatIsGround;
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform CeilingCheck { get => ceilingCheck; private set => ceilingCheck = value; }
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public float PlayerHeight { get => playerHeight; set => playerHeight = value; }
    public float LedgeCheckDistance { get => ledgeCheckDistance; set => ledgeCheckDistance = value; }

    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    #endregion

    #region Check Functions

    public bool isOnWall
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool isOnWallBack
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool isGrounded
    {
        //Debug.Log(Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround));
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool isTouchingCeiling
    {
        get => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool isOnLedge
    {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, ledgeCheckDistance, whatIsGround);
    }
    public bool isLedgeValid
    {
        get => !Physics2D.Raycast(ledgeCheck.position, Vector2.down, playerHeight, whatIsGround);
    }

    #endregion
}
