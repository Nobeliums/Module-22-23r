public interface IJumpable
{
	public bool CanJump { get; }
	public bool InJumpProcess { get; }

	public void Jump();
}