using System;
using UnityEngine;

public class PlayerGun : CachedMonoBehaviour
{
    [Header("References")]
    [SerializeReference] SimpleAnimator _gunAnimator;
    [SerializeField] TrajectoryDrawer _trajectoryDrawer;

    [Header("Power Settings")]

    [Range(1, 10)]
    [SerializeField] private float _powerChnagingSpeed;

    [Space]
    [Range(1f, 9f)]
    [SerializeField] private float _minPower;
    [Range(10f, 30f)]
    [SerializeField] private float _maxPower;

    [Header("Rotation Settings")]
    [SerializeField] float _rotationSpeed = 30;

    [Space]
    [Range(-1f,1f)]
    [SerializeField] float _minRotation;
    [Range(-1f, 1f)]
    [SerializeField] float _maxRotation;

    public event Action<float> OnPowerChange;

    private event Action OnShot;

    private Vector3 _gunFroward => CachedTransform.forward;

    private float _gunPower = 8;

    private bool _rotationIsDirty = false;

    public float MinPower => _minPower;
    public float MaxPower => _maxPower;

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ChnagePower(+Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            ChnagePower(-Time.deltaTime);
        }


        if (_animationInProcess) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.W))
        {
            Rotate(Vector3.left);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Rotate(Vector3.right);
        }
    }

    private void ChnagePower(float value)
    {
        _gunPower = Mathf.Clamp(_gunPower + value * _powerChnagingSpeed, _minPower, _maxPower);
        OnPowerChange?.Invoke(_gunPower);
    }

    private void Rotate(Vector3 direction)
    {
        CachedTransform.Rotate(direction, Time.deltaTime * _rotationSpeed);
        _rotationIsDirty = true;
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
