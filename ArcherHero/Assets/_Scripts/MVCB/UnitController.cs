using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController<V, M> 
    where V : UnitView 
    where M : UnitModel
{
    protected V _view;
    protected M _model;
    protected UnitBody _body;
   public UnitController(V view, M model, UnitBody body)
   {
        _view = view;
        _model = model;
        _body = body;
   }
    public virtual void Death()
    {

    }
}
