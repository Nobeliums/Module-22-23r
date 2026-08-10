using UnityEngine;

public class RotateToMoveDirectionController : Controller
{
	private Character _character;

	public RotateToMoveDirectionController(Character character)
	{
		_character = character;
	}

	protected override void UpdateLogic(float deltaTime)
	{
		_character.SetRotationDirection(_character.Agent.desiredVelocity);
	}
}