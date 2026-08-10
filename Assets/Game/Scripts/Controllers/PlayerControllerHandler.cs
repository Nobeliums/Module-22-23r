using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerHandler : MonoBehaviour
{
	[SerializeField] private Character _character;
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private Transform _rootTransform;
	[SerializeField] private GameObject _moveDirectionFlag;
	
	private Controller _controller;

	private void Awake()
	{
		_controller = new CompositeController(
			new NavMeshAgentController(_character.Agent, _groundLayer, _moveDirectionFlag),
			new RotateToMoveDirectionController(_character));
		_controller.Enable();
		_character.Agent.updateRotation = false;
	}

	private void Update()
	{
		_controller.Update(Time.deltaTime);

		if (_character.IsDeath && _controller.IsEnabled)
		{
			_controller.Disable();

			Debug.Log($"Контроллер отключен. {_character.Health.Value}");
		}
	}
}