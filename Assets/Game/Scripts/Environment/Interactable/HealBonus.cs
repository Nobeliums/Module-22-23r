using System;
using UnityEngine;

public class HealBonus : ContactInteractable
{
	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private int _healAmount;

	public override void Initialize()
	{
		base.Initialize();
		
		float colliderRadius =  gameObject.AddComponent<SphereCollider>().radius;
		float radius = transform.lossyScale.x * colliderRadius;
		
		_effects.Add(new HealEffect(_healAmount, transform, radius, _layerMask));
	}

	protected override void Trigger()
	{
		Debug.Log("HealBonus Trigger");
		
		foreach (var effect in _effects)
		{
			effect.Activate();
		}
		
		Disable();
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		IHealable healable = other.GetComponent<IHealable>();

		if (healable != null)
		{
			Trigger();
		}
	}
}