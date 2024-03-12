using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    Transform gunTransfrom;
    [SerializeField] ProjectailTest projectail;
    

    // Start is called before the first frame update
    void Start()
    {
        gunTransfrom = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.W))
        {
            gunTransfrom.Rotate(Vector3.left,Time.deltaTime * 30);
        }

        if (Input.GetKey(KeyCode.S))
        {
            gunTransfrom.Rotate(Vector3.right, Time.deltaTime * 30);
        }

        gunTransfrom.localRotation =
         new Quaternion(Mathf.Clamp(transform.rotation.x, 0.1f, 0.4f), transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    void Shoot()
    {
        projectail.transform.position = transform.position;

        projectail.Move(gunTransfrom.forward);
    }
}
