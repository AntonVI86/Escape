using UnityEngine;

public class PlayerDetector
{
    private Transform _sourceTransform;

    private float _explosionRange = 2f;

    public float ExplosionRange => _explosionRange;

    public PlayerDetector(Transform source)
    {
        _sourceTransform = source;
    }

    public Collider[] GetDamageablesInExplosionRange()
    {
        Collider[] colliders = Physics.OverlapSphere(_sourceTransform.position, _explosionRange);

        if(colliders.Length > 0)
            return colliders;

        return null;
    }
}
