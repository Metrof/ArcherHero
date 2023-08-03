using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectilePrefabsScriptable", menuName = "ScriptableObjects/ProjectilePrefabsScriptable")]
public class ProjectilePrefabsScriptable : ScriptableObject
{
    [SerializeField] private List<Projectile> _projectilePrefabs;

    public List<Projectile> ProjectilesPrefabs => _projectilePrefabs;
}
