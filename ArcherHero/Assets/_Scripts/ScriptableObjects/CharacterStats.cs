using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [Header("HP")]
    [SerializeField] private int _maxHP;
    [SerializeField] private int _hpUpgradeCounter;
    [SerializeField] private int _hpUpgradeLimit;
    [SerializeField] private int _hpUpgradePrice;
    [SerializeField] private int _hpUpgradeValue;
    [SerializeField] private int _hpPriceFactor;
    

    [Header("Weapon")]
    [SerializeField] private int _damage;
    [SerializeField] private int _damageUpgradeCounter;
    [SerializeField] private int _damageUpgradeLimit;
    [SerializeField] private int _damageUpgradePrice;
    [SerializeField] private int _damageUpgradeValue;
    [SerializeField] private int _damagePriceFactor;
   
   
    [SerializeField] private float _speedAttack;
    [SerializeField] private int _speedAttackUpgradeCounter;
    [SerializeField] private int _speedAttackUpgradeLimit;
    [SerializeField] private int _speedAttackUpgradePrice;
    [SerializeField] private int _speedAttackUpgradeValue;
    [SerializeField] private int _speedAttackPriceFactor;

    [SerializeField] private CharacterStatsE _baseStats;
    [SerializeField] private CharacterStatsE _hpUp;
    [SerializeField] private CharacterStatsE _speedUp;
    [SerializeField] private CharacterStatsE _attackDellayUp;


    [Header("Money")]
    [SerializeField] private int _money;


    private DataHolderTestZ _holderTestZ;

    [Inject]
    public void Construct(DataHolderTestZ holderTestZ)
    {
        _holderTestZ = holderTestZ;
    }

    public int HpUpgradeCounter => _hpUpgradeCounter;
    public int HpUpgradePrice => _hpUpgradePrice;
    public int HpUpgradeLimit => _hpUpgradeLimit;
    public int HpUpgradeValue => _hpUpgradeValue;

    public int DamageUpgradeLimit => _damageUpgradeLimit;
    public int DamageUpgradeCounter => _damageUpgradeCounter;
    public int DamageUpgradePrice => _damageUpgradePrice;
    public int DamageUpgradeValue => _damageUpgradeValue;

    public int SpeedAttackUpgradeLimit => _speedAttackUpgradeLimit;
    public int SpeedAttackUpgradeCounter => _speedAttackUpgradeCounter;
    public int SpeedAttackUpgradePrice => _speedAttackUpgradePrice;
    public int SpeedAttackUpgradeValue => _speedAttackUpgradeValue;

    public int MaxHp => _maxHP;
    public int Damage => _damage;
    public float SpeedAttack => _speedAttack;
    

    public int Money
    {
        get => _money;
        set => _money = value;
    }

    private void Awake()
    {
        _holderTestZ.SetStats(_baseStats);
    }

    public void UpgradeHp(UpgradeReaction reaction)
    {   
        if (_hpUpgradeCounter == _hpUpgradeLimit)
        {
            reaction(CharacterStorePurchaseCallback.StatIsFull);
            return;
        }
           if (_money < _hpUpgradePrice)
        {
            reaction(CharacterStorePurchaseCallback.NotEnoughMoney);
            return;
        }
            
        _hpUpgradeCounter++;
        _money -= _hpUpgradePrice;
        _maxHP += _hpUpgradeValue;
        _hpUpgradePrice += _hpUpgradeCounter * _hpPriceFactor;

        _holderTestZ.PlayerStats.AddStats(_hpUp);
                   
        reaction(CharacterStorePurchaseCallback.Upgraded);
    }
    public void UpgradeDamage(UpgradeReaction reaction)
    {   
        if (_damageUpgradeCounter == _damageUpgradeLimit)
        {
            reaction(CharacterStorePurchaseCallback.StatIsFull);
            return;
        }
        if (_money < _damageUpgradePrice)
        {
            reaction(CharacterStorePurchaseCallback.NotEnoughMoney);
            return;
        }

        _damageUpgradeCounter++;
        _money -= _damageUpgradePrice;
        _damage += _damageUpgradeValue;
        _damageUpgradePrice +=  _damageUpgradeCounter * _damagePriceFactor;
  
        reaction(CharacterStorePurchaseCallback.Upgraded);
    }

    public void UpgradeSpeedAttack(UpgradeReaction reaction)
    {   
        if (_speedAttackUpgradeCounter == _speedAttackUpgradeLimit)
        {
            reaction(CharacterStorePurchaseCallback.StatIsFull);
            return;
        }
        if (_money < _speedAttackUpgradePrice)
        {
            reaction(CharacterStorePurchaseCallback.NotEnoughMoney);
            return;
        }

        _speedAttackUpgradeCounter++;
        _money -= _speedAttackUpgradePrice;
        _speedAttack += _speedAttackUpgradeValue;
        _speedAttackUpgradePrice +=_speedAttackUpgradeCounter * _speedAttackPriceFactor;

        _holderTestZ.PlayerStats.AddStats(_attackDellayUp);

        reaction(CharacterStorePurchaseCallback.Upgraded);
    }

    public delegate void UpgradeReaction(CharacterStorePurchaseCallback callback);
} 

public enum CharacterStorePurchaseCallback
{
    StatIsFull,
    Upgraded,
    NotEnoughMoney
}



