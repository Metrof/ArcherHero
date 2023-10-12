
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
   [SerializeField] private Image _filler;
   [SerializeField] protected Entity _entity;
   [SerializeField] private Canvas _canvas;
   
   protected float _currentFillAmount;
   

   public virtual void Start()
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

   private void LateUpdate()
   {
       _canvas.transform.rotation = Quaternion.Euler(90f, transform.rotation.y, 0f);
   }
}
