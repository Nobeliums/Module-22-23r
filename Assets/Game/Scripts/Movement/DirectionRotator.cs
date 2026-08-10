using UnityEngine;

public class DirectionRotator
{
    private float _rotationSpeed;
    private Transform _rotatableTransform;

    public DirectionRotator(float rotationSpeed, Transform rotatableTransform)
    {
        _rotationSpeed = rotationSpeed;
        _rotatableTransform = rotatableTransform;
    }

    public Vector3 MoveDirection { get; set; } 

    public void Update(float deltaTime)
    {
        if (MoveDirection.magnitude <= float.Epsilon)
            return;
        
        Quaternion rotateDirection = Quaternion.LookRotation(MoveDirection.normalized);
        float step = _rotationSpeed * deltaTime;

        _rotatableTransform.rotation = Quaternion.RotateTowards(_rotatableTransform.rotation, rotateDirection, step);
    }
}
