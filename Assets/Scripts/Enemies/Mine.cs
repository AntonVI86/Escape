using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionVfx;
    [SerializeField] private ParticleSystem _burningVfx;

    private PlayerDetector _detector;

    private float _timeToExplosion = 1f;
    private float _damage = 30f;

    private void Awake()
    {
        _detector = new PlayerDetector(transform);
    }

    private void Update()
    {
        if (IsDamageablesInRange())
        {
            _burningVfx.Play();
            CountDownToExplosion();            
        }
        else
        {
            _burningVfx.Stop();
        }
    }

    private void CountDownToExplosion()
    {
        _timeToExplosion -= Time.deltaTime;

        if(_timeToExplosion <= 0)
        {
            ProcessExplosion();
            Instantiate(_explosionVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void ProcessExplosion()
    {
        foreach (Collider collider in _detector.GetDamageablesInExplosionRange())
        {
            if (collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }
    }

    private bool IsDamageablesInRange()
    {
        List<IDamageable> damageables = new List<IDamageable>();

        foreach (Collider collider in _detector.GetDamageablesInExplosionRange())
        {
            if (collider.TryGetComponent(out IDamageable damageable))
                damageables.Add(damageable);
        }

        return damageables.Count > 0;
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor == true)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detector.ExplosionRange);
    }
}
