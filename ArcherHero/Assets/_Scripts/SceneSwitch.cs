using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{   
    [SerializeField] private GameObject _magazinMenu;

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    } 

    public void OpenMagazin()
    {
        _magazinMenu.SetActive(true);
    }

    public void ClosedMagazin()
    {
        _magazinMenu.SetActive(false);
    }
}
