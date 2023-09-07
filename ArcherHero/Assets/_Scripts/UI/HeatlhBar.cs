
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
   [SerializeField] private Image _filler;
   [SerializeField] private Entity _entity;
   //[SerializeField] private Transform _camera;
   private float _currentFillAmount;
   

   void Start()
   {
       _currentFillAmount = _entity.currentHealth;
   }

   void OnEnable()
   {
        _entity.OnTakeDamage += ChangeHealth;
   }
   void OnDisable()
   {
        _entity.OnTakeDamage -= ChangeHealth;
   }


   private void ChangeHealth(int currentHealth)
   {    
       _filler.fillAmount =  currentHealth / _currentFillAmount;
   }

   /*private void LateUpdate()
   {
       _camera.transform.LookAt(transform.position + _camera.forward);
   }*/
}
