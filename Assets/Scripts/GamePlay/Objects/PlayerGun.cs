using System;
using UnityEngine;

public class PlayerGun : CachedMonoBehaviour
{
    [Header("References")]
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

    private Vector3 _gunFroward => CachedTransform.forward;

    private bool _rotationIsDirty = false;

    private bool _animationInProcess => _gunAnimator.AnimationInProcess;

    private void Awake()
    {
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
        GunProjectile projectile = PoolingManager.Instance.CollectProjectile(CachedTransform.position);
        projectile.Shoot(_gunFroward, _gunPower);
        OnShot.Invoke();
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
