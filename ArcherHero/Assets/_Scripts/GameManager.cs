using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeathHandler))]
[RequireComponent(typeof(ProjectilePull))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPref;
    [SerializeField] private CharacterStatsE _baseCharacterStats;
    [SerializeField] private EnemyController _enemyPref;
    
    [SerializeField] private Transform _defoltPlayerPos;
    [SerializeField] private Transform _enemyAnchor;
    [SerializeField] private Vector3 _enemyPullPos;
    [SerializeField] private float _mapDiagonalSize = 39;

    private PlayerController _playerController;
    private List<EnemyController> _enemys = new List<EnemyController>();

    private PlayerModel _playerModel;
    private DeathHandler _deathHandler;

    private List<Transform> _playerDeathList = new List<Transform>();
    private List<Transform> _enemyDeathList = new List<Transform>();


    private void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
    }

    public void Start()
    {
        CreateEnemys();
        CreatePlayer();

        _playerController.StartNewLvl(_enemyDeathList);
    }

    private void CreatePlayer()
    {
        if (_playerPref != null)
        {
            _playerController = Instantiate(_playerPref, _defoltPlayerPos.position, Quaternion.identity);
            var playerView = _playerController.GetComponent<PlayerView>();
            _playerModel = new PlayerModel(_mapDiagonalSize, _playerController.gameObject.layer);

            _playerController.Init(playerView, _playerModel, _defoltPlayerPos.position);
            _playerController.SetNewModelParram(_baseCharacterStats);
            _playerController.OnEnablePerson += _deathHandler.DeleteBodyFromTargetList;

            _playerDeathList.Add(_playerController.transform);
            _deathHandler.SetTargetList(_playerDeathList);
        }
    }
    private void CreateEnemys()
    {
        if (_enemyPref != null)
        {
            for (int i = 0; i < 3; i++)
            {
                EnemyController enemyController = Instantiate(_enemyPref, new Vector3(1.5f * i, 1.5f, 2 * i), Quaternion.identity);
                EnemyModel model = new EnemyModel(_mapDiagonalSize, enemyController.gameObject.layer);
                var enemyView = enemyController.GetComponent<EnemyView>();

                enemyController.gameObject.transform.SetParent(_enemyAnchor);
                enemyController.Init(enemyView, model, new Vector3(0, 1, 0));
                enemyController.SetPullPos(_enemyPullPos);
                enemyController.SetNewModelParram(_baseCharacterStats);
                _enemys.Add(enemyController);

                enemyController.OnEnablePerson += _deathHandler.DeleteBodyFromTargetList;
                _enemyDeathList.Add(enemyController.transform);
            }
            _deathHandler.SetTargetList(_enemyDeathList);
        }
    }
}
