using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerConfig config;

    CinemachineCamera camera;

    CharacterController controller;
    PlayerInputActions input;

    MovementMotor movement;
    RotationMotor rotation;
    SprintLogic sprint;
    CameraLogic cameraLogic;

    void Awake()
    {
        camera = FindFirstObjectByType<CinemachineCamera>();
        controller = GetComponent<CharacterController>();
        input = new PlayerInputActions();

        movement = new MovementMotor(controller, transform, config);
        rotation = new RotationMotor(transform, config);
        sprint = new SprintLogic(config);
        cameraLogic = new CameraLogic(camera, config);
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();
        Vector2 lookInput = input.Player.Look.ReadValue<Vector2>();

        bool isSprinting = sprint.IsSprinting(lookInput);

        rotation.Tick(lookInput);
        movement.Tick(moveInput, isSprinting);
        cameraLogic.Tick(isSprinting);
    }
}