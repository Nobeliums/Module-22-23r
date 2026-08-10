using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContactInteractable : MonoBehaviour
{
	protected bool _isEnabled;
	protected List<IEffect> _effects;

	public virtual void Enable() => _isEnabled = true;
	public virtual void Disable() => _isEnabled = false;

	private void Awake()
	{
		Initialize();
	}

	public virtual void Initialize()
	{
		_effects = new List<IEffect>();

		Enable();
	}

	protected abstract void Trigger();
}