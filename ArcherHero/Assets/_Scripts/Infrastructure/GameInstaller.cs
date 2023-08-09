using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ProjectilePool _prefabProjectilePool;
    public override void InstallBindings()
    {
        BindProjectilePool();
        BindEnemyPool();
    }

    private void BindProjectilePool()
    {
        Container
            .Bind<ProjectilePool>()
            .FromComponentInNewPrefab(_prefabProjectilePool)
            .AsSingle()
            .NonLazy();
    }
    private void BindEnemyPool()
    {
        Container.Bind<EnemyPool>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}