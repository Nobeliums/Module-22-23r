using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : Controller
{
	private const float Epsilon = 0.05f;
	
	private NavMeshAgent _agent;
	private Vector3 _currentTarget;
	private LayerMask _groundLayer;
	private GameObject _moveDirectionFlag;

	private bool IsMove => _agent.pathPending || _agent.remainingDistance > _agent.stoppingDistance;

	public NavMeshAgentController(NavMeshAgent agent, LayerMask groundLayer, GameObject moveDirectionFlag)
	{
		_agent = agent;
		_groundLayer = groundLayer;
		_moveDirectionFlag = moveDirectionFlag;
		_currentTarget = agent.transform.position;
	}

	protected override void UpdateLogic(float deltaTime)
	{
		if (InputManager.GetMouseButtonDown(InputManager.RightMouseButton))
		{
			UpdateTarget();
		}

		if (IsMove == false)
			_moveDirectionFlag.SetActive(false);
		
	}

	private void UpdateTarget()
	{
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, _groundLayer))
		{
			_currentTarget = hit.point;
			_agent.SetDestination(_currentTarget);

			_moveDirectionFlag.SetActive(true);
			_moveDirectionFlag.transform.position = new Vector3(hit.point.x, _agent.transform.position.y, hit.point.z);
		}
	}
}