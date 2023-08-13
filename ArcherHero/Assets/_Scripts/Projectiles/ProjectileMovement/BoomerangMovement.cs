using DG.Tweening;
using UnityEngine;

public class BoomerangMovement : IProjectileMovement
{
    private const float moveDistance = 2;

    public void Move(Projectile projectile, Transform target, float speed)
    {
        Vector3 movePoint = target.transform.position + projectile.transform.forward * moveDistance;
        projectile.MoveSequence
            .Append(projectile.transform.DOMove(movePoint, speed * 0.5f)
            .SetEase(Ease.OutCubic)
            .SetLoops(2, LoopType.Yoyo));
        projectile.MoveSequence
            .AppendCallback(() => projectile.ProjectilePool.Release(projectile));
    }
}
