using System.Collections.Generic;
using UnityEngine;

public static class EnemyTargetSelector
{
    public static EnemyCore SelectBest(
        List<EnemyCore> enemies,
        Camera cam,
        float innerRadius,
        float outerRadius)
    {
        EnemyCore best = null;
        float bestScore = float.MaxValue;

        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);

        foreach (var enemy in enemies)
        {
            if (!DetectionLogic.InCameraView(cam, enemy.transform.position))
                continue;

            Vector3 screenPos = cam.WorldToScreenPoint(enemy.transform.position);

            if (screenPos.z < 0)
                continue;

            float dist = Vector2.Distance(center, screenPos);

            if (dist > outerRadius)
                continue;

            float score = dist;

            if (dist < innerRadius)
                score *= 0.5f;

            if (score < bestScore)
            {
                bestScore = score;
                best = enemy;
            }
        }

        return best;
    }
}