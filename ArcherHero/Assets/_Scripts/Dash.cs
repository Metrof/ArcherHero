using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    private float _dashForse = 20;
    private float _dashTime = 0.3f;

    public Dash(float colldown) : base(colldown)
    {
    }

    public override void Action(PlayerController player)
    {
        player.StartCoroutine(DashCorotine(player));
        player.StartStun(_dashTime);
    }
    IEnumerator DashCorotine(PlayerController player)
    {
        float dashEnd = Time.time + _dashTime;
        player.Rigidbody.drag = 2;
        player.Rigidbody.AddForce(player.Rigidbody.velocity.normalized * _dashForse, ForceMode.Impulse);
        while (dashEnd > Time.time)
        {
            yield return null;
        }
        player.Rigidbody.drag = 0;
        player.Rigidbody.velocity = Vector3.zero;
    }
}
