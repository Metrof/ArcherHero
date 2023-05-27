using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private static int _lvlCount;
    public static int LvlCount { get { return _lvlCount; } }

    private void Awake()
    {
        _lvlCount = _levels.Length;
        _cameraController = GetComponent<CameraController>();
        _currentLvl = _levels[DataHolder.LvlStart];
    }

    public void Start()
    {
        LvlInit();
        CreateEnemys(_currentLvl.EnemyPower);
        CreatePlayer(DataHolder.PlayerStats);
        _cameraController.SetTarget(_playerController.transform);
    }

    private void CreatePlayer(CharacterStatsE playerStats)
    {
        if (_playerPref != null)
        {
            _playerController = Instantiate(_playerPref, _defoltPlayerPos.position, Quaternion.identity);
            var playerView = _playerController.GetComponent<PlayerView>();
            var playerModel = new PlayerModel(_mapDiagonalSize, _playerController.gameObject.layer, _playerController.GetComponent<Renderer>().material);

            if (playerStats == null) playerStats = _baseCharacterStats;
            _playerController.Init(playerView, playerModel, _defoltPlayerPos.position);
            _playerController.SetNewModelParram(playerStats);

            Dagger dagger = Instantiate(_dagger);
            dagger.SetOwner(_playerController.transform);
            _playerController.SetFirstSkill(new Dash(2));
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
            for (int i = 0; i < _currentLvl.Enemies.Count; i++)
            {
                for (int f = 0; f < _currentLvl.EnemyCount; f++)
                {
                    var enemyController = Instantiate(_currentLvl.Enemies[i], _enemyAnchor);
                    var model = new EnemyModel(_mapDiagonalSize, enemyController.gameObject.layer, enemyController.GetComponent<Renderer>().material);
                    var enemyView = enemyController.GetComponent<EnemyView>();
                    var enemyStats = _baseCharacterStats;
                    enemyStats.ScaleStats(scaleStats);

                    enemyController.Init(enemyView, model, _enemyPullPos);
                    enemyController.SetEnemyType(_currentLvl.Enemies[i].Type);
                    enemyController.SetNewModelParram(enemyStats);
                    enemyController.OnLastEnemyDeath += StartNewWave;
                    enemyController.LvlStart();
                    enemysList.Add(enemyController);
                }
            }
            UnitManager.Instance.SetEnemysAtUnitManager(enemysList);
        }
    }
    private void UpdateUI()
    {

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
