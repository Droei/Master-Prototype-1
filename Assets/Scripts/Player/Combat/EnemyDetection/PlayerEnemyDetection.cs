using UnityEngine;

public class PlayerEnemyDetection : MonoBehaviour
{
    [SerializeField] DetectionSettings settings;

    Camera playerCamera;
    float timer;

    EnemyDetectionState enemyDetectionState = new EnemyDetectionState();

    void Awake()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= settings.checkInterval)
        {
            timer = 0f;
            Tick();
        }
    }

    void Tick()
    {
        var candidates = EnemyDetectionQuery.GetCandidates(
            transform.position,
            settings.radius,
            settings.enemyMask
        );

        EnemyCore best = EnemyTargetSelector.SelectBest(
            candidates,
            playerCamera,
            transform,
            settings.innerPriorityRadius,
            settings.outerPriorityRadius
        );

        enemyDetectionState.Evaluate(best);
    }

    void OnDrawGizmosSelected()
    {
        if (settings == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, settings.radius);
    }

    public EnemyDetectionState GetEnemyDetectionState => enemyDetectionState;
}