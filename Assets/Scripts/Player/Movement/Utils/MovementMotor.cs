using UnityEngine;

public class MovementMotor
{
    readonly CharacterController controller;
    readonly Transform transform;
    readonly PlayerConfig config;

    Vector3 velocity;

    public MovementMotor(
        CharacterController controller,
        Transform transform,
        PlayerConfig config)
    {
        this.controller = controller;
        this.transform = transform;
        this.config = config;
    }

    public void Tick(Vector2 moveInput, bool sprinting)
    {
        float speed =
            sprinting
            ? config.sprintSpeed
            : config.moveSpeed;

        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        velocity.y += config.gravity * Time.deltaTime;

        controller.Move(
            (move * speed + velocity) *
            Time.deltaTime);
    }
}