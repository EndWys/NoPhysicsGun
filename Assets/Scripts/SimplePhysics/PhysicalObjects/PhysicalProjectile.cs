using System;
using UnityEngine;

public class PhysicalProjectile : BasePhysicalObject
{
    public event Action OnHit;

    public void Shoot(Vector3 direction, float power)
    {
        StartMoving(direction * power);
        StartFalling();
    }

    protected override void Hit()
    {
        StopMoving();
        StopFalling();
        OnHit?.Invoke();
    }

}
