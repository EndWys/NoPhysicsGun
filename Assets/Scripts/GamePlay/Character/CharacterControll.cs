using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 30);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down, Time.deltaTime * 30);
        }

        transform.localRotation =
         new Quaternion(transform.localRotation.x, Mathf.Clamp(transform.localRotation.y, -0.7f, 0.3f), transform.localRotation.z, transform.localRotation.w);
    }
}
