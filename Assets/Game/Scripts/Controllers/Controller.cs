public abstract class Controller
{
	protected bool _isEnabled;

	public bool IsEnabled => _isEnabled;

	public void Update(float deltaTime)
	{
		if (_isEnabled == false)
			return;
		
		UpdateLogic(deltaTime);
	}

	public virtual void Enable() => _isEnabled = true;

	public virtual void Disable() => _isEnabled = false;

	protected abstract void UpdateLogic(float deltaTime);
}
