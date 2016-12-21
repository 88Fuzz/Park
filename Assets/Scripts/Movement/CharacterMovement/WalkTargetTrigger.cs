using UnityEngine;

/*
 * Component to tell anthing that implements IWalkTargetTriggerListener that the object is inside a walk target.
 */
public class WalkTargetTrigger : MonoBehaviour
{
    public int targetId{get; set;}

    public void OnTriggerEnter(Collider collider)
    {
        IWalkTargetTriggerListener testing = collider.gameObject.GetComponent<IWalkTargetTriggerListener>();
        if(testing != null)
            testing.walkTargetTriggered(targetId);
    }

    public WalkTarget getWalkTarget()
    {
        WalkTarget walkTarget = new WalkTarget();
        walkTarget.transformId = targetId;
        walkTarget.transform = transform;

        return walkTarget;
    }
}