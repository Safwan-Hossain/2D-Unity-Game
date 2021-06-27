using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndClickWeapon : AggressiveWeapon
{
    [SerializeField] private float force;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform startPoint;

    private Quaternion rotation;
    private Vector2 direction;
    private Vector2 endPoint;

    public override void EnterWeapon()
    {
        base.EnterWeapon();

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        endPoint = (Vector2)startPoint.position + direction;
        direction.Normalize();

    }

    private void Update()
    {
        Debug.DrawLine(startPoint.position, endPoint, Color.black);
    }
    public override void AnimationActionTrigger()
    {
        shootProjectile(startPoint, direction);
    }

    private void shootProjectile(Transform startPoint, Vector2 direction)
    {
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        rotation = Quaternion.Euler(0f, 0f, angle);
        
        GameObject newProjectile = Instantiate(projectile, startPoint.position, rotation);
        Rigidbody2D RB = newProjectile.GetComponent<Rigidbody2D>();
        Vector2 forceVector = direction * force;

        RB.AddForce(forceVector, ForceMode2D.Impulse);
    }

}
