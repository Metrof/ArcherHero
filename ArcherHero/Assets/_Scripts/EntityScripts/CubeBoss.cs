
using UnityEngine;

public class CubeBoss : ExplosiveEnemy
{
    [SerializeField] private SpawnChildrenEnemy _spawnChildrenEnemy;

    private void Start()
    {   
        base.Start();
    }
    protected override void Attack()
    {
        if (_targetAttack != null)
        {
            IDamageable damageable = _targetAttack.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(typeDamage, damage);
            }
        }
    }
    
    protected override void Die()
    {
        base.Die();
        _spawnChildrenEnemy.OnBossDestroyed();
        DestroyGO();
    }
}