using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private bool _pauseActive;
    [SerializeField] private GameObject _pausePanel;
    Button _pauseButton;

    private void Awake()
    {
        _pauseButton = GetComponent<Button>();
        _pauseButton.onClick.AddListener(PauseGame);
        if(_pausePanel != null ) _pausePanel.SetActive(false);
    }
    private void PauseGame()
    {
        if (_pauseActive)
        {
            if (_pausePanel != null) _pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            if (_pausePanel != null) _pausePanel.SetActive(false);
        }
    }
}
