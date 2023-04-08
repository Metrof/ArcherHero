using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPref;

    private PlayerController _playerController;
    private PlayerModel _playerModel;
    private UnitBody _playerBody;

    public void Start()
    {
        if (_playerPref != null)
        {
            _playerModel = new PlayerModel();

            var playerObject = Instantiate(_playerPref, Vector3.zero, Quaternion.identity);
            var playerView = playerObject.GetComponent<PlayerView>();
            _playerBody = playerObject.GetComponent<UnitBody>();

            _playerController = new PlayerController(playerView, _playerModel, _playerBody);
        }
    }
}
