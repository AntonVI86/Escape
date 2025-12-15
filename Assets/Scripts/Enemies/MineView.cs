using UnityEngine;

public class MineView : MonoBehaviour
{
    [SerializeField] private AudioClip _explosionSfx;

    [SerializeField] private ParticleSystem _explosionVfx;
    [SerializeField] private ParticleSystem _burningVfx;

    [SerializeField] private Mine _mine;

    private void OnEnable()
    {
        _mine.Activated += OnActivated;
        _mine.Detonated += OnDetonated;
    }

    private void OnActivated()
    {
        _burningVfx.Play();
    }

    private void OnDetonated()
    {
        ParticleSystem explosion = Instantiate(_explosionVfx, transform.position, Quaternion.identity);
        explosion.Play();
        AudioPlayer.Instance.PlaySound(_explosionSfx);
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor == true)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _mine.ExplosionRange);
    }
}
