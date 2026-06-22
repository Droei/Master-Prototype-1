using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;

    [Header("Movement")]
    public NavMeshAgent agent;

    private bool isKnockedBack;
    bool isDead = false;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerEnemyDetection>().transform;
    }

    private void Update()
    {
        if (player == null || isKnockedBack || isDead)
            return;

        agent.SetDestination(player.position);
    }

    public void Knockback(Vector3 direction, float force, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(KnockbackCoroutine(direction, force, duration));
    }

    private IEnumerator KnockbackCoroutine(
        Vector3 direction,
        float force,
        float duration)
    {
        isKnockedBack = true;
        agent.isStopped = true;

        direction.y = 0f;
        direction.Normalize();

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            float currentForce =
                force * (1f - Mathf.SmoothStep(0f, 1f, t));

            transform.position +=
                direction * currentForce * Time.deltaTime;

            yield return null;
        }

        agent.isStopped = false;
        isKnockedBack = false;
    }

    public void SetDeath() => isDead = true;
}