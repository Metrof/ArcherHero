using System;
using System.Collections.Generic;

public class ResourceBank
{
    public readonly IStorageService StorageService;

    private readonly Dictionary<ResourceType, Resource> _resources;

    private readonly int _startMoney;
    private readonly int _startGoldCoins;

    public ResourceBank(IStorageService storageService)
    {
        StorageService = storageService;

        LoadResources();

        _resources = CreateResources();
    }

    private void LoadResources()
    {
        StorageService.Load<int>(ResourceType.Money.ToString(), (money) => money = _startMoney);
        StorageService.Load<int>(ResourceType.GoldCoins.ToString(), (goldCoins) => goldCoins = _startGoldCoins);
    }

    private Dictionary<ResourceType, Resource> CreateResources()
    {
        return new Dictionary<ResourceType, Resource>
        {
            [ResourceType.Money] = new Resource(_startMoney),
            [ResourceType.GoldCoins] = new Resource(_startGoldCoins)
        };
    }

    public void SubscribeToChangeResource(ResourceType type, Action<int> OnChanged)
    {
        _resources[type].OnChangedEvent += OnChanged;
    }

    public void UnSubscribeToChangeResource(ResourceType type, Action<int> OnChanged)
    {
        _resources[type].OnChangedEvent -= OnChanged;
    }

    public void AddResource(ResourceType type, int value)
    {
        CheckIfValueIsPositive(value);

        var resource = _resources[type];

        resource.Amount += value;
    }

    public bool TrySpendResource(ResourceType type, int value)
    {
        CheckIfValueIsPositive(value);

        if (HasResource(type, value))
        {
            var resource = _resources[type];
            resource.Amount -= value;

            return true;
        }

        return false;
    }

    public bool HasResource(ResourceType type, int value)
    {
        var resource = _resources[type];

        return resource.Amount >= value;
    }

    public int GetResourceValue(ResourceType type)
    {
        return _resources[type].Amount;
    }

    private void CheckIfValueIsPositive(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }

    public void SaveResources()
    {
        foreach (var resource in _resources)
        {
            StorageService.Save(resource.Key.ToString(), resource.Value);
        }
    }
}
