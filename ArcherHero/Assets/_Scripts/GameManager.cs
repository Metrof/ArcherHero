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

        _playerController.LvlStart(_enemyDeathList);
        foreach (var enemy in _enemys)
        {
            enemy.LvlStart(_playerDeathList);
        }
    }

    private void CreatePlayer()
    {
        if (_playerPref != null)
        {
            _playerController = Instantiate(_playerPref, _defoltPlayerPos.position, Quaternion.identity);
            var playerView = _playerController.GetComponent<PlayerView>();
            var playerModel = new PlayerModel(_mapDiagonalSize, _playerController.gameObject.layer, _playerController.GetComponent<Renderer>().material);

            _playerController.Init(playerView, playerModel, _defoltPlayerPos.position);
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
                var enemyController = Instantiate(_enemyPref, Vector3.zero, Quaternion.identity);
                var model = new EnemyModel(_mapDiagonalSize, enemyController.gameObject.layer, enemyController.GetComponent<Renderer>().material);
                var enemyView = enemyController.GetComponent<EnemyView>();

                enemyController.gameObject.transform.SetParent(_enemyAnchor);
                enemyController.Init(enemyView, model, new Vector3(1.5f * i, 1.5f, 2 * i));
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
