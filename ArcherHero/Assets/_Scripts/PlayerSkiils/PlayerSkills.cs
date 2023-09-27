using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerSkills
{
    private readonly List<Skill> _skills;
    private readonly Controller _controller;
    private readonly Player _player;

    public PlayerSkills(Player player, Controller controller, List<Skill> skills)
    {
        _player = player;
        _controller = controller;
        _skills = skills;
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
        if (_skills != null && _skills.Count > 0)
        {
            _skills[0].Activate(_player);
        }
    }
}
