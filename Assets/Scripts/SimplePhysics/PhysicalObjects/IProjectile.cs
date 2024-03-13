using System;
using UnityEngine;

public interface IProjectile
{
    public event Action<RaycastHit> OnHit;

    public void Shoot(Vector3 direction, float power);
}
