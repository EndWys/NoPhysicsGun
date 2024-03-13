using UnityEngine;

public abstract class SimpleAnimator : MonoBehaviour
{
    public bool AnimationInProcess { get; protected set; }
    public abstract void CallAnimation();
}
