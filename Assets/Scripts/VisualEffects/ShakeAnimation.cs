using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    [SerializeField] float _distance;
    [SerializeField] Vector3 _direction;
    [SerializeField] Transform _target;

    [field: SerializeField] AnimationCurve _cameraShakeCurve;

    private Task _callbackTask;

    public void StartAnimation(Task task)
    {
        _callbackTask = task;

        StartCoroutine(Shake(_target, _cameraShakeCurve));
    }

    private IEnumerator Shake(Transform obj, AnimationCurve curve)
    {
        Vector3 startetPos = obj.localPosition;

        Vector3 shakeTarget = startetPos - _direction * _distance;

        yield return StartCoroutine(Utils.MoveToTarget(obj, shakeTarget, curve));

        yield return StartCoroutine(Utils.MoveToTarget(obj, startetPos, curve));

        _callbackTask.RunSynchronously();
    }
}
