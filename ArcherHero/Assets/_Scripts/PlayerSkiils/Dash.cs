using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "PlayerSkills", menuName = "createSkill/Dash")]
public sealed class Dash : Skill
{
    [SerializeField] private float _distance = 1f;
    [SerializeField] private float _dashSpeed = 1f;

    private LayerMask _checkLayerMask = 1 << 0;
    Sequence s;
    public override void Activate(Player player)
    {
        s = DOTween.Sequence();
        Vector3 dashTargetDir = new Vector3(player.MoveDirection.x, 0, player.MoveDirection.y);
        if (dashTargetDir == Vector3.zero) dashTargetDir = player.transform.forward;

        Vector3 dashTargetPos = dashTargetDir * _distance;

        RaycastHit hit;

        Ray ray = new Ray(player.transform.position, dashTargetPos - player.transform.position);
        Physics.Raycast(ray, out hit, _distance);
        if (hit.collider != null)
        {
            if ((_checkLayerMask.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                float distanceToWall = Vector3.Distance(player.transform.position, hit.point);
                distanceToWall -= player.ColliderRadius;
                Debug.Log(distanceToWall);
                dashTargetPos = dashTargetDir * distanceToWall;
            }
        }

        float finalSpeed = CalculateMovement.CalculateMoveTime(player.transform.position, dashTargetPos, _dashSpeed);

        s.Append(player.transform.DOMove(dashTargetPos, _dashSpeed)).SetRelative() 
            .OnStart(player.PlayerDisable)
            .OnComplete(player.PlayerEnable);
        s.Insert(0, player.transform.DOLocalRotate(player.transform.up * 360, _dashSpeed, RotateMode.FastBeyond360)
            .SetRelative(true)
        .SetEase(Ease.InOutQuint));
    }
}
