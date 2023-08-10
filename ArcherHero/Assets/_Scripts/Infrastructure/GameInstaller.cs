using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ProjectilePool _prefabProjectilePool;
    [SerializeField] private Player _player;
    public override void InstallBindings()
    {
        BindProjectilePool();
        BindEnemyPool();
        BindPlayer();
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
    private void BindPlayer()
    {
        Container.Bind<Player>()
            .FromComponentInNewPrefab(_player)
            .AsSingle()
            .NonLazy();
    }
}