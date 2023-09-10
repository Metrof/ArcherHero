public class ItemRandomProjectilePattern : DropItem
{
    private ProjectileCreationType _creation;
    private ProjectileMovementType _movement;
    private ProjectileHitType _hit;

    protected override void TakeItem(Player player)
    {
        SetRandomValue();

        player.Weapon.ProjectilePattern
            .SetCreation(_creation)
            .SetMovement(_movement)
            .SetHit(_hit);

        Destroy(gameObject);
    }

    private void SetRandomValue()
    {
        _creation = new System.Random().RandomEnumValue<ProjectileCreationType>();
        _movement = new System.Random().RandomEnumValue<ProjectileMovementType>();
        _hit = new System.Random().RandomEnumValue<ProjectileHitType>();
    }
}
