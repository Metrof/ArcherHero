using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSwitch : MonoBehaviour
{
    [SerializeField][Range(-1, 1)] private int _swipeDir = 0;
    [SerializeField] private TextMeshPro _text;
    Button _switchButton;

    private void Awake()
    {
        _switchButton = GetComponent<Button>();
        _switchButton.onClick.AddListener(SwitchLvlNum);
        if (_text != null) _text.text = "Level " + (DataHolder.LvlStart + 1);
    }

    private void SwitchLvlNum()
    {
        DataHolder.LvlStart += _swipeDir;
        if(_text != null) _text.text = "Level " + (DataHolder.LvlStart + 1);
    }
}
