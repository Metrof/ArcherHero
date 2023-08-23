using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkills", menuName = "createSkill/Dash")]
public sealed class Dash : Skill
{
    [SerializeField] private float _distance = 1f;
    public override void Activate(Player player)
    {
        
    }
}
