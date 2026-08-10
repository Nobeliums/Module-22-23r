using System;
using System.Linq;
using UnityEngine;

public class ExplosionTimerTrap : ContactInteractable
{
	[SerializeField] private int _damage;
	[SerializeField] private float _timeToActivate;
	[SerializeField] private LayerMask _layerMasks;
	[SerializeField] private ParticleSystem _explosionParticle;

	private MeshRenderer _mesh;

	private float _currentTimer;
	private bool _timerStarted;

	private float ExplosionRadius => GetComponent<SphereCollider>().radius * transform.lossyScale.x;

	public override void Initialize()
	{
		base.Initialize();

		_effects.Add(new ExplosionEffect(transform, _damage, ExplosionRadius, _layerMasks));
		_mesh = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		if (_isEnabled == false)
			return;

		if (_timerStarted)
			_currentTimer += Time.deltaTime;
		
		if  (_currentTimer >= _timeToActivate)
			Trigger();
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Произошел триггер");

		IDamagable damagable = other.GetComponent<IDamagable>();

		if (damagable != null && _isEnabled)
		{
			StartTimer();
		}
	}

	protected override void Trigger()
	{
		Debug.Log("Запущен Trigger");

		foreach (IEffect effect in _effects)
		{
			effect.Activate();
		}

		_mesh.enabled = false;
		Disable();
		_explosionParticle.Play();
	}


	private void StartTimer()
	{
		Debug.Log("Запущен таймер");
		_timerStarted = true;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, ExplosionRadius);
	}
}