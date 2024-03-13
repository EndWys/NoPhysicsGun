using UnityEngine;

public class CharacterControll : CachedMonoBehaviour
{
    [SerializeField] PlayerInputConnenctor _inputConnector;

    [Header("Rotation Settings")]
    [SerializeField] float _rotationSpeed = 30;
    [Space]
    [Range(-1f, 1f)]
    [SerializeField] float _minRotation;
    [Range(-1f, 1f)]
    [SerializeField] float _maxRotation;

    private bool _rotationIsDirty = false;

    private void Awake()
    {
        ConnectInput();
    }

    private void Update()
    {
        TryToNormalizeCharacterRotation();
    }

    private void ConnectInput()
    {
        _inputConnector.ConnectSignal();

        _inputConnector.ConnectAction(InputT.RotateLeft, () => Rotate(Vector3.down));
        _inputConnector.ConnectAction(InputT.RotateRight, () => Rotate(Vector3.up));
    }

    private void Rotate(Vector3 direction)
    {
        CachedTransform.Rotate(direction, Time.deltaTime * _rotationSpeed);
        _rotationIsDirty = true;
    }

    private void TryToNormalizeCharacterRotation()
    {
        if (_rotationIsDirty)
        {
            Quaternion rotation = CachedTransform.localRotation;

            float clampedY = Mathf.Clamp(rotation.y, _minRotation, _maxRotation);

            CachedTransform.localRotation = new Quaternion(rotation.x, clampedY, rotation.z, rotation.w);
        }
    }
}
