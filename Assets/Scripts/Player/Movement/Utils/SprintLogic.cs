using UnityEngine;

public class SprintLogic
{
    readonly PlayerConfig config;

    public SprintLogic(PlayerConfig config)
    {
        this.config = config;
    }

    public bool IsSprinting(Vector2 moveInput)
    {
        return moveInput.y > config.sprintThreshold;
    }
}