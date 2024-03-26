using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : Skill
{
    private float _parryTime = 1;
    private GameObject _dagger;
    public Parry(GameObject daggerObj, float colldown) : base(colldown)
    {
        _dagger = daggerObj;
        _dagger.SetActive(false);
    }

    public override void Action(PlayerController player)
    {
        player.StartCoroutine(ParryCorotine(player));
    }
    IEnumerator ParryCorotine(PlayerController player)
    {
        float parryEnd = Time.time + _parryTime;
        _dagger.SetActive(true);
        while (parryEnd > Time.time)
        {
            yield return null;
        }
        _dagger.SetActive(false);
    }
}
