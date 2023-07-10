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
    [SerializeField] private GameObject _enemyPref;

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

    private DataHolderTestZ _holderTestZ;

    [Inject] private DiContainer _diContainer;
    [Inject]
    private void Construct(DataHolderTestZ holderTestZ)
    {
        _holderTestZ = holderTestZ;
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
        UnitManager.Instance.OnLastEnemyDeath += StartNewWave;
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
            var playerModel = new PlayerModel(_playerController.gameObject.layer, _playerController.GetComponent<Renderer>().material);

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

            UnitManager.Instance.SetPlayerAtUnitManager(_playerController);
        }
    }
    private void CreateEnemys(float scaleStats)
    {
        if (_currentLvl != null)
        {
            var enemysList = new List<EnemyController>();
            for (int i = 0; i < _currentLvl.EnemyTypes.Count; i++) // пон€ть зачем € оставил _currentLvl.EnemyTypes пустым
            {
                for (int f = 0; f < _currentLvl.EnemyCount; f++)
                {
                    // вместе с параметрами, во врага будет также передаватс€ его моделька
                    // изменить лист _currentLvl.EnemyTypes[i] на лист голых префабов врагов, без скриптов
                    var enemyController = _diContainer.InstantiatePrefab(_enemyPref, _enemyAnchor);
                    EnemyTypes type = _currentLvl.EnemyTypes[i]; 
                    _enemyfactoryDic[type].CreateEnemy(enemyController, _baseCharacterStats, scaleStats);
                    enemysList.Add(_enemyfactoryDic[type].GetEnemy());
                    foreach (var enemy in enemysList)
                    {
                        Debug.Log(enemy.CheckModel());
                    }
                }
            }
            UnitManager.Instance.SetEnemysAtUnitManager(enemysList);
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
            UnitManager.Instance.LvlEnd();
            return;
        }
        UnitManager.Instance.TeleportWaveEnemys(_waves[_currentWave].Points);
        _currentWave++;
    }
    private void LvlEnd()
    {
        SceneManager.LoadScene(0);
    }
}
