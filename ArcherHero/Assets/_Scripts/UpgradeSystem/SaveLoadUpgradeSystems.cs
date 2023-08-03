using PlayerStats;
using Zenject;

public class SaveLoadUpgradeSystems
{
    private const string _statsPathFolder = "Stats/";
    private const string _nameSaveHeroLvl = "HeroLVL";

    private IStorageService _storageService;
    private DiContainer _container;

    public HeroLVL HeroLvl { get; private set; }
    public CharacterStats CharacterStats { get; private set; }

    public SaveLoadUpgradeSystems(IStorageService storageService, CharacterStats characterStats, DiContainer diContainer)
    {
        _storageService = storageService;
        CharacterStats = characterStats;
        _container = diContainer;

        LoadData();
    }

    public void Save()
    {
        SaveHeroLvl();
        SaveCharacterStats();
    }

    private void LoadData()
    {
        LoadCharacterStats();
        LoadHeroLvl();
    }

    private void LoadHeroLvl()
    {
        _storageService.Load<int>(_nameSaveHeroLvl, LoadHeroLvlCallBack);
        _container.Bind<HeroLVL>().FromInstance(HeroLvl).AsSingle();
    }

    private void LoadCharacterStats()
    {
        foreach (StatInfo stat in CharacterStats.Stats)
        {
            _storageService.Load<int>(_statsPathFolder + stat.Name, LoadStatLvl);

            void LoadStatLvl(int lvl)
            {
                if (lvl != 0)
                {
                    stat.InitializeUnlockedLvl(lvl);
                }
            }
        }
    }

    private void LoadHeroLvlCallBack(int heroLvl)
    {
        int lvl = (heroLvl != 0) ? heroLvl : 1;

        HeroLvl = new HeroLVL(lvl, 3000, 100);
    }

    private void SaveCharacterStats()
    {
        foreach (StatInfo stat in CharacterStats.Stats)
        {
            _storageService.Save(_statsPathFolder + stat.Name, stat.CurrentValue);
        }
    }

    private void SaveHeroLvl()
    {
        _storageService.Save(_nameSaveHeroLvl, HeroLvl.CurrentLvl);
    }
}
