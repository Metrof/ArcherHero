using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LvlDoor _door;
    [SerializeField] private ProjectilePool _prefabProjectilePool;
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        BindLvlSwitchManager();
        BindProjectilePool();
        BindEnemyPool();
        BindPlayer();
        BindDoor();
        BindCamera();
    }

    private void BindCamera()
    {
        Container.Bind<Camera>()
            .FromComponentInNewPrefab(_camera)
            .AsSingle()
            .NonLazy();
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
    private void BindLvlSwitchManager()
    {
        Container.Bind<LvlSwitchManager>()
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
    private void BindDoor()
    {
        Container.Bind<LvlDoor>()
            .FromComponentInNewPrefab(_door)
            .AsSingle()
            .NonLazy();
    }
}