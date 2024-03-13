using System.Threading.Tasks;
using UnityEngine;

public class GunShootAnimation : SimpleAnimator
{
    [SerializeField] ShakeAnimation _gunImpact;
    [SerializeField] ShakeAnimation _cameraShake;

    public override void CallAnimation()
    {
        CallShootAnimation();
    }

    public async void CallShootAnimation()
    {
        AnimationInProcess = true;

        Task cameraShakeCallback = new Task(() => { });
        Task gunShakeCallback = new Task(() => { });

        _cameraShake.StartAnimation(cameraShakeCallback);
        _gunImpact.StartAnimation(gunShakeCallback);

        await Task.WhenAll(new Task[] { cameraShakeCallback, gunShakeCallback });

        AnimationInProcess = false;
    }
}
