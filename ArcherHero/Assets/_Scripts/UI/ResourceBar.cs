
using UnityEngine;
using Zenject;
using TMPro;

public class ResourceBar : MonoBehaviour
{   
    private ResourceBank _resourcesBank;
    [SerializeField] private TextMeshProUGUI _moneyText;


    [Inject]
    private void Construct(ResourceBank resourceBank)
    {
         _resourcesBank = resourceBank;
    }
    
    private void Start()
    {
        _moneyText.text =$"Money {_resourcesBank.GetResourceValue(ResourceType.Money).ToString()}";
    }
    void OnEnable()
    {
        _resourcesBank.SubscribeToChangeResource(ResourceType.Money, OnChanged);
    }
    void OnDisable()
    {
        _resourcesBank.SubscribeToChangeResource(ResourceType.Money, OnChanged);
    }

    private void OnChanged(int newValue)
    {
        _moneyText.text =$"Money {newValue.ToString()}";
    }
    

}
