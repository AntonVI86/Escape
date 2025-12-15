using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public event Action Detonated;
    public event Action Activated;

    private PlayerDetector _detector;
    private Coroutine _activateProcess;

    private float _timeToExplosion = 1f;
    private float _damage = 30f;

    public float ExplosionRange => _detector.ExplosionRange;

    private void Awake()
    {
        _detector = new PlayerDetector(transform);
    }

    private void Update()
    {
        if (IsDamageablesInRange())
        {
            if(_activateProcess == null)
            {
                _activateProcess = StartCoroutine(CountDownToExplosion());
                Activated?.Invoke();
            }
        }
    }

    private void ProcessExplosion()
    {
        foreach (Collider collider in _detector.GetDamageablesInExplosionRange())
        {
            if (collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }

        Detonated?.Invoke();
        Destroy(gameObject);
    }

    public bool IsDamageablesInRange()
    {
        List<IDamageable> damageables = new List<IDamageable>();

        foreach (Collider collider in _detector.GetDamageablesInExplosionRange())
        {
            if (collider.TryGetComponent(out IDamageable damageable))
                damageables.Add(damageable);
        }

        return damageables.Count > 0;
    }

    private IEnumerator CountDownToExplosion()
    {
        while (_timeToExplosion > 0)
        {
            _timeToExplosion -= Time.deltaTime;
            yield return null;
        }

        ProcessExplosion();
    }
}
