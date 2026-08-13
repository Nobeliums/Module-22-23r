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
	private GameObject _moveDirectionFlag;

	public NavMeshAgentController(IDestinationMovable agent, LayerMask groundLayer, GameObject moveDirectionFlag)
	{
		_agent = agent;
		_groundLayer = groundLayer;
		_moveDirectionFlag = moveDirectionFlag;
	}

	protected override void UpdateLogic(float deltaTime)
	{
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