using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputConnenctor : MonoBehaviour
{
    [SerializeField] LocalPlayerInputController _localPlayerInputController;

    private Dictionary<InputT, Action> _actionsMap = new Dictionary<InputT, Action>();

    public void ConnectSignal()
    {
        _localPlayerInputController.OnInputMove += DoAction;
    }

    public void ConnectAction(InputT inputT, Action action)
    {
        _actionsMap.Add(inputT, action);
    }

    void DoAction(InputT inputT)
    {
        _actionsMap[inputT].Invoke();
    }
}