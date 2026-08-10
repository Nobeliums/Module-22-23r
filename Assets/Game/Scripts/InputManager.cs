
using UnityEngine;

public static class InputManager
{
	public const int LeftMouseButton = 0;
	public const int RightMouseButton = 1;
	
	public static bool GetMouseButtonDown(int mouseButton)
	{
		return Input.GetMouseButtonDown(mouseButton);
	}
}