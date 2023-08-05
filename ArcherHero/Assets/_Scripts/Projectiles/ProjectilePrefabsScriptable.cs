using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectilePrefabsScriptable", menuName = "ScriptableObjects/ProjectilePrefabsScriptable")]
public class ProjectilePrefabsScriptable : ScriptableObject
{
    [SerializeField] private List<KeyValuePair<string, Projectile>> _projectilesData;

    public List<KeyValuePair<string, Projectile>> ProjectilesData => _projectilesData;
}
