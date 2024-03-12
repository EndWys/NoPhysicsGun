using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private const float SHAKE_DISTANCE = 0.1f;

    [field: SerializeField] AnimationCurve _cameraShakeCurve;

    private Transform _cameraTr;

    private void Awake()
    {
        Camera cam = Camera.main;
        _cameraTr = cam.transform;
    }

    public void OnShoot()
    {
        StopCoroutine(Shake());
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 startetPos = _cameraTr.position;

        Vector3 shakeTarget = startetPos + (Vector3.left + Vector3.up) * SHAKE_DISTANCE;

        yield return StartCoroutine(Utils.MoveToTarget(_cameraTr.parent, shakeTarget, _cameraShakeCurve));

        yield return StartCoroutine(Utils.MoveToTarget(_cameraTr.parent, startetPos, _cameraShakeCurve));
    }
}
