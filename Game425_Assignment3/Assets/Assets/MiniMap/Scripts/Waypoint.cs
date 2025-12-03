using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public string waypointName;
    private bool triggered = false;
    private BoxCollider BoxCollider;

    void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();
    }

    public bool CheckTrigger(BoxCollider playerCollider)
    {
        if (triggered) return false;

        if (CollisionHelper.CheckAABB(playerCollider, BoxCollider))
        {
            triggered = true;
            return true;
        }
        return false;
    }
}