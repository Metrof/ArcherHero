using UnityEngine;

public static class CalculateMovement
{
    public static float CalculateMoveTime(Vector3 point1, Vector3 point2, float speed)
    {
        float distance = Vector3.Distance(point1, point2);
        float time = distance / speed;
        return time;
    }
}