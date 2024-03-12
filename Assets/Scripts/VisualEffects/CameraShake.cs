using System.Collections;
using System.Threading.Tasks;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private const float SHAKE_DISTANCE = 0.1f;

    [field: SerializeField] AnimationCurve _cameraShakeCurve;

    private Transform _cameraTr;
    private Task _callbackTask;

    private void Awake()
    {
        Camera cam = Camera.main;
        _cameraTr = cam.transform;
    }

    public void OnShoot(Task task)
    {
        _callbackTask = task;

        StopCoroutine(Shake());
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 startetPos = _cameraTr.localPosition;

        Vector3 shakeTarget = startetPos + (Vector3.left + Vector3.up) * SHAKE_DISTANCE;

        yield return StartCoroutine(Utils.MoveToTarget(_cameraTr.parent, shakeTarget, _cameraShakeCurve));

        yield return StartCoroutine(Utils.MoveToTarget(_cameraTr.parent, startetPos, _cameraShakeCurve));

        _callbackTask.RunSynchronously();
    }
}
