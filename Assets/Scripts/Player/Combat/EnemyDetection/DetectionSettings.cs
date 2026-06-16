using UnityEngine;

[System.Serializable]
public class DetectionSettings
{
    public float radius = 10f;
    public LayerMask enemyMask;
    public float checkInterval = 0.2f;

    public float innerPriorityRadius = 120f;
    public float outerPriorityRadius = 300f;
}