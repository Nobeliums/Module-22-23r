using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDirectionRotateble, IDamagable, IHealable
{
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private int _maxHealth;
	[SerializeField] private int _startingHealth;

	private DirectionRotator _rotator;
	private Health _health;

	[field: SerializeField] public NavMeshAgent Agent { get; private set; }

	public Quaternion CurrentRotation => transform.rotation;
	public Health Health => _health;
	public bool IsDeath => _health.Value <= 0;

	private void Awake()
	{
		_rotator = new DirectionRotator(_rotationSpeed, transform);
		_health = new Health(_startingHealth, _maxHealth);
	}

	private void Update()
	{
		_rotator.Update(Time.deltaTime);
	}

	public void SetRotationDirection(Vector3 direction) => _rotator.MoveDirection = direction;

	public void TakeDamage(int amount)
	{
		if (amount <= 0)
		{
			Debug.LogError($"{nameof(amount)} в методе {nameof(TakeDamage)} должен быть >= 0");
			return;
		}

		_health.Value -= amount;
		
		Debug.Log($"Нанесен урон по {gameObject.name}, нанесено {amount} урона");
	}

	public void Heal(int amount)
	{
		if (amount <= 0)
		{
			Debug.LogError($"{nameof(amount)} в методе {nameof(Heal)} должен быть >= 0");
			return;
		}
		
		_health.Value += amount;
		
		Debug.Log($"Исцеление на {amount} у {gameObject.name}");
	}
}
