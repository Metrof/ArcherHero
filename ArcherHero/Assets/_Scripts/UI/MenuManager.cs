using System.Collections.Generic;
using UnityEngine;



public class MenuManager : MonoBehaviour
{   
    [SerializeField] private List<GameObject> objectOn;
    [SerializeField] private List<GameObject> objectOff;

    public void OnClickButton()
    {
        if(objectOff != null)
        {
            foreach (GameObject offObject in objectOff)
            {
                offObject.gameObject.SetActive(false);
            }
        }
        if(objectOn != null)
        {
            foreach (GameObject onObject in objectOn)
            {
                onObject.gameObject.SetActive(true);
            }
        }   
    }
}
