public class ItemRandomProjectilePattern : DropItem
{
    private ProjectileCreationType _creation;
    private ProjectileMovementType _movement;
    private ProjectileHitType _hit;

    protected override void TakeItem(Player player)
    {
        SetRandomValue();

        player.SetProjectilePattern(_creation, _movement, _hit, _timeActionInSeconds);

        Destroy(gameObject);
    }

    private void SetRandomValue()
    {
        _creation = new System.Random().RandomEnumValue<ProjectileCreationType>();
        _movement = new System.Random().RandomEnumValue<ProjectileMovementType>();
        _hit = new System.Random().RandomEnumValue<ProjectileHitType>();
    }
}
