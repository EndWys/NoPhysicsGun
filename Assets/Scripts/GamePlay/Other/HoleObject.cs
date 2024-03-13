using System.Collections;
using UnityEngine;

public class HoleObject : PoolingObject
{
    public override string ObjectName => "Hole";

    public override void OnCollect()
    {
        base.OnCollect();

        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        PoolingManager.Instance.ReleaseHole(this);
    }
}
