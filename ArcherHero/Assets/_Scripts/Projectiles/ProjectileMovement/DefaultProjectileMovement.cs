using DG.Tweening;
using UnityEngine;

public class DefaultProjectileMovement : IProjectileMovement
{
    private const float moveDistance = 50f;
    public void Move(Projectile projectile, Transform target, float speed)
    {
        Vector3 movePoint = projectile.transform.position + projectile.transform.forward * moveDistance;
        projectile.MoveSequence
            .Append(projectile.transform.DOMove(movePoint, speed)
            .SetEase(Ease.Linear));
        projectile.MoveSequence
            .AppendCallback(() => projectile.ProjectilePool.Release(projectile));
    }
}
