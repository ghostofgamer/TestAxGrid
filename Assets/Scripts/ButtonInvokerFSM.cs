using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonInvokerFSM : MonoBehaviourExt
{
    [SerializeField] private string _buttonName;
    [SerializeField] private string _eventName;

    private Button _button;

    [OnAwake]
    private void Init()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        Settings.Model.EventManager.AddAction<bool>(_buttonName, SetInteractable);
    }

    [Bind("{_buttonName}")]
    private void SetInteractable(bool value) => _button.interactable = value;

    private void OnClick()
    {
        if (!string.IsNullOrEmpty(_eventName))
            Settings.Invoke(_eventName);
    }
}