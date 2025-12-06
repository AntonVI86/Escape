using UnityEngine;

public class PlayerDetector
{
    private Transform _sourceTransform;
    private Transform _characterTransform;

    private float _explosionRange = 2f;

    public PlayerDetector(Transform source, Transform character)
    {
        _sourceTransform = source;
        _characterTransform = character;
    }

    public bool IsCharacterInExplosionRange()
    {
        if ((_characterTransform.position - _sourceTransform.position).magnitude < _explosionRange)
            return true;

        return false;
    }
}
