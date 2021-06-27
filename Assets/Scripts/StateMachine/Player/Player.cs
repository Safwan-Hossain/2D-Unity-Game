using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Varaibles
    public StateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public CapsuleCollider2D PlayerCollider { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    #endregion

    #region Other Variables

    private Vector2 workspace;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        StateMachine = new StateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
        Inventory = GetComponent<PlayerInventory>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int) AttackInput.PRIMARY]);
               
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = PlayerCollider.offset;
        workspace.Set(PlayerCollider.size.x, height);
        center.y += (height - PlayerCollider.size.y) / 2;

        PlayerCollider.offset = center;
        PlayerCollider.size = workspace;
    }


    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }
    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishedTrigger();
    }


    #endregion

}
