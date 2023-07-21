using PlayerStats;
using UnityEngine;

public class UpgradeSystems
{
    private const string _characterStatsPath = "CharacterStats";
    private const string _statsPathFolder = "Stats/";

    private readonly IStorageService _storageService;

    public HeroLVL HeroLvl { get; private set; }
    public CharacterStats CharacterStats { get; private set; }

    public UpgradeSystems(IStorageService storageService)
    {
        _storageService = storageService;

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
        _storageService.Load<int>(HeroLvl.ToString(), LoadHeroLvlCallBack);
    }

    private void LoadCharacterStats()
    {
        CharacterStats = Resources.Load<CharacterStats>(_characterStatsPath);

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
        _storageService.Save(HeroLvl.ToString(), HeroLvl.CurrentLvl);
    }
}
