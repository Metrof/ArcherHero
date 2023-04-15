using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitController<V, M> : MonoBehaviour
    where V : UnitView 
    where M : UnitModel
{
    public delegate void DeathDelegate(Transform unitController);
    public event DeathDelegate OnEnablePerson;

    private Coroutine _attackCoroutine;

    protected V _view;
    protected M _model;
    protected Vector3 _defoltPosition;
   public void Init(V view, M model, Vector3 startPos)
   {
        _view = view;
        _model = model;
        _defoltPosition = startPos;


        _model.OnDeath += Death;
        _model.OnAttackModel += ModelAttack;
        Enable();
    }
    protected virtual void Enable()
    {
        _model.OnStopAttack += StopAttacking;
        _model.OnStartAttack += StartAttacking;
    }
    protected virtual void ModelAttack()
    {
        _model.ChangeTarget(transform.position);
    }
    private void StartAttacking()
    {
        _attackCoroutine = StartCoroutine(AttackCorotine());
    }
    private void StopAttacking()
    {
        StopCoroutine(_attackCoroutine);
    }
    IEnumerator AttackCorotine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_model.AttackDellay);
            _model.Attack(transform.position);
        }
    }


    public void LvlStart(List<Transform> enemies)
    {
        SetEnemyPull(enemies);
        TransformToDefoltPos();
        _model.ChangeTarget(transform.position);
    }
    public void SetEnemyPull(List<Transform> enemies)
    {
        _model.SetPull(enemies);
    }
    public void TransformToDefoltPos()
    {
        Teleportation(_defoltPosition);
    }


    public void SetNewModelParram(CharacterStatsE stats)
    {
        _model.SetStats(stats);
    }
    public void ChangeModelParram(CharacterStatsE stats)
    {
        _model.ChangeStats(stats);
    }




    public void Teleportation(Vector3 newPos)
    {
        transform.position = newPos;
    }
    protected virtual void Death()
    {
        OnEnablePerson?.Invoke(transform);
        StopAttacking();
        Disable();
    }
    protected virtual void Disable()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            foreach (var effect in projectile.Effects)
            {
                effect.GetEffect(ChangeModelParram);
            }
        }
    }
}
