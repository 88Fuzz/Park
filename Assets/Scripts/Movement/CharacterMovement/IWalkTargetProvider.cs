public interface IWalkTargetProvider
{
    /*
     * Given the current WalkTarget and a WalkDirection, will return the next walk target the character should move to.
     */
    WalkTarget getNextWalkTarget(WalkTarget currentTarget, WalkDirection walkDirection);

    /*
     * Get the target the character should start at.
     */
    WalkTarget getStartingTarget();

    /*
     * Get the direction the character should be walking.
     */
    WalkDirection getStartingWalkDirection();
}