using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class GameDataServiceInstaller : MonoInstaller
{
    [SerializeField] private GameDataService _gameDataService;
    public override void InstallBindings()
    {
        Container.Bind<IGameDataService>().
            To<GameDataService>().
            FromComponentInNewPrefab(_gameDataService).AsSingle();
    }
}
