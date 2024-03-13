using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePhysicalObject : CachedMonoBehaviour
{
    private const float RAY_SIZE_MULTIPLIER = 1.5f;

    private bool _isMoving = false;
    private bool _isFalling = false;

    private float _fallingTime = 0f;
    private Vector3 _moveVector;

    private bool _isStatic => !_isMoving && !_isFalling;

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

        if (!_isStatic)
        {

            TryToHit();
        }
    }

    private void Move()
    {
        Vector3 move = _moveVector * Time.fixedDeltaTime;
        CachedTransform.Translate(move);
    }

    private void Fall()
    {
        Vector3 gravityVector = Physics.gravity;

        float lastFrameFallingTime = _fallingTime;

        _fallingTime += Time.fixedDeltaTime;

        float accelerationDifference = (_fallingTime * _fallingTime) - (lastFrameFallingTime * lastFrameFallingTime);

        Vector3 gravity = gravityVector * accelerationDifference * 0.5f;

        CachedTransform.Translate(gravity);
    }

    private void TryToHit()
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
            if (Physics.Raycast(ray, out RaycastHit hit, CachedTransform.localScale.x * RAY_SIZE_MULTIPLIER))
            {
                Hit(hit);
                return;
            }
        }
    }

    protected abstract void Hit(RaycastHit hitInfo);

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
        _moveVector = Vector3.zero;
    }
}
