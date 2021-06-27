using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPosition;
    private Vector2 ledgePosition;
    private Vector2 startPosition;
    private Vector2 stopPosition;
    private Vector2 tempStopPosition;
    private Vector2 workspace;

    private bool isHanging;
    private bool isClimbing;
    private bool isTouchingCeiling;
    private bool jumpInput;
    private int xInput;
    private int yInput;
    public PlayerLedgeClimbState(Player player, StateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        player.Animator.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityZero();
        player.transform.position = detectedPosition;
        ledgePosition = DetermineLedgePosition();

        startPosition.Set(ledgePosition.x - (core.Movement.FacingDirection * playerData.startOffset.x), ledgePosition.y - playerData.startOffset.y);
        stopPosition = DetermineStopPosition(ledgePosition);
        player.transform.position = startPosition;

    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPosition;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isTouchingCeiling)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            } 
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
            return;
        }

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;

        core.Movement.SetVelocityZero();
        player.transform.position = startPosition;
        if (isClimbing || isHanging)
        {
            DetermineLedgePosition();
        }
        if (xInput == core.Movement.FacingDirection && isHanging && !isClimbing)
        {
            CheckForSpace();
            isClimbing = true;
            player.Animator.SetBool("climbLedge", true);
        }
        else if (yInput == -1 && isHanging && !isClimbing)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (jumpInput && !isClimbing)
        {
            player.WallJumpState.determineWallJumpDirection(true);
            stateMachine.ChangeState(player.WallJumpState);
        }
    }

    public void SetDetectedPosition(Vector2 position)
    {
        detectedPosition = position;
    }
    public void CheckForSpace()
    {
        float padding = 0.015f;
        isTouchingCeiling = Physics2D.Raycast(ledgePosition + (Vector2.up * padding) + (Vector2.right * core.Movement.FacingDirection * 0.015f), Vector2.up, playerData.standColliderHeight, core.CollisionSenses.WhatIsGround);
    }

    public Vector2 DetermineLedgePosition()
    {
        Vector3 wallCheckPosition = core.CollisionSenses.WallCheck.position;
        Vector3 ledgeCheckPosition = core.CollisionSenses.LedgeCheck.position;

        float xPadding = 0.08f;
        float yPadding = 0.05f;
        float facingDirection = core.Movement.FacingDirection;
        // sets the cast distance so that it doesn't check below the wallCheck position
        float yCastDistance = ledgeCheckPosition.y - wallCheckPosition.y + yPadding;

        RaycastHit2D xHit = Physics2D.Raycast(wallCheckPosition, Vector2.right * facingDirection, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.WhatIsGround);
        float xDistance = xHit.distance;
        workspace.Set((xDistance + xPadding) * facingDirection, yPadding);

        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheckPosition + (Vector3)workspace, Vector2.down, yCastDistance, core.CollisionSenses.WhatIsGround);
        float yDistance = yHit.distance - yPadding;
        workspace.Set(wallCheckPosition.x + xDistance * facingDirection, ledgeCheckPosition.y - yDistance);

        return workspace;
    }

    public Vector2 DetermineStopPosition(Vector2 ledgePosition)
    {
        float yCastDistance = 1f;
        float yPadding = 0.1f;

        tempStopPosition.Set(ledgePosition.x + (core.Movement.FacingDirection * playerData.stopOffset.x), ledgePosition.y + yCastDistance);
        RaycastHit2D yHit = Physics2D.Raycast(tempStopPosition, Vector2.down, 2 * yCastDistance, core.CollisionSenses.WhatIsGround);
        workspace.Set(tempStopPosition.x, tempStopPosition.y - yHit.distance + yPadding);

        return workspace;
    }
}
