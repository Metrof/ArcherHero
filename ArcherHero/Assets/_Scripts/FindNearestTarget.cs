using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FindNearestTarget : MonoBehaviour
{
    public static Transform GetNearestTarget(Vector3 unitPos, List<Transform> targets)
    {
        Transform nearestTarget = null;
        float minDistance = Mathf.Infinity;
        foreach (var target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, unitPos);
            if (distance < minDistance)
            {
                nearestTarget = target.transform;
                minDistance = distance;
            }
        }
        return nearestTarget;
    }
    public static List<Transform> GetVisibleTargets(Vector3 unitPos, List<Transform> targets, int raycastLayerMask = ~0)
    {
        List<Transform> visibleTargets = new List<Transform>();
        foreach (var target in targets)
        {
            if (target == null)
            {
                continue;
            }
            RaycastHit hit;
            if (Physics.Raycast(unitPos, target.transform.position - unitPos, out hit, Mathf.Infinity, raycastLayerMask))
            {
                if (hit.collider.gameObject == target.gameObject)
                {
                    visibleTargets.Add(target);
                }
            }
        }
        return visibleTargets;
    }
}
