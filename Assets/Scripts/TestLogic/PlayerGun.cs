using UnityEngine;

public class PlayerGun : CachedMonoBehaviour
{
    [Header("References")]
    [SerializeField] Projectile _projectail;
    
    [Space]
    [SerializeField] float _gunPower;

    [Header("Rotation Settings")]
    [SerializeField] float _rotationSpeed = 30;
    [SerializeField] float _minRotation;
    [SerializeField] float _maxRotation;

    private bool _rotationIsDirty = false;

    private void Update()
    {
        GunInput();

        TryToNormalizeCharacterRotation();
    }

    private void GunInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.W))
        {
            CachedTransform.Rotate(Vector3.left, Time.deltaTime * 30);
            _rotationIsDirty = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            CachedTransform.Rotate(Vector3.right, Time.deltaTime * 30);
            _rotationIsDirty = true;
        }
    }
    private void TryToNormalizeCharacterRotation()
    {
        if (_rotationIsDirty)
        {
            Quaternion rotation = CachedTransform.localRotation;

            float clampedX = Mathf.Clamp(rotation.x, _minRotation, _maxRotation);

            CachedTransform.localRotation = new Quaternion(clampedX, rotation.y, rotation.z, rotation.w);
        }
    }


    void Shoot()
    {
        _projectail.CachedTransform.position = CachedTransform.position;

        _projectail.Shoot(CachedTransform.forward, _gunPower);
    }
}
