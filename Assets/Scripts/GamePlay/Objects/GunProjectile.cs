using System;
using UnityEngine;

[RequireComponent(typeof(Bounc³ngPhysicalProjectile))]
public class GunProjectile : PoolingObject
{
    public event Action<GunProjectile, RaycastHit> OnHit;

    public override string ObjectName => "GunProjectile";

    private IProjectile _physicalProjectile;

    private void Awake()
    {
        _physicalProjectile = CachedTransform.GetComponent<IProjectile>();
    }

    public void Shoot(Vector3 direction, float power)
    {
        _physicalProjectile.Shoot(direction, power);
        _physicalProjectile.OnHit += (RaycastHit hitInfo) => OnHit?.Invoke(this, hitInfo);
    }

    public override void OnRelease()
    {
        base.OnRelease();
        OnHit = null;
    }
}
