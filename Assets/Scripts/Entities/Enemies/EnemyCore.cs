using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    [SerializeField] GameObject debugCanvas;
    public void InRange()
    {
        debugCanvas.SetActive(true);
    }

    public void OutRange()
    {
        debugCanvas.SetActive(false);
    }
}