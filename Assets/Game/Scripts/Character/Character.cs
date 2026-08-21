using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDirectionRotateble, IDamagable, IHealable, IDestinationMovable, IJumpable
{
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private int _maxHealth;
	[SerializeField] private int _startingHealth;
	[SerializeField] private int _jumpSpeed;
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private AnimationCurve _jumpCurve;

	private DirectionRotator _rotator;
	private NavMeshAgentJumper _jumper;
	private Health _health;


	public Quaternion CurrentRotation => transform.rotation;
	public Health Health => _health;
	public bool IsDeath => _health.Value <= 0;
	public NavMeshAgent Agent => _agent;

	public bool IsMoving => _agent.pathPending || _agent.remainingDistance > _agent.stoppingDistance;
	public Vector3 CurrentDestinaction => _agent.destination;

	public bool CanJump => _agent.isOnOffMeshLink;
	public bool InJumpProcess => _jumper.InProcess;

	private void Awake()
	{
		_rotator = new DirectionRotator(_rotationSpeed, transform);
		_health = new Health(_startingHealth, _maxHealth);
		_agent.updateRotation = false;
		_jumper = new NavMeshAgentJumper(_agent, _jumpSpeed, this, _jumpCurve);
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
	}

	public void Heal(int amount)
	{
		if (amount <= 0)
		{
			Debug.LogError($"{nameof(amount)} в методе {nameof(Heal)} должен быть >= 0");
			return;
		}
		
		_health.Value += amount;
	}


	public void SetDestination(Vector3 target) => _agent.SetDestination(target);

	public void Jump() => _jumper.Jump(_agent.currentOffMeshLinkData);
}
