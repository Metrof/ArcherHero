using UnityEngine;

public class ItemChangeBulletType : DropItem
{
    [SerializeField]
    private TypeDamage _typeDamage;

    protected override void TakeItem(Player player)
    {
        player.StopAttack();
        player.SetNewProjectile(_typeDamage, _timeActionInSeconds);

        Destroy(gameObject);
    }
}
