using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeathHandler))]
[RequireComponent(typeof(ProjectilePull))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private UnitBody _playerPref;
    [SerializeField] private UnitBody _enemyPref;

    [SerializeField] private Transform _defoltPlayerPos;
    [SerializeField] private float _mapDiagonalSize = 39;

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
        CreateEnemys();
        CreatePlayer();

        _playerController.StartNewLvl(_enemyDeathList);
    }

    private void CreatePlayer()
    {
        if (_playerPref != null)
        {
            _playerBody = Instantiate(_playerPref, Vector3.zero, Quaternion.identity);
            _playerModel = new PlayerModel(_mapDiagonalSize, _playerBody.gameObject.layer);
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
            for (int i = 0; i < 3; i++)
            {
                UnitBody enemyBody = Instantiate(_enemyPref, new Vector3(1.5f * i, 1.5f, 2 * i), Quaternion.identity);
                EnemyModel model = new EnemyModel(_mapDiagonalSize, enemyBody.gameObject.layer);
                var enemyView = enemyBody.GetComponent<EnemyView>();

                var enemyController = new EnemyController(enemyView, model, enemyBody, new Vector3(0, 1, 0), new Vector3(-50, 0, 0));
                _enemys.Add(enemyController);

                enemyController.OnEnablePerson += _deathHandler.DeleteBodyFromTargetList;
                _enemyDeathList.Add(enemyBody);
            }
            _deathHandler.SetTargetList(_enemyDeathList);
        }
    }
}
