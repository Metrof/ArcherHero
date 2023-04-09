using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class UnitController<V, M> 
    where V : UnitView 
    where M : UnitModel
{
    public delegate void DeathDelegate(UnitBody unitBody);
    public event DeathDelegate OnEnablePerson;

    protected V _view;
    protected M _model;
    protected UnitBody _body;
    protected Vector3 _defoltPosition;
   public UnitController(V view, M model, UnitBody body, Vector3 startPos)
   {
        _view = view;
        _model = model;
        _body = body;
        _defoltPosition = startPos;
        _body.OnDeath += Death;
        Enable();
    }
    protected virtual void Enable()
    {

    }
    protected virtual void Death()
    {
        OnEnablePerson?.Invoke(_body);
        Disable();
    }
    protected virtual void Disable()
    {

    }
}
