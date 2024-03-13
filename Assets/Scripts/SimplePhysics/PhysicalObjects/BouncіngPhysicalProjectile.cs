using UnityEngine;

public class BouncіngPhysicalProjectile : PhysicalProjectile
{
    private int _posibleBounceCount = 1;
    private int _causedBounceCount = 0;

    private void Bounce(Vector3 direction)
    {
        _causedBounceCount++;

        StartMoving(direction * 15);
    }

    protected override void Hit(RaycastHit hitInfo)
    {
        if (_causedBounceCount < _posibleBounceCount)
        {
            Bounce(hitInfo.normal);
            return;
        }

        _causedBounceCount = 0;
        base.Hit(hitInfo);
    }
}
