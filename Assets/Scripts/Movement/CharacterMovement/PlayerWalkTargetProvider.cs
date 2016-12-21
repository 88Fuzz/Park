using UnityEngine;

public class PlayerWalkTargetProvider : MonoBehaviour, IWalkTargetProvider
{
    public WalkTargetTrigger[] walkTargetTriggers;
    private WalkTarget[] walkTargets;

    public void Awake()
    {
        walkTargets = new WalkTarget[walkTargetTriggers.Length];
        int count = 0;
        foreach(WalkTargetTrigger walkTargetTrigger in walkTargetTriggers)
        {
            walkTargetTrigger.targetId = count;
            walkTargets[count++] = walkTargetTrigger.getWalkTarget();
        }
    }

    public WalkTarget getNextWalkTarget(WalkTarget currentTarget, WalkDirection walkDirection)
    {
        if (currentTarget.transformId == 0 && walkDirection == WalkDirection.BACKWARD)
            return null;
        else if (currentTarget.transformId == walkTargets.Length && walkDirection == WalkDirection.FORWARD)
            return null;

        switch(walkDirection)
        {
            case WalkDirection.FORWARD:
                return walkTargets[currentTarget.transformId + 1];
            case WalkDirection.BACKWARD:
                return walkTargets[currentTarget.transformId - 1];
            default:
                return null;
        }
    }

    public WalkTarget getStartingTarget()
    {
        return walkTargets[0];
    }

    public WalkDirection getStartingWalkDirection()
    {
        return WalkDirection.FORWARD;
    }
}