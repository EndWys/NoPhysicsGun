using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static IEnumerator MoveToTarget(Transform obj, Vector3 target)
    {
        if(obj == null) yield break;

        while(obj.localPosition != target) 
        { 
            obj.localPosition = Vector3.MoveTowards(obj.localPosition, target, Time.deltaTime);
            yield return null;
        }
    }

    public static IEnumerator MoveToTarget(Transform obj, Vector3 target, AnimationCurve curve)
    {
        if (obj == null) yield break;

        float time = 0;

        while (obj.localPosition != target)
        {
            time += Time.deltaTime;
            obj.localPosition = Vector3.MoveTowards(obj.localPosition, target, curve.Evaluate(time) * Time.deltaTime);
            yield return null;
        }
    }
}
