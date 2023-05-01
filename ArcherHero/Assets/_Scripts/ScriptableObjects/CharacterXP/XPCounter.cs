
using TMPro;
using UnityEngine;
using ScriptableObjects;

public class XPCounter : MonoBehaviour
{
    [SerializeField] private CharacterSkills _characterSkills;
    [SerializeField] private TextMeshProUGUI _XPText;

    private void Start()
    {
        UpdateXPCounter();
    }

    public void UpdateXPCounter()
    {
        _XPText.text = $"XP points: {_characterSkills.SkillPoints}";
    }
}

