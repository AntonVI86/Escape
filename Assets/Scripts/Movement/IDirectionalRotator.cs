using UnityEngine;

public interface IDirectionalRotator : ITransformPosition
{
    Quaternion CurrentRotation { get; }
    void SetRotationDirection(Vector3 direction);
}
