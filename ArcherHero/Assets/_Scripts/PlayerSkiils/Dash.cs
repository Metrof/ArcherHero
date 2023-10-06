using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkills", menuName = "createSkill/Dash")]
public sealed class Dash : Skill
{
    [SerializeField] private float _distance = 1f;
    [SerializeField] private float _dashTime = 1f;

    private LayerMask _checkLayerMask = 1 << 0;
    Sequence s;
    public override void Activate(Player player)
    {
        s = DOTween.Sequence();
        Vector3 dashTargetDir = new Vector3(player.MoveDirection.x, 0, player.MoveDirection.y);
        if (dashTargetDir == Vector3.zero)
        {
            return;
        }

        Vector3 dashTargetDis = dashTargetDir * _distance;

        RaycastHit[] hits;

        Ray ray = new Ray(player.transform.position, dashTargetDir);
        hits = Physics.RaycastAll(ray, _distance);

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if ((_checkLayerMask.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    float distanceToWall = Vector3.Distance(player.transform.position, hit.point);
                    distanceToWall -= player.ColliderRadius;
                    dashTargetDis = dashTargetDir * distanceToWall;
                    break;
                }
            }
        }

        s.Append(player.transform.DOMove(dashTargetDis, _dashTime))
            .SetRelative()
            .SetEase(Ease.InQuad)
            .OnStart(player.PlayerDisable)
            .OnComplete(player.PlayerEnable);
        s.Insert(0, player.transform.DOPunchScale(new Vector3(-0.6f, -0.6f, -0.6f), _dashTime, 2)
            .SetRelative(true)
            .SetEase(Ease.Linear));
    }
}
