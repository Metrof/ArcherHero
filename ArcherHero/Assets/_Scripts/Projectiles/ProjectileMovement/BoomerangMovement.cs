using DG.Tweening;
using UnityEngine;

public class BoomerangMovement : IProjectileMovement
{
    private const float moveDistance = 2;

    public void Move(Projectile projectile, Vector3 target, float speed)
    {
        Vector3 movePoint = target + projectile.transform.forward * moveDistance;

        float time = CalculateMovement.CalculateMoveTime(projectile.transform.position, movePoint, speed);

        projectile.MoveSequence
            .Append(projectile.transform.DOMove(movePoint, time)
            .SetEase(Ease.OutCubic)
            .SetLoops(2, LoopType.Yoyo));
        projectile.MoveSequence
            .AppendCallback(() => projectile.ProjectilePool.Release(projectile));
    }
}
