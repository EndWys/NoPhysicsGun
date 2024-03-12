using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static IEnumerator MoveToTarget(Transform obj, Vector3 target)
    {
        if(obj == null) yield break;

        while(obj.position != target) 
        { 
            obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime);
            yield return null;
        }
    }

    public static IEnumerator MoveToTarget(Transform obj, Vector3 target, AnimationCurve curve)
    {
        if (obj == null) yield break;

        float time = 0;

        while (obj.position != target)
        {
            time += Time.deltaTime;
            obj.position = Vector3.MoveTowards(obj.position, target, curve.Evaluate(time) * Time.deltaTime);
            yield return null;
        }
    }
}
