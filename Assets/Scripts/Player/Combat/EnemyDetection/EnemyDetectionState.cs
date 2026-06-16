using System;
using System.Collections.Generic;

public class EnemyDetectionState
{
    public event Action<EnemyCore> OnBestTargetChanged;

    public EnemyCore CurrentBestTarget { get; private set; }

    private DetectionTracker tracker = new DetectionTracker();

    public void Evaluate(EnemyCore bestTarget)
    {
        if (bestTarget != null)
            tracker.Evaluate(new HashSet<EnemyCore> { bestTarget });
        else
            tracker.Evaluate(new HashSet<EnemyCore>());

        if (bestTarget != CurrentBestTarget)
        {
            CurrentBestTarget = bestTarget;
            OnBestTargetChanged?.Invoke(CurrentBestTarget);
        }
    }
}