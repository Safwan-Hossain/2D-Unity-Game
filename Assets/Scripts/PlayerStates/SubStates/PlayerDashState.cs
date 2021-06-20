using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDash();

        isHolding = true;
        dashDirection = Vector2.right * core.Movement.FacingDirection;

        Time.timeScale = playerData.dashTimeScale;
        Time.fixedDeltaTime = playerData.dashTimeScale * 0.02f;
        startTime = Time.unscaledTime;

        player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if(core.Movement.CurrentVelocity.y > 0f)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.dashYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dashInputStop = player.InputHandler.DashInputStop;

        if (isExitingState)
        {
            return;
        }

        player.Animator.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
        player.Animator.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
        if (isHolding)
        {
            dashDirectionInput = player.InputHandler.DashDirectionVector;

            if (dashDirectionInput != Vector2.zero)
            {
                dashDirection = dashDirectionInput;
                dashDirection.Normalize();
            }

            float dashAngle = Vector2.SignedAngle(Vector2.left, dashDirection);
            player.DashDirectionIndicator.rotation = Quaternion.Euler(0, 0, dashAngle);

            if (dashInputStop || Time.unscaledTime >= startTime + playerData.dashHoldTime)
            {
                isHolding = false;
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                startTime = Time.time;
                core.Movement.CheckIfFlip(Mathf.RoundToInt(dashDirection.x));
                player.RB.drag = playerData.dashDrag;
                core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
                player.DashDirectionIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
            if (Time.time >= startTime + playerData.dashTime)
            {
                player.RB.drag = playerData.defaultDrag;
                isAbilityDone = true;
                lastDashTime = Time.time;
            }
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetDash() => CanDash = true;
}
