using UnityEngine;

public interface IDestinationMovable
{
	bool IsMoving { get; }
	Vector3 CurrentDestinaction { get; }

	void SetDestination(Vector3 target);
}