using System;
using UnityEngine;
using UnityEngine.UI;

public class GunPowerSlider : MonoBehaviour
{
    [SerializeField] PlayerGun _gun;
    [SerializeField] Slider _slider;
    [SerializeField] Text _power;

    public event Action<float> PowerChanged;

    private void Awake()
    {
        _slider.minValue = _gun.MinPower;
        _slider.maxValue = _gun.MaxPower;

        OnPowerChange(_gun.MinPower);

        _gun.OnPowerChange += OnPowerChange;
    }

    private void OnPowerChange(float power)
    {
        _slider.value = power;
        _power.text = ((int)power).ToString();
    }
}
