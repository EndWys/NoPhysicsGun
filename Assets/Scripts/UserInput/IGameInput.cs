using System;

public enum InputT
{
    RotateLeft,
    RotateRight,
    MoveUp,
    MoveDown,
    PowerUp,
    PowerDown,
    Shoot,
}

public interface IGameInput
{
    public Action<InputT> OnInput { get; set; }

    public void Initialize();
}