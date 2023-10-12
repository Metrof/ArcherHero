public class ProjectileSounds
{
    private readonly ProjectileOwner _owner;

    public int NumSoundShot {  get => GetNumSoundShot(_owner); }

    public ProjectileSounds(ProjectileOwner owner)
    {
        _owner = owner;
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
