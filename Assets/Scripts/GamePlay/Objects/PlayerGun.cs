using System;
using UnityEngine;

public class PlayerGun : CachedMonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _projectail;
    [SerializeField] GameObject _hole;
    [SerializeField] Transform _projectailParent;

    [Space]
    [SerializeReference] SimpleAnimator _gunAnimator;
    [SerializeField] TrajectoryDrawer _trajectoryDrawer;
    
    [Space]
    [SerializeField] float _gunPower;

    [Header("Rotation Settings")]
    [SerializeField] float _rotationSpeed = 30;
    [Space]
    [Range(-1f,1f)]
    [SerializeField] float _minRotation;
    [Range(-1f, 1f)]
    [SerializeField] float _maxRotation;

    private Action OnShot;

    private Pooling<GunProjectile> _projectiles = new Pooling<GunProjectile>();
    private Pooling<HoleObject> _holes = new Pooling<HoleObject>();

    private Vector3 _gunFroward => CachedTransform.forward;

    private bool _rotationIsDirty = false;

    private bool _animationInProcess => _gunAnimator.AnimationInProcess;

    private void Awake()
    {
        _projectiles.CreateMoreIfNeeded = true;
        _projectiles.Initialize(_projectail, _projectailParent);

        _holes.CreateMoreIfNeeded = true;
        _holes.Initialize(_hole, _projectailParent);

        OnShot += CallShotAnimation;
    }

    private void Update()
    {
        GunInput();

        TryToNormalizeCharacterRotation();

        ShowTrajectory();
    }

    private void GunInput()
    {
        if (_animationInProcess) return;

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

        projectile.OnHit += OnBulletHit;

        OnShot.Invoke();
    }

    private void OnBulletHit(GunProjectile p, RaycastHit hit)
    {
        _projectiles.Release(p);
        SpawnHole(hit);
    }

    private void SpawnHole(RaycastHit hit)
    {
        _holes.Collect(_projectailParent, hit.point, false, Quaternion.LookRotation(-hit.normal));
    }

    private void CallShotAnimation()
    {
        _gunAnimator.CallAnimation();
    }

    private void ShowTrajectory()
    {
        _trajectoryDrawer.ShowTrajectory(CachedTransform.position, _gunFroward * _gunPower);
    }
}
