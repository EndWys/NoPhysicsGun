using System;
using UnityEngine;

[RequireComponent(typeof(Bounc³ngPhysicalProjectile))]
public class GunProjectile : PoolingObject
{
    public event Action<GunProjectile> AfterHit;

    public override string ObjectName => "GunProjectile";

    private PhysicalProjectile _physicalProjectile;

    private void Awake()
    {
        _physicalProjectile = CachedTransform.GetComponent<PhysicalProjectile>();
    }

    public void Shoot(Vector3 direction, float power)
    {
        _physicalProjectile.Shoot(direction, power);
        _physicalProjectile.AfterHit += () => AfterHit?.Invoke(this);
    }

    public override void OnRelease()
    {
        base.OnRelease();
        AfterHit = null;
    }
}
