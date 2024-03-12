using System.Collections.Generic;
using UnityEngine;

public abstract class BasePhysicalObject : CachedMonoBehaviour
{
    private bool IsMoving = false;
    private bool IsFalling = false;

    private float FallingTime = 0f;
    private Vector3 _moveVector;


    private void FixedUpdate()
    {
        if (IsMoving)
        {
            Move();
        }

        if (IsFalling)
        {
            Fall();
        }

        if (CheckForHit())
        {
            OnHit();
        }
    }

    private void Move()
    {
        CachedTransform.position += _moveVector * Time.fixedDeltaTime;
    }

    private void Fall()
    {
        Vector3 gravityVector = Physics.gravity;

        float lastFrameFallingTime = FallingTime;

        FallingTime += Time.fixedDeltaTime;

        float accelerationDifference = (FallingTime * FallingTime) - (lastFrameFallingTime * lastFrameFallingTime);

        Vector3 gravity = gravityVector * accelerationDifference * 0.5f;

        CachedTransform.position += gravity;
    }

    private bool CheckForHit()
    {
        List <Ray> hitRays = new() {
            new Ray(CachedTransform.position, CachedTransform.up),
            new Ray(CachedTransform.position, -CachedTransform.up),
            new Ray(CachedTransform.position, CachedTransform.right),
            new Ray(CachedTransform.position, -CachedTransform.right),
            new Ray(CachedTransform.position, CachedTransform.forward),
            new Ray(CachedTransform.position, -CachedTransform.forward),
        };

        
        foreach (var ray in hitRays)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, CachedTransform.localScale.x))
            {
                return true;
            }
        }

        return false;
    }

    protected abstract void OnHit();

    public void StartFalling()
    {
        IsFalling = true;
    }

    public void StopFalling()
    {
        IsFalling = false;
        FallingTime = 0f;
    }

    public void StartMoving(Vector3 moveVector)
    {
        IsMoving = true;
        _moveVector = moveVector;
    }

    public void StopMoving()
    {
        IsMoving = false;
    }
}
