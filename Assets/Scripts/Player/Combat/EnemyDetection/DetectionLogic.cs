using UnityEngine;

public static class DetectionLogic
{
    public static bool InCameraView(Camera cam, Vector3 worldPos)
    {
        Vector3 sp = cam.WorldToScreenPoint(worldPos);

        if (sp.z < 0) return false;

        return sp.x >= 0 &&
               sp.x <= Screen.width &&
               sp.y >= 0 &&
               sp.y <= Screen.height;
    }

    public static bool InRange(Vector3 a, Vector3 b, float radius)
    {
        return (a - b).sqrMagnitude <= radius * radius;
    }
}