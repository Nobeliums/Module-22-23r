using UnityEngine;

public class HealEffect : IEffect
{
	private int _healAmount;
	private Transform _origin;
	private float _radius;
	private LayerMask _layerMask;

	public HealEffect(int amount,  Transform origin, float radius,  LayerMask layerMask)
	{
		_healAmount = amount;
		_origin = origin;
		_radius = radius;
		_layerMask = layerMask;
		
		Debug.Log(_radius);
	}

	public void Activate()
	{
		RaycastHit[] hits = Physics.SphereCastAll(
		    _origin.position,
		    _radius, 
		    _origin.forward,
		    0.0f,
		    _layerMask);

		foreach (RaycastHit hit in hits)
		{
			hit.collider.GetComponent<IHealable>()?.Heal(_healAmount);
		}
	}
}