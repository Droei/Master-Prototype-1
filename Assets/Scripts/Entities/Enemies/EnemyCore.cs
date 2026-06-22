using UnityEngine;

public class EnemyCore : MonoBehaviour
{

    [SerializeField] private float knockbackRadius = 5f;
    [SerializeField] private float maxKnockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.3f;
    [SerializeField] GameObject debugCanvas;

    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void InRange()
    {
        debugCanvas.SetActive(true);
    }

    public void OutRange()
    {
        debugCanvas.SetActive(false);
    }

    public void Hit()
    {
        animator.SetTrigger("Slam");

    }

    public void TriggerKnockback()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            knockbackRadius
        );

        foreach (Collider collider in colliders)
        {
            EnemyMovement enemy = collider.GetComponent<EnemyMovement>();

            if (enemy == null || enemy.gameObject == gameObject)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                enemy.transform.position
            );

            float normalizedDistance = distance / knockbackRadius;
            float force = maxKnockbackForce * (1f - normalizedDistance);

            Vector3 direction = enemy.transform.position - transform.position;
            direction.y = 0f;
            direction.Normalize();

            enemy.Knockback(direction, force, knockbackDuration);
        }
    }
}