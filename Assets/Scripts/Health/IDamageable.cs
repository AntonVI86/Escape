public interface IDamageable
{
    bool IsAlive { get; }
    bool IsInjured { get; }
    void TakeDamage(float damage);
}
