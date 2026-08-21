using System;
using System.Collections;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
	private const string TakeDamageLayerName = "TakeDamage Layer";
	private const string EdgeDisolveParamName = "_Edge";

	private readonly int _isRunningHash = Animator.StringToHash("IsRunning");
	private readonly int _healthPercentHash =  Animator.StringToHash("HealthPercent");
	private readonly int _takeDamageHash = Animator.StringToHash("TakeDamage");
	private readonly int _deathHash = Animator.StringToHash("Death");
	private readonly int _isJumpingHash = Animator.StringToHash("IsJumping");
	
	[SerializeField] private Character _character;
	[SerializeField] private Animator _animator;
	[SerializeField] private GameObject _moveDirectionFlag;
	
	[SerializeField] private Transform _rootTransform;

	[SerializeField] private float _disolveDuration;

	private float _previousHealthPercent;
	private bool _isEnabled;
	private Coroutine _deathRoutine;

	private bool IsRunning => _character.IsMoving;
	private bool IsJumping => _character.InJumpProcess;
	private float HealthPercent => (float)_character.Health.Value / _character.Health.MaxValue;

	private void Start()
	{
		_previousHealthPercent = HealthPercent;
		_animator.SetFloat(_healthPercentHash, HealthPercent);
		_isEnabled = true;
	}

	private void Update()
	{
		if (_isEnabled == false)
			return;

		if (_previousHealthPercent != HealthPercent)
		{
			_animator.SetFloat(_healthPercentHash, HealthPercent);
		}

		SetMoveState();

		if (_previousHealthPercent > HealthPercent)
		{
			TakeDamageState();
		}

		if (_character.IsDeath)
			PlayDeath();

		_previousHealthPercent = HealthPercent;

		SetMoveDirectionFlagTarget();

		if (_animator.GetBool(_isJumpingHash) != IsJumping)
			_animator.SetBool(_isJumpingHash, IsJumping);

		Debug.Log($"HP: {_character.Health.Value}, {HealthPercent}, {_previousHealthPercent}");
	}

	private void SetMoveState()
	{
		if (_animator.GetBool(_isRunningHash) != IsRunning)
			_animator.SetBool(_isRunningHash, IsRunning);
	}

	private void PlayDeath()
	{
		_animator.SetTrigger(_deathHash);
		_isEnabled = false;
	}

	private void TakeDamageState()
	{
		var layerIndex = _animator.GetLayerIndex(TakeDamageLayerName);

		_animator.SetLayerWeight(layerIndex, 1.0f);
		_animator.Play(_takeDamageHash, layerIndex, 0f);
	}

	public void StartDeathDesolveRoutine()
	{
		if (_deathRoutine != null)
			return;

		_deathRoutine = StartCoroutine(DeathDisolve());
	}

	private IEnumerator DeathDisolve()
	{
		float currentDuration = 0.0f;
		float edge = currentDuration / _disolveDuration;
		
		Debug.Log(edge);

		while (currentDuration < _disolveDuration)
		{
			Renderer[] renders = _character.GetComponentsInChildren<Renderer>();

			foreach (Renderer render in renders)
			{
				render.material.SetFloat(EdgeDisolveParamName, edge);
			}
			
			currentDuration += Time.deltaTime;
			
			edge = currentDuration / _disolveDuration;
			
			Debug.Log(edge);
			
			yield return null;
		}

		_deathRoutine = null;
	}
	
	private void SetMoveDirectionFlagTarget()
	{
		float rootY = Math.Abs(_rootTransform.transform.localPosition.y);
		
		_moveDirectionFlag.transform.position = new Vector3(_character.CurrentDestinaction.x,
			rootY,
			_character.CurrentDestinaction.z);
		
		_moveDirectionFlag.SetActive(IsRunning);
	}
}