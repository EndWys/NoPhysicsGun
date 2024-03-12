using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterControll : CachedMonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float _rotationSpeed = 30;
    [SerializeField] float _minRotation;
    [SerializeField] float _maxRotation;

    private bool _rotationIsDirty = false;

    private void Update()
    {
        CharacterInput();

        TryToNormalizeCharacterRotation();
    }

    private void CharacterInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            CachedTransform.Rotate(Vector3.up, Time.deltaTime * _rotationSpeed);
            _rotationIsDirty = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            CachedTransform.Rotate(Vector3.down, Time.deltaTime * _rotationSpeed);
            _rotationIsDirty = true;
        }
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
