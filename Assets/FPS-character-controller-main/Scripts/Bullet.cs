using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public int damage = 25;

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = transform.forward;
        float distance = speed * Time.deltaTime;

        if (Physics.Raycast(currentPosition, direction, out RaycastHit hit, distance))
        {
            //Enemy enemy = hit.collider.GetComponent<Enemy>();
            //if (enemy != null)
            //{
            //    enemy.TakeDamage(damage);
            //    Destroy(gameObject);
            //}

            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * distance);
        }
    }
}
