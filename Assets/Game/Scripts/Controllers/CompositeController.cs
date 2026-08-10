using System.Collections.Generic;

public class CompositeController : Controller
{
	private List<Controller> _controllers;

	public CompositeController(params Controller[] controllers)
	{
		_controllers = new List<Controller>(controllers);
	}

	public override void Enable()
	{
		base.Enable();

		foreach (var controller in _controllers)
		{
			controller.Enable();
		}
	}

	public override void Disable()
	{
		base.Disable();

		foreach (var controller in _controllers)
		{
			controller.Disable();
		}
	}

	protected override void UpdateLogic(float deltaTime)
	{
		foreach (var controller in _controllers)
		{
			controller.Update(deltaTime);
		}
	}
}