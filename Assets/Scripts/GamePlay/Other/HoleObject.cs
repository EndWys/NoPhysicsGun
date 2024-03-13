using System.Collections;
using UnityEngine;

public class HoleObject : PoolingObject
{
    private const float DELAY_BEFORE_DISAPEAR = 3F;
    public override string ObjectName => "Hole";

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
