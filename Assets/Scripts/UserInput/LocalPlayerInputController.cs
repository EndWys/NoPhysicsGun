using System;
using UnityEngine;

public class LocalPlayerInputController : MonoBehaviour
{
    [SerializeField] EditorGameInput _editorGameInput;

    public Action<InputT> OnInputMove;

    private IGameInput _inputController;
    public void Awake()
    {
        SetInputController();
        ConnectSignals();
    }

    public void SetInputController()
    {
        _inputController = _editorGameInput;

        _inputController.Initialize();

    }

    private void ConnectSignals()
    {
        _inputController.OnInput += DoInput;
    }

    private void DoInput(InputT type)
    {
        OnInputMove.Invoke(type);
    }
}
