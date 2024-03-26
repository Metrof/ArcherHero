using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    private float _dashForse = 20;
    private float _dashTime = 0.3f;
   
    public List<IPerk> perks;
    private List<PerkManager.PerkType> purchasedPerks;
    private List<IPerk> purchasedPerks1;

    

    public Dash(float colldown, List<IPerk> purchasedPerks1) : base(colldown)
    {
        this.purchasedPerks1 = purchasedPerks1;
    }

    //public Dash(float colldown, List<IPerk> purchasedPerks) : base(colldown)
    //{
    //    _purchasedPerks = purchasedPerks;
    //}

    public override void Action(PlayerController player)
    {
        player.StartCoroutine(DashCorotine(player));
        player.StartStun(_dashTime);


       
        ApplyPerks(player, perks);
        //ApplyPerkModifications(player);
        //ApplyPerks(player);
    }

    private void ApplyPerks(PlayerController player, List<IPerk> perks)
    {
        
        foreach (var perk in purchasedPerks1)
        {
            perk.Apply(player);
        }
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
