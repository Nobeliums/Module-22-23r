public interface IDamagable
{
	Health Health { get; }

	void TakeDamage(int amount);
}