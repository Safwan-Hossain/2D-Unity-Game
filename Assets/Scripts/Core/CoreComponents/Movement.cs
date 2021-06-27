using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }

    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; } 

    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Functions
    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(velocity * angle.x * direction, velocity * angle.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void ApplyImpulse(float forceX, float forceY)
    {
        workspace.Set(forceX, forceY);
        RB.AddForce(workspace, ForceMode2D.Impulse);
    }

    public void ApplyImpulseX(float force)
    {
        workspace.Set(force, 0);
        RB.AddForce(workspace, ForceMode2D.Impulse);
    }

    public void ApplyImpulseY(float force)
    {
        workspace.Set(0, force);
        RB.AddForce(workspace, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        core.Movement.FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void CheckIfFlip(int xInput)
    {
        if (xInput != 0 && xInput != core.Movement.FacingDirection)
        {
            Flip();
        }
    }

    #endregion
}
