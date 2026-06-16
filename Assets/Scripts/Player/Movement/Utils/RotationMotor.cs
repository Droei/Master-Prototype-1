using UnityEngine;

public class RotationMotor
{
    readonly Transform transform;
    readonly PlayerConfig config;

    public RotationMotor(Transform transform, PlayerConfig config)
    {
        this.transform = transform;
        this.config = config;
    }

    public void Tick(Vector2 lookInput)
    {
        if (Mathf.Abs(lookInput.x) < config.rotationDeadzone)
            return;

        float rotation =
            lookInput.x *
            config.sensitivity *
            Time.deltaTime;

        transform.Rotate(Vector3.up * rotation);
    }
}