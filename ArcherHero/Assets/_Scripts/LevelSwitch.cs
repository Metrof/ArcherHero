using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class LevelSwitch : MonoBehaviour
{
    [SerializeField][Range(-1, 1)] private int _swipeDir = 0;
    [SerializeField] private TextMeshPro _text;
    Button _switchButton;

    private DataHolderTestZ _holderTestZ;

    [Inject]
    private void Construct(DataHolderTestZ holderTestZ)
    {
        _holderTestZ = holderTestZ;
    }

    private void Awake()
    {
        _holderTestZ.LvlStart += 1;
        _switchButton = GetComponent<Button>();
        _switchButton.onClick.AddListener(SwitchLvlNum);
        if (_text != null) _text.text = "Level " + (_holderTestZ.LvlStart + 1);
    }

    private void SwitchLvlNum()
    {
        _holderTestZ.LvlStart += _swipeDir;
        if(_text != null) _text.text = "Level " + (_holderTestZ.LvlStart + 1);
    }
}
