using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(ProjectilePull))]
public class GameManager : MonoBehaviour
{
    [SerializeField] Dagger _dagger;
    [SerializeField] private PlayerController _playerPref;
    [SerializeField] private CharacterStatsE _baseCharacterStats;
    [SerializeField] private EnemyController _enemyPref;

    [SerializeField] private Transform _defoltPlayerPos;
    [SerializeField] private Transform _enemyAnchor;
    [SerializeField] private Vector3 _enemyPullPos;
    [SerializeField] private float _mapDiagonalSize = 39;

    [SerializeField] private Level[] _levels;

    [SerializeField] private Image _playerHpBar;

    private PlayerController _playerController;
    private CameraController _cameraController;

    private Level _currentLvl;
    private GameObject _lvlObj;

    private Wave[] _waves;
    private int _currentWave;

    private LvlTriggerZone _startTriggerZone;
    private LvlTriggerZone _endTriggerZone;

    private Dictionary<EnemyTypes, AbstractEnemyFactory> _enemyfactoryDic = new();

    private DiContainer _diContainer;
    private DataHolderTestZ _holderTestZ;
    private UnitManager _unitManager;

    [Inject]
    private void Construct(DiContainer container, DataHolderTestZ holderTestZ, UnitManager unitManager)
    {
        _diContainer = container;
        _holderTestZ = holderTestZ;
        _unitManager = unitManager;
    }

    // тестовый вариант с конструктором


    Dictionary<PerkManager.PerkType, PerkManager.PerkStatus> _perkStates;

    private static int _lvlCount;
    public static int LvlCount { get { return _lvlCount; } }

    private void Awake()
    {
        _lvlCount = _levels.Length;
        _cameraController = GetComponent<CameraController>();
        _currentLvl = _levels[_holderTestZ.LvlStart];

        _enemyfactoryDic.Add(EnemyTypes.DefoltRobot, new DefoltEnemyFactory(EnemyTypes.DefoltRobot, _enemyPullPos));
    }

    public void Start()
    {
        LvlInit();
        CreateEnemys(_currentLvl.EnemyPower);
        CreatePlayer(_holderTestZ.PlayerStats);
        _unitManager.OnLastEnemyDeath += StartNewWave;
        _cameraController.SetTarget(_playerController.transform);

        _perkStates = new Dictionary<PerkManager.PerkType, PerkManager.PerkStatus>();
    }

    private void LoadPerkData()
    {
        Debug.Log("LoadPD");
        _perkStates = SaveSystem.SaveSystem.LoadPerkData();
    }

    public List<IPerk> GetPurchasedPerks()
    {
        LoadPerkData();

        List<IPerk> purchasedPerks = new List<IPerk>();

        PerkFactory perkFactory = new PerkFactory();

        foreach (var perkEntry in _perkStates)
        {
            if (perkEntry.Value == PerkManager.PerkStatus.Purchased)
            {
                IPerk perk = perkFactory.CreatePerk(perkEntry.Key);
                purchasedPerks.Add(perk);
            }
        }
        return purchasedPerks;
    }

    private void CreatePlayer(CharacterStatsE playerStats)
    {
        if (_playerPref != null)
        {
            _playerController = Instantiate(_playerPref, _defoltPlayerPos.position, Quaternion.identity);
            var playerView = _playerController.GetComponent<PlayerView>();
            var playerModel = new PlayerModel(_unitManager, _playerController.gameObject.layer, _playerController.GetComponent<Renderer>().material);

            if (playerStats == null) playerStats = _baseCharacterStats;
            _playerController.Init(playerView, playerModel, _defoltPlayerPos.position);
            _playerController.SetNewModelParram(playerStats);
            _playerController.SubscriptionUIForHpUpdate(_playerHpBar);

            Dagger dagger = Instantiate(_dagger);
            dagger.SetOwner(_playerController.transform);

            List<IPerk> purchasedPerks = GetPurchasedPerks();

            _playerController.SetFirstSkill(new Dash(2, purchasedPerks));

            _playerController.SetSecondSkill(new Parry(dagger.gameObject, 2));

            _playerController.LvlStart();

            _unitManager.SetPlayerAtUnitManager(_playerController);
        }
    }
    private void CreateEnemys(float scaleStats)
    {
        if (_currentLvl != null)
        {
            var enemysList = new List<EnemyController>();
            for (int i = 0; i < _currentLvl.EnemyTypes.Count; i++)
            {
                for (int f = 0; f < _currentLvl.EnemyCount; f++)
                {
                    EnemyTypes type = _currentLvl.EnemyTypes[i]; 
                    enemysList.Add(_enemyfactoryDic[type]
                        .CreateEnemy(_diContainer, _enemyPref, _enemyAnchor, _baseCharacterStats, scaleStats));
                }
            }
            _unitManager.SetEnemysAtUnitManager(enemysList);
        }
    }
    private void LvlInit()
    {
        _lvlObj = Instantiate(_currentLvl.LvlPref, Vector3.zero, Quaternion.identity);
        _startTriggerZone = Instantiate(_currentLvl.LvlStartTriggerZone);
        _endTriggerZone =  Instantiate(_currentLvl.LvlEndTriggerZone);
        _waves = _lvlObj.GetComponentsInChildren<Wave>();

        _startTriggerZone.OnTrigger += LvlStart;
        _endTriggerZone.OnTrigger += LvlEnd;
    }
    private void LvlStart()
    {
        LvlDoor.CloseDoor();
        StartNewWave();
    }
    public void StartNewWave()
    {
        if (_currentWave > _waves.Length - 1)
        {
            LvlDoor.OpenDoor();
            _unitManager.LvlEnd();
            return;
        }
        _unitManager.TeleportWaveEnemys(_waves[_currentWave].Points);
        _currentWave++;
    }
    private void LvlEnd()
    {
        SceneManager.LoadScene(0);
    }
}
