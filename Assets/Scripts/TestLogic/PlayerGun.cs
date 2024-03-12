using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Projectile projectail;
    [SerializeField] float _gunPower;
    private Transform gunTransfrom;

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
         new Quaternion(Mathf.Clamp(transform.localRotation.x, 0.1f, 0.4f), transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }

    void Shoot()
    {
        projectail.transform.position = transform.position;

        projectail.Shoot(gunTransfrom.forward, _gunPower);
    }
}
