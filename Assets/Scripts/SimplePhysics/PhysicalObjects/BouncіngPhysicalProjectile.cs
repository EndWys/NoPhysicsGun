using UnityEngine;

public class BouncіngPhysicalProjectile : PhysicalProjectile
{
    [Range(1,30)]
    [SerializeField] int _bouncePower = 15;
    [Range(1,10)]
    [SerializeField] int _bounceCount = 1;

    private int _causedBounceCount = 0;
    private int _posibleBounceCount => _bounceCount;

    private void Bounce(Vector3 direction)
    {
        _causedBounceCount++;

        StartMoving(direction * _bouncePower);
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
