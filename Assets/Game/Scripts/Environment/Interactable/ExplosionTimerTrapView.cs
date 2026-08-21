using System;
using UnityEngine;

public class ExplosionTimerTrapView : MonoBehaviour
{
	[SerializeField] private Material _activateEffect;
	
	private ExplosionTimerTrap _exploder;
	
	private bool _isEffectActivated;

	private void Awake()
	{
		_exploder = GetComponent<ExplosionTimerTrap>();
		_isEffectActivated = false;
	}

	private void Update()
	{
		if (_exploder.IsActivated && !_isEffectActivated)
		{
			ChangeMaterials();
			_isEffectActivated = true;
		}
	}

	private void ChangeMaterials()
	{
		Renderer renderer =  GetComponent<Renderer>();
		Material[] currentMaterials = renderer.materials;
		Material[] newMaterials = new  Material[currentMaterials.Length + 1];
			
		for (int i = 0; i < currentMaterials.Length; i++)
		{
			newMaterials[i] = currentMaterials[i];
		}
			
		newMaterials[newMaterials.Length - 1] = _activateEffect;
			
		renderer.materials = newMaterials;
	}
}