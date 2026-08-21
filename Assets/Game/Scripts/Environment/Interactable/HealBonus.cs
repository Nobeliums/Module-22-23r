using System;
using UnityEngine;

public class HealBonus : MonoBehaviour
{
	[SerializeField] private int _healAmount;

	private void OnTriggerEnter(Collider other)
	{
		IHealable healable = other.GetComponent<IHealable>();

		if (healable != null)
		{
			healable.Heal(_healAmount);
			Destroy(gameObject);
		}
	}
}