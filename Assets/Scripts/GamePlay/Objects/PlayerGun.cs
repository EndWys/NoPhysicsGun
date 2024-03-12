using UnityEngine;

public class PlayerGun : CachedMonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _projectail;
    [SerializeField] Transform _projectailParent;
    [SerializeField] TrajectoryDrawer _trajectoryDrawer;
    
    [Space]
    [SerializeField] float _gunPower;

    [Header("Rotation Settings")]
    [SerializeField] float _rotationSpeed = 30;
    [SerializeField] float _minRotation;
    [SerializeField] float _maxRotation;

    private Pooling<GunProjectile> _projectiles = new Pooling<GunProjectile>();

    private Vector3 _gunFroward => CachedTransform.forward;

    private bool _rotationIsDirty = false;

    private void Awake()
    {
        _projectiles.CreateMoreIfNeeded = true;
        _projectiles.Initialize(_projectail, _projectailParent);
    }

    private void Update()
    {
        GunInput();

        TryToNormalizeCharacterRotation();

        ShowTrajectory();
    }

    private void GunInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.W))
        {
            CachedTransform.Rotate(Vector3.left, Time.deltaTime * _rotationSpeed);
            _rotationIsDirty = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            CachedTransform.Rotate(Vector3.right, Time.deltaTime * _rotationSpeed);
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

    private void Shoot()
    {
        GunProjectile projectile = _projectiles.Collect(_projectailParent,CachedTransform.position,false);
        projectile.Shoot(_gunFroward, _gunPower);
        projectile.OnHit += (GunProjectile p) => { _projectiles.Release(p); };
    }

    private void ShowTrajectory()
    {
        _trajectoryDrawer.ShowTrajectory(CachedTransform.position, _gunFroward * _gunPower);
    }
}
