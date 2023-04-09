using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private List<List<UnitBody>> _listsOftargets = new List<List<UnitBody>>();
    public void SetTargetList(List<UnitBody> targetList)
    {
        _listsOftargets.Add(targetList);
    }
    public void DeleteBodyFromTargetList(UnitBody target)
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
