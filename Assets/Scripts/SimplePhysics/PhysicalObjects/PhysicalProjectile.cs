using System;
using UnityEngine;

public class PhysicalProjectile : BasePhysicalObject, IProjectile
{
    public event Action OnHit;

    public void Shoot(Vector3 direction, float power)
    {
        StartMoving(direction * power);
        StartFalling();
    }

    protected override void Hit(RaycastHit hitInfo)
    {
        StopMoving();
        StopFalling();
        OnHit.Invoke();
    }

}
