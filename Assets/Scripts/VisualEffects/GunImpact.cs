using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GunImpact : CachedMonoBehaviour
{
    private const float SHAKE_DISTANCE = 0.15f;

    [SerializeField] Transform _shakingPart;
    [field: SerializeField] AnimationCurve _cameraShakeCurve;

    private Task _callbackTask;

    public void OnShoot(Task task)
    {
        _callbackTask = task;

        StopCoroutine(Shake());
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 startetPos = _shakingPart.localPosition;

        Vector3 shakeTarget = startetPos - _shakingPart.forward * SHAKE_DISTANCE;

        yield return StartCoroutine(Utils.MoveToTarget(_shakingPart, shakeTarget, _cameraShakeCurve));

        yield return StartCoroutine(Utils.MoveToTarget(_shakingPart, startetPos, _cameraShakeCurve));

        _callbackTask.RunSynchronously();
    }
}
