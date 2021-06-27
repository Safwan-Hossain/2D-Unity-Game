using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D RB;

    private Animator animator;
    private List<IDamageable> detectedDamageables = new List<IDamageable>();

    private Vector3 lastPosition;
    private Vector3 lastVelocity;

    private bool canPenetrate;
    private bool inAir;

    private float lastAngularVelocity;
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canPenetrate = false;
        inAir = true;
    }
    private void Update()
    {
        if (inAir)
        {
            // makes projectile point in same direction as its velocity
            float travelDirection = Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(travelDirection, Vector3.forward);
        }
    }
    private void FixedUpdate()
    {
        lastPosition = transform.position;
        lastVelocity = RB.velocity;
        lastAngularVelocity = RB.angularVelocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(10f);
        }
        canPenetrate = true;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canPenetrate)
        {
            RB.velocity = Vector2.zero;
            RB.isKinematic = true;
            inAir = false;

            transform.parent = collision.transform;
            animator.SetBool("inAir", inAir);
        }
        else
        {
            transform.position = lastPosition;
            RB.velocity = lastVelocity;
            RB.angularVelocity = lastAngularVelocity;
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }
}
