using UnityEngine;
using UnityEngine.InputSystem;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] float attackRadius = 0.1f;

    PlayerEnemyDetection enemyDetection;
    CharacterController controller;
    PlayerInputActions input;

    EnemyCore currentTarget;

    void Awake()
    {
        enemyDetection = GetComponent<PlayerEnemyDetection>();
        controller = GetComponent<CharacterController>();

        input = new PlayerInputActions();
    }

    void OnEnable()
    {
        input.Enable();

        input.Player.Attack.performed += OnAttack;

        enemyDetection
            .GetEnemyDetectionState
            .OnBestTargetChanged += OnBestTargetChanged;
    }

    void OnDisable()
    {
        input.Player.Attack.performed -= OnAttack;

        enemyDetection
            .GetEnemyDetectionState
            .OnBestTargetChanged -= OnBestTargetChanged;

        input.Disable();
    }

    void OnBestTargetChanged(EnemyCore target)
    {
        currentTarget = target;
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        if (currentTarget == null)
            return;

        TeleportToTarget(currentTarget.transform);
        currentTarget.Hit();
    }

    void TeleportToTarget(Transform target)
    {
        Vector3 enemyPosition = target.position;

        Vector3 direction =
            (transform.position - enemyPosition).normalized;

        Vector3 destination =
            enemyPosition +
            direction * attackRadius;

        if (controller != null)
            controller.enabled = false;

        transform.position = destination;

        if (controller != null)
            controller.enabled = true;
    }
}