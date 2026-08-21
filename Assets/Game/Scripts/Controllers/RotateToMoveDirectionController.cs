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
		if (_character.InJumpProcess)
		{
			_character.SetRotationDirection(_character.Agent.currentOffMeshLinkData.endPos - _character.Agent.currentOffMeshLinkData.startPos);
			return;
		}
		
		_character.SetRotationDirection(_character.Agent.desiredVelocity);
	}
}