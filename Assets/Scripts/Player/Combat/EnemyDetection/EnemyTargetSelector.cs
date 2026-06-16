using System.Collections.Generic;
using UnityEngine;

public static class EnemyTargetSelector
{
    public static EnemyCore SelectBest(
        List<EnemyCore> enemies,
        Camera cam,
        Transform player,
        float innerRadius,
        float outerRadius)
    {
        Vector2 screenCenter = GetScreenCenter();

        List<EnemyCore> valid =
            GetValidEnemies(enemies, cam, screenCenter, outerRadius);

        if (valid.Count == 0)
            return null;

        List<EnemyCore> inner =
            GetInnerEnemies(valid, player, innerRadius);

        List<EnemyCore> source =
            inner.Count > 0 ? inner : valid;

        return GetBestByPlayerDistance(source, player);
    }

    private static Vector2 GetScreenCenter()
    {
        return new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private static List<EnemyCore> GetValidEnemies(
        List<EnemyCore> enemies,
        Camera cam,
        Vector2 screenCenter,
        float outerRadius)
    {
        List<EnemyCore> valid = new();

        foreach (var enemy in enemies)
        {
            if (!DetectionLogic.InCameraView(cam, enemy.transform.position))
                continue;

            Vector3 screenPos =
                cam.WorldToScreenPoint(enemy.transform.position);

            if (screenPos.z < 0)
                continue;

            float screenDist =
                Vector2.Distance(screenCenter, screenPos);

            if (screenDist > outerRadius)
                continue;

            valid.Add(enemy);
        }

        return valid;
    }

    private static List<EnemyCore> GetInnerEnemies(
        List<EnemyCore> valid,
        Transform player,
        float innerRadius)
    {
        List<EnemyCore> inner = new();

        foreach (var enemy in valid)
        {
            float playerDist =
                Vector3.Distance(player.position, enemy.transform.position);

            if (playerDist <= innerRadius)
                inner.Add(enemy);
        }

        return inner;
    }

    private static EnemyCore GetBestByPlayerDistance(
        List<EnemyCore> enemies,
        Transform player)
    {
        EnemyCore best = null;
        float bestScore = float.MinValue;

        foreach (var enemy in enemies)
        {
            float playerDist =
                Vector3.Distance(player.position, enemy.transform.position);

            float score = 1f / (1f + playerDist);

            if (score > bestScore)
            {
                bestScore = score;
                best = enemy;
            }
        }

        return best;
    }
}