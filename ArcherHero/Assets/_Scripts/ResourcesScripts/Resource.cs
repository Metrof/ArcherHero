using System;

public class Resource
{
    public event Action<int> OnChangedEvent;
    
    private int _amount;

    public int Amount { 
        get => _amount;
        set
        {
            if(value != _amount)
            {
                OnChangedEvent?.Invoke(value);
            }

            _amount = value;
        } 
    }

    public Resource(int amountByDefault = default)
    {
        Amount = amountByDefault;
    }
}
