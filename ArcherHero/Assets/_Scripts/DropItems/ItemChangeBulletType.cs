using UnityEngine;

public class ItemChangeBulletType : DropItem
{
    private const ProjectileOwner _owner = ProjectileOwner.Player;

    [SerializeField]
    private TypeDamage _typeDamage;

    protected override void TakeItem(Player player)
    {
        player.Weapon.ChangeProjectiles(player.ProjectilePool.GetPool(_owner, _typeDamage));

        Destroy(gameObject);
    }
}
