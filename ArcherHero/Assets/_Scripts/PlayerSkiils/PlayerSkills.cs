using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.InputSystem;

public class PlayerSkills
{
    private readonly List<Skill> _skills;
    private readonly Controller _controller;
    private readonly Player _player;

    private CancellationTokenSource _cTS;
    public PlayerSkills(Player player, Controller controller, List<Skill> skills)
    {
        _player = player;
        _controller = controller;
        _skills = skills;
    }
    public void ResetDelay()
    {
        _cTS = new CancellationTokenSource();
    }

    public void StopDelay()
    {
        _cTS?.Cancel();
    }

    public void SubscribeToSkills()
    {
        _controller.Player.ActivateFirstSkill.performed += ActivateFirstSkill;
    }
    public void UnsubscribeToSkills()
    {
        _controller.Player.ActivateFirstSkill.performed -= ActivateFirstSkill;
    }

    private void ActivateFirstSkill(InputAction.CallbackContext context)
    {
        if (_skills == null && _skills.Count <= 0)
        {
            return;
        }
        Skill skill;
        skill = _skills[0];

        if (skill.IsReady)
        {
            skill.Activate(_player);
            ActivateSkillDelay(skill, _cTS.Token).Forget();
        }
    }

    private async UniTaskVoid ActivateSkillDelay(Skill skill, CancellationToken token)
    {
        skill.IsReady = false;
        await UniTask.Delay(TimeSpan.FromSeconds(skill.Cooldown), cancellationToken: token).SuppressCancellationThrow();
        skill.IsReady = true;
    }
}
