using DG.Tweening;
using UnityEngine;

public class MovementHingedTrajectory : IProjectileMovement
{
    private const float _jumpPower = 5;
    private const int _numJumps = 1;

    public void Move(Projectile projectile, Vector3 target, float speed)
    {
        projectile.MoveSequence
            .Append(projectile.transform.DOJump(target, _jumpPower, _numJumps, speed * 0.5f)
            .SetEase(Ease.Linear));
        projectile.MoveSequence
            .AppendCallback(()=> projectile.ProjectileHit
            .Hit(projectile.GetComponent<Collider>(), projectile));
    }
}
