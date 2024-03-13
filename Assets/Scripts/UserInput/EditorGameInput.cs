using System;
using UnityEngine;

public class EditorGameInput : CachedMonoBehaviour, IGameInput
{
    public Action<InputT> OnInput { get; set; }

    bool _chenckForButtons = false;

    public void Initialize()
    {
        _chenckForButtons = true;
    }

    private void Update()
    {
        if (!_chenckForButtons)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnInput?.Invoke(InputT.Shoot);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            OnInput?.Invoke(InputT.PowerUp);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            OnInput?.Invoke(InputT.PowerDown);
        }

        if (Input.GetKey(KeyCode.A))
        {
            OnInput?.Invoke(InputT.RotateLeft);
        }

        if (Input.GetKey(KeyCode.D))
        {
            OnInput?.Invoke(InputT.RotateRight);
        }

        if (Input.GetKey(KeyCode.W))
        {
            OnInput?.Invoke(InputT.MoveUp);
        }

        if (Input.GetKey(KeyCode.S))
        {
            OnInput?.Invoke(InputT.MoveDown);
        }
    }
}
