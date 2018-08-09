public interface IDamageable {
    void TakeDamage(float damage);
    void Slow(bool slow, float duration, float strength);
    void Burn(bool burn, float duration, float strength, float damage);
}
