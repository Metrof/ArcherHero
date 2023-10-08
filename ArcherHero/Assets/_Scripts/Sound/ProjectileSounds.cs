public class ProjectileSounds
{
    private readonly ProjectileOwner _owner;

    public int NumSoundHit { get => GetNumSoundHit(_owner); }
    public int NumSoundShot {  get => GetNumSoundShot(_owner); }

    public ProjectileSounds(ProjectileOwner owner)
    {
        _owner = owner;
    }

    private int GetNumSoundHit(ProjectileOwner owner)
    {
        switch (owner)
        {
            case ProjectileOwner.Player:
                return 0;
            case ProjectileOwner.SimpleEnemy:
                return 0;
            case ProjectileOwner.Boss:
                return 0;
            default:
                return 0;
        }
    }
    private int GetNumSoundShot(ProjectileOwner owner)
    {
        switch (owner)
        {
            case ProjectileOwner.Player:
                return 1;
            case ProjectileOwner.SimpleEnemy:
                return 1;
            case ProjectileOwner.Boss:
                return 1;
            default:
                return 1;
        }
    }
}
