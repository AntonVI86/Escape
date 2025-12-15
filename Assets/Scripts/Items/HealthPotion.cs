using UnityEngine;

public class HealthPotion : Item
{
    [SerializeField] private float _healValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealable healable))
        {
            AudioPlayer.Instance.PlaySound(Sfx);
            healable.Heal(_healValue);
            Destroy(gameObject);
        }
    }
}
