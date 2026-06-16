using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;
    [SerializeField] Camera playerCamera;
    [SerializeField] float fireRate = 0.15f;

    Coroutine fireRoutine;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            fireRoutine = StartCoroutine(FireLoop());
        }
        else if (context.canceled)
        {
            if (fireRoutine != null)
                StopCoroutine(fireRoutine);
        }
    }

    private IEnumerator FireLoop()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Fire()
    {
        Instantiate(bullet, shootPos.position, Quaternion.LookRotation(playerCamera.transform.forward));
    }
}
