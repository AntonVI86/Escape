using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionVfx;
    [SerializeField] private ParticleSystem _burningVfx;

    private AgentCharacter _character;
    private PlayerDetector _detector;

    private float _radiusDetected = 1f;

    private float _timeToExplosion = 1f;
    private float _damage = 30f;

    private void Awake()
    {
        _character = GetComponentInParent<CharacterGetter>().Agent;
        _detector = new PlayerDetector(transform, _character.transform);
    }

    private void Update()
    {
        if (_detector.IsCharacterInExplosionRange())
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusDetected);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusDetected);
    }
}
