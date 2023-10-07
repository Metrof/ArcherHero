using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [SerializeField] protected float _cooldown = 1f;

    public bool IsReady = true;
    public float Cooldown { get => _cooldown; }

    public abstract void Activate(Player player);
}
