using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BasePhysicalObject
{
    public void Shoot(Vector3 direction, float power)
    {
        StartMoving(direction * power);
        StartFalling();
    }

    protected override void OnHit()
    {
        StopMoving();
        StopFalling();
    }

}
