using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectailTest : MonoBehaviour
{
    public float forwardSpeed = 5f; // Adjust the forward speed as needed
    public float gravity = 9.8f;    // Adjust the gravity as needed
    private Transform projectileTransform;

    private Vector3 _shootDirection;

    private Vector3 _originPos;

    float time = 0;

    bool stop = false;

    void Start()
    {
        projectileTransform = transform;
        Stop();
    }


    void Update()
    {
        MoveProjectile();
        Raycast();
    }

    void Stop()
    {
        time = 0;
        stop = true;
    }

    public void Move(Vector3 shootDirection)
    {
        time = 0;
        stop = false;
        _shootDirection = shootDirection;
        _originPos = projectileTransform.position;
    }

    void MoveProjectile()
    {
        if (stop) return;

        Vector3 manually = Vector3.down * gravity;
        
        time += Time.deltaTime;

        Vector3 shoorDirection = _shootDirection * forwardSpeed * time;
        Vector3 GravityDirection = manually * time * time * 0.5f;

        projectileTransform.position = _originPos + shoorDirection + GravityDirection;
    }

    void Raycast()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(transform.position, -transform.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 0.11f))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            Stop();
        }
    }
}

