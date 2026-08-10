using UnityEngine;

public interface IDirectionRotateble
{
	Quaternion CurrentRotation { get; }

	void SetRotationDirection(Vector3 direction);
}
