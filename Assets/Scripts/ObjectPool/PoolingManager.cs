using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [SerializeField] GameObject _projectail;
    [SerializeField] GameObject _hole;

    [Space]
    [SerializeField] Transform _projectailParent;

    public static PoolingManager Instance { get; private set; }

    private Pooling<GunProjectile> _projectiles = new Pooling<GunProjectile>();
    private Pooling<HoleObject> _holes = new Pooling<HoleObject>();

    private void Awake()
    {
        if(TryToInitialize())
        {
            InitializePools();
        }
    }

    private bool TryToInitialize()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return false;
        }

        Instance = this;
        return true;
    }

    private void InitializePools()
    {
        _projectiles.CreateMoreIfNeeded = true;
        _projectiles.Initialize(_projectail, _projectailParent);

        _holes.CreateMoreIfNeeded = true;
        _holes.Initialize(_hole, _projectailParent);
    }

    public GunProjectile CollectProjectile(Vector3 startPoint)
    {
        return _projectiles.Collect(_projectailParent, startPoint, false);
    }

    public void ReleaseProjectile(GunProjectile obj) {
        _projectiles.Release(obj);
    }

    public HoleObject CollectHole(Vector3 point, Vector3 normal)
    {
        return _holes.Collect(_projectailParent, point, false, Quaternion.LookRotation(-normal));
    }

    public void ReleaseHole(HoleObject obj)
    {
        _holes.Release(obj);
    }
}
