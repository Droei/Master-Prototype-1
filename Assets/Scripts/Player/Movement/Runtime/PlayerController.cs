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

    PlayerAnimationHandler animationHandler;

    public PlayerStateMachine StateMachine { get; private set; }

    void Awake()
    {
        camera = FindFirstObjectByType<CinemachineCamera>();

        controller = GetComponent<CharacterController>();
        input = new PlayerInputActions();

        movement = new MovementMotor(controller, transform, config);
        rotation = new RotationMotor(transform, config);
        sprint = new SprintLogic(config);
        cameraLogic = new CameraLogic(camera, config);

        animationHandler = GetComponent<PlayerAnimationHandler>();

        StateMachine = new PlayerStateMachine();
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();
        Vector2 lookInput = input.Player.Look.ReadValue<Vector2>();

        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        bool isSprinting = sprint.IsSprinting(lookInput);

        rotation.Tick(lookInput);
        movement.Tick(moveInput, isSprinting);
        cameraLogic.Tick(isSprinting);

        UpdateState(isMoving, isSprinting);

        animationHandler.UpdateMovement(moveInput, isSprinting);
    }

    void UpdateState(bool isMoving, bool isSprinting)
    {
        PlayerState newState;

        if (!isMoving)
            newState = PlayerState.Idle;
        else if (isSprinting)
            newState = PlayerState.Running;
        else
            newState = PlayerState.Walking;

        StateMachine.SetState(newState);
    }
}