using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePull : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePref;
    [SerializeField] private Transform _projectileAnchor;
    [SerializeField] private Transform _pullPos;
    [SerializeField] private int _projectileCount;

    private static int _currentProjectile = 0;
    private static List<Projectile> _projectiles = new List<Projectile>();
    private void Start()
    {
        if (_projectilePref != null)
        {
            for (int i = 0; i < _projectileCount; i++)
            {
                Projectile projectile = Instantiate(_projectilePref, _projectileAnchor);
                projectile.SetPullPos(_pullPos.position);
                projectile.Effects.Add(new DamageEffect(50));
                _projectiles.Add(projectile);
            }
        }
    }
    public static Projectile GetProjectile(int layer)
    {
        _currentProjectile++;
        _currentProjectile %= _projectiles.Count;
        Projectile projectile = _projectiles[_currentProjectile];
        projectile.ChangeLayer(layer);
        return projectile;
    }
}
