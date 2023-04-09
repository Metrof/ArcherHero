using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnitBody _playerPref;
    [SerializeField] private UnitBody _enemyPref;

    [SerializeField] private Transform _defoltPlayerPos;
    [SerializeField] private float _mapDiagonalSize;

    private PlayerController _playerController;
    private List<EnemyController> _enemys = new List<EnemyController>();

    private PlayerModel _playerModel;
    private DeathHandler _deathHandler;

    private UnitBody _playerBody;
    private UnitBody _enemyBody;

    private List<UnitBody> _playerDeathList = new List<UnitBody>();
    private List<UnitBody> _enemyDeathList = new List<UnitBody>();


    private void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
    }

    public void Start()
    {
        CreatePlayer();
        CreateEnemys();

        _playerController.SetEnemyPull(_enemyDeathList);
    }

    private void CreatePlayer()
    {
        if (_playerPref != null)
        {
            _playerModel = new PlayerModel(_mapDiagonalSize);

            _playerBody = Instantiate(_playerPref, Vector3.zero, Quaternion.identity);
            var playerView = _playerBody.GetComponent<PlayerView>();

            _playerController = new PlayerController(playerView, _playerModel, _playerBody, _defoltPlayerPos.position);
            _playerController.OnEnablePerson += _deathHandler.DeleteBodyFromTargetList;

            _playerDeathList.Add(_playerBody);
            _deathHandler.SetTargetList(_playerDeathList);
        }
    }
    private void CreateEnemys()
    {
        if (_enemyPref != null)
        {
            EnemyModel model = new EnemyModel(_mapDiagonalSize);

            _enemyBody = Instantiate(_enemyPref, Vector3.zero, Quaternion.identity);
            var enemyView = _enemyBody.GetComponent<EnemyView>();

            var enemyController = new EnemyController(enemyView, model, _enemyBody, new Vector3(0, 2, 0));
            _enemys.Add(enemyController);

            enemyController.OnEnablePerson += _deathHandler.DeleteBodyFromTargetList; 
            _enemyDeathList.Add(_enemyBody);
            _deathHandler.SetTargetList(_playerDeathList);
        }
    }
}
