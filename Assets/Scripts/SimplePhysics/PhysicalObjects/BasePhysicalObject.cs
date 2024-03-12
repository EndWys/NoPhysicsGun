using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePhysicalObject : CachedMonoBehaviour
{
    public event Action AfterHit;

    private bool _isMoving = false;
    private bool _isFalling = false;

    private float _fallingTime = 0f;
    private Vector3 _moveVector;

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move();
        }

        if (_isFalling)
        {
            Fall();
        }

        if (CheckForHit())
        {
            Hit();
            AfterHit?.Invoke();
        }
    }

    private void Move()
    {
        CachedTransform.position += _moveVector * Time.fixedDeltaTime;
    }

    private void Fall()
    {
        Vector3 gravityVector = Physics.gravity;

        float lastFrameFallingTime = _fallingTime;

        _fallingTime += Time.fixedDeltaTime;

        float accelerationDifference = (_fallingTime * _fallingTime) - (lastFrameFallingTime * lastFrameFallingTime);

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

    protected abstract void Hit();

    public void StartFalling()
    {
        _isFalling = true;
    }

    public void StopFalling()
    {
        _isFalling = false;
        _fallingTime = 0f;
    }

    public void StartMoving(Vector3 moveVector)
    {
        _isMoving = true;
        _moveVector = moveVector;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }
}
