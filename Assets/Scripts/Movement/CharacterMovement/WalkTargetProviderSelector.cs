using UnityEngine;

[RequireComponent(typeof(PlayerWalkTargetProvider))]
public class WalkTargetProviderSelector : MonoBehaviour
{
    public IWalkTargetProvider getPlayerWalkTargetProvider()
    {
        return GetComponent<PlayerWalkTargetProvider>();
    }
}