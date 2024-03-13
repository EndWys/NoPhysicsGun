using System.Collections;
using UnityEngine;

public class HoleObject : PoolingObject
{
    [SerializeField] ParticleSystem _particleSystem;

    private const float DELAY_BEFORE_DISAPEAR = 3F;
    public override string ObjectName => "Hole";

    private void OnEnable()
    {
        _particleSystem.Emit(20);
    }

    public override void OnCollect()
    {
        base.OnCollect();

        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(DELAY_BEFORE_DISAPEAR);

        PoolingManager.Instance.ReleaseHole(this);
    }
}
