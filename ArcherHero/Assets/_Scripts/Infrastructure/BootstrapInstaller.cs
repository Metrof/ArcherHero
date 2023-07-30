using PlayerStats;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    private const string _characterStatsPath = "CharacterStats";

    private IStorageService _storageService;
    private CharacterStats _characterStats;

    public override void InstallBindings()
    {
        BindStorageService();
        BindBank();
        BindCharacterStats();
        BindSaveLoadUpgradeSystems();
    }

    private void BindBank()
    {
        ResourceBank bank = new ResourceBank(_storageService);

        Container
            .Bind<ResourceBank>()
            .FromInstance(bank)
            .AsSingle();
    }

    private void BindSaveLoadUpgradeSystems()
    {
        SaveLoadUpgradeSystems upgradeSystems = new SaveLoadUpgradeSystems(_storageService, _characterStats, Container);

        Container
            .Bind<SaveLoadUpgradeSystems>()
            .FromInstance(upgradeSystems)
            .AsSingle();
    }

    private void BindCharacterStats()
    {
        _characterStats = Resources.Load<CharacterStats>(_characterStatsPath);

        Container
            .Bind<CharacterStats>()
            .FromInstance(_characterStats)
            .AsSingle();
    }

    private void BindStorageService()
    {
        _storageService = new JsonToFileStorageService();

        Container
            .Bind<IStorageService>()
            .FromInstance(_storageService)
            .AsSingle();
    }
}
