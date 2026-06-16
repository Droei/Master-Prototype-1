using System.Collections.Generic;

public class DetectionTracker
{
    private readonly HashSet<EnemyCore> current = new HashSet<EnemyCore>();

    public void Evaluate(HashSet<EnemyCore> newSet)
    {
        foreach (var enemy in newSet)
        {
            if (!current.Contains(enemy))
            {
                current.Add(enemy);
                enemy.InRange();
            }
        }

        List<EnemyCore> toRemove = new List<EnemyCore>();

        foreach (var enemy in current)
        {
            if (!newSet.Contains(enemy))
            {
                enemy.OutRange();
                toRemove.Add(enemy);
            }
        }

        foreach (var enemy in toRemove)
        {
            current.Remove(enemy);
        }
    }
}