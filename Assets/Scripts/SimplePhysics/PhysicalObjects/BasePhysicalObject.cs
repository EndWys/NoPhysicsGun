using UnityEngine;

public abstract class BasePhysicalObject : MonoBehaviour
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
        transform.position += _moveVector * Time.fixedDeltaTime;
    }

    private void Fall()
    {
        Vector3 gravityVector = Physics.gravity;

        float lastFrameFallingTime = FallingTime;

        FallingTime += Time.fixedDeltaTime;

        float accelerationDifference = (FallingTime * FallingTime) - (lastFrameFallingTime * lastFrameFallingTime);

        Vector3 gravity = gravityVector * accelerationDifference * 0.5f;

        transform.position += gravity;
    }

    private bool CheckForHit()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        
        if (Physics.Raycast(ray, out RaycastHit hit, transform.localScale.y / 1.8f))
        {
            return true;
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
