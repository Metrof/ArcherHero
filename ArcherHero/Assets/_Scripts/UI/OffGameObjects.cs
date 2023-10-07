using System.Collections.Generic;
using UnityEngine;

public class OffGameObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects = new List<GameObject>();

    private void OnEnable()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
