using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region State Variables
    public StateMachine StateMachine { get; private set; }

    [SerializeField]
    private SO_EnemyData enemyData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public CapsuleCollider2D EnemyCollider { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    #endregion

    #region Unity Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        StateMachine = new StateMachine();
    }
    private void Start()
    {
        Animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        EnemyCollider = GetComponent<CapsuleCollider2D>();
        Inventory = GetComponent<PlayerInventory>();

        //PrimaryAttackState.SetWeapon(Inventory.weapons[(int)AttackInput.PRIMARY]);
        //StateMachine.Initialize(IdleState);
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

    
}
