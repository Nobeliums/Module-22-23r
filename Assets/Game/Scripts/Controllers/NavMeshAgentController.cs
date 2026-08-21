using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : Controller
{
	private const float Epsilon = 0.05f;
	
	private IDestinationMovable _agent;
	private Vector3 _currentTarget;
	private LayerMask _groundLayer;
	private readonly IJumpable _jumpable;

	public NavMeshAgentController(IDestinationMovable agent,
		LayerMask groundLayer,
		GameObject moveDirectionFlag,
		IJumpable jumpable)
	{
		_agent = agent;
		_groundLayer = groundLayer;
		_jumpable = jumpable;
	}

	protected override void UpdateLogic(float deltaTime)
	{
		if (_jumpable.CanJump)
		{
			if (_jumpable.InJumpProcess)
				return;

			_jumpable.Jump();
		}

		if (InputManager.GetMouseButtonDown(InputManager.RightMouseButton))
		{
			UpdateTarget();
		}
	}

	private void UpdateTarget()
	{
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, _groundLayer))
		{
			_currentTarget = hit.point;
			_agent.SetDestination(_currentTarget);
		}
	}
}