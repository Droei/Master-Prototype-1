[System.Serializable]
public class PlayerConfig
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float gravity = -9.81f;
    public float sensitivity = 100f;

    public float rotationDeadzone = 0.25f;
    public float sprintThreshold = 0.25f;

    public float normalCameraDistance = 2f;
    public float sprintCameraDistance = 5f;
}