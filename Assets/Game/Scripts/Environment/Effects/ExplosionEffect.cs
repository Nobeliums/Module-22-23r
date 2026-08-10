using UnityEngine;

public class ExplosionEffect : IEffect
{
	private Transform _exploder;
	private int _damage;
	private float _radius;
	private LayerMask _layer;

	public ExplosionEffect(Transform exploder, int damage,  float radius, LayerMask layer)
	{
		_exploder = exploder;
		_damage = damage;
		_radius = radius;
		_layer = layer;
	}
	
	public void Activate()
	{
		RaycastHit[] hits = Physics.SphereCastAll(
			_exploder.position,
			_radius, 
			_exploder.forward,
			0.0f, 
			_layer);

		foreach (RaycastHit hit in hits)
		{
			hit.transform.GetComponent<IDamagable>()?.TakeDamage(_damage);
		}
	}
}