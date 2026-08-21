
using UnityEngine;

public static class InputManager
{
	public const int LeftMouseButton = 0;
	public const int RightMouseButton = 1;
	public const KeyCode SpawnerSwitchKey = KeyCode.F;
	
	public static bool GetMouseButtonDown(int mouseButton)
	{
		return Input.GetMouseButtonDown(mouseButton);
	}

	public static bool GetSpawnerSwitchKey()
	{
		return Input.GetKeyDown(SpawnerSwitchKey);
	}
}