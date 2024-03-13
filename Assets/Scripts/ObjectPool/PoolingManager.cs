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

    public Pooling<GunProjectile> Projectiles = new Pooling<GunProjectile>();
    public Pooling<HoleObject> Holes = new Pooling<HoleObject>();

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
        Projectiles.CreateMoreIfNeeded = true;
        Projectiles.Initialize(_projectail, _projectailParent);

        Holes.CreateMoreIfNeeded = true;
        Holes.Initialize(_hole, _projectailParent);
    }

    public GunProjectile CollectProjectile(Vector3 startPoint)
    {
        return Projectiles.Collect(_projectailParent, startPoint, false);
    }

    public void ReleaseProjectile(GunProjectile obj) {
        Projectiles.Release(obj);
    }

    public HoleObject CollectHole(Vector3 point, Vector3 normal)
    {
        return Holes.Collect(_projectailParent, point, false, Quaternion.LookRotation(-normal));
    }

    public void ReleaseHole(HoleObject obj)
    {
        Holes.Release(obj);
    }
}
