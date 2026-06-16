using System.Collections.Generic;
using UnityEngine;

public static class EnemyDetectionQuery
{
    public static List<EnemyCore> GetCandidates(
        Vector3 origin,
        float radius,
        LayerMask mask)
    {
        Collider[] hits = Physics.OverlapSphere(origin, radius, mask);

        List<EnemyCore> result = new List<EnemyCore>();

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out EnemyCore enemy))
                result.Add(enemy);
        }

        return result;
    }
}