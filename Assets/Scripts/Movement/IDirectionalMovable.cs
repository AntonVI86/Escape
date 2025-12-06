using UnityEngine;

public interface IDirectionalMovable : ITransformPosition
{
    Vector3 CurrentDirection { get; }
    void SetMoveDirection(Vector3 direction);
}
