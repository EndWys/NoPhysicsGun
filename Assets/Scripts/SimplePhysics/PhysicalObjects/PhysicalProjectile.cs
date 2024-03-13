using System;
using UnityEngine;

public class PhysicalProjectile : BasePhysicalObject
{
    public event Action AfterHit;

    public void Shoot(Vector3 direction, float power)
    {
        StartMoving(direction * power);
        StartFalling();
    }

    protected override void Hit(RaycastHit hitInfo)
    {
        StopMoving();
        StopFalling();
        AfterHit.Invoke();
    }

}
