
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSwitch : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public Transform teleportLocation;
    private int currentObjectIndex = 1; 
    public GameObject player;
    private CharacterController characterController;
    [SerializeField] private TextMeshPro tableText;

    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {   
        
        if (other.CompareTag("Player"))
        {   
            characterController.enabled = false;
            player.transform.position = teleportLocation.position;
            characterController.enabled = true;
            
            tableText.text = "Level " + (currentObjectIndex + 1);

            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                if (i != currentObjectIndex)
                {
                    objectsToActivate[i].SetActive(false);
                }
            }

            objectsToActivate[currentObjectIndex].SetActive(true);
            currentObjectIndex++;

            if (currentObjectIndex >= objectsToActivate.Length)
            {
                currentObjectIndex = 1;
            }

           
        }
    }
}
