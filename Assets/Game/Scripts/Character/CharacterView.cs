using UnityEngine;

public class CharacterView : MonoBehaviour
{
	private const string TakeDamageLayerName = "TakeDamage Layer";

	private readonly int _isRunningHash = Animator.StringToHash("IsRunning");
	private readonly int _healthPercentHash =  Animator.StringToHash("HealthPercent");
	private readonly int _takeDamageHash = Animator.StringToHash("TakeDamage");
	private readonly int _deathHash = Animator.StringToHash("Death");
	
	[SerializeField] private Character _character;
	[SerializeField] private Animator _animator;
	[SerializeField] private GameObject _moveDirectionFlag;

	private float _previousHealthPercent;
	private bool _isEnabled;

	private bool IsRunning => _character.IsMoving;
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

	private void SetMoveDirectionFlagTarget()
	{
		_moveDirectionFlag.transform.position = new Vector3(_character.CurrentDestinaction.x,
			_character.transform.position.y,
			_character.CurrentDestinaction.z);
		
		_moveDirectionFlag.SetActive(IsRunning);
	}
}