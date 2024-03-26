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

    protected bool _isMoving = true;
    protected Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get { return _rigidbody ??= GetComponent<Rigidbody>(); } }   
    public virtual void Init(V view, M model, Vector3 startPos)
   {
        _view = view;
        _model = model;
        _defoltPosition = startPos;


        _model.OnDeath += Death;
        _model.OnAttackModel += ModelAttack;
        _model.OnStopAttack += StopAttacking;
        _model.OnStartAttack += StartAttacking;
    }

    public bool CheckModel()
    {
        return _model != null;
    }
    protected virtual void ModelAttack()
    {
        _model.ChangeTarget(transform.position);
    }

    protected virtual void EnableUnit()
    {
        _isMoving = true;
        StartAttacking();
    }
    protected virtual void DisableUnit()
    {
        _isMoving = false;
        StopAttacking();
    }

    private void StartAttacking()
    {
        StopAttacking();
        _attackCoroutine = StartCoroutine(AttackCorotine());
    }
    private void StopAttacking()
    {
        if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
    }
    IEnumerator AttackCorotine()
    {
        if (!_model.ThereIsTarget) StopAttacking();
        while (true)
        {
            yield return new WaitForSeconds(_model.AttackDellay);
            _model.Attack(transform);
        }
    }


    public void StartStun(float enableTime)
    {
        StartCoroutine(StunCorotine(enableTime));
    }
    IEnumerator StunCorotine(float enableTime)
    {
        DisableUnit();
        float endEnableTime = Time.time + enableTime;
        while (endEnableTime > Time.time)
        {
            yield return null;
        }
        EnableUnit();
    }


    public void LvlStart()
    {
        TransformToDefoltPos();
        _model.ChangeTarget(transform.position);
    }
    public void WaveStart()
    {
        StartAttacking();
    }
    public void WaveStop()
    {
        StopAttacking();
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
        _model.Restats();
        OnEnablePerson?.Invoke(transform);
        StopAttacking();
        DisableUnit();
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
