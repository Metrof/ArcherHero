using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private List<List<Transform>> _listsOftargets = new List<List<Transform>>();
    public void SetTargetList(List<Transform> targetList)
    {
        _listsOftargets.Add(targetList);
    }
    public void DeleteBodyFromTargetList(Transform target)
    {
        foreach (var list in _listsOftargets)
        {
            if (list.Contains(target))
            {
                list.Remove(target);
            }
        }
    }
}
