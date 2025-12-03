using UnityEngine;

public static class CollisionHelper
{
    public static bool CheckAABB(BoxCollider a, BoxCollider b)
    {
        Bounds ab = a.bounds;
        Bounds bb = b.bounds;

        return ab.min.x <= bb.max.x &&
               ab.max.x >= bb.min.x &&
               ab.min.y <= bb.max.y &&
               ab.max.y >= bb.min.y &&
               ab.min.z <= bb.max.z &&
               ab.max.z >= bb.min.z;
    }
}