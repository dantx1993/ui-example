using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAvatarItem : MonoBehaviour
{
    [SerializeField] private Image _avatar;
    [SerializeField] private GameObject _check;
    [SerializeField] private Button _button;

    private int _dataIndex;
    private System.Action<int> _onClicked;

    private void OnEnable() 
    {
        _button.onClick.AddListener(OnClicked);
    }
    private void OnDisable() 
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    public void Initialize(int dataIndex, bool isChoosen, System.Action<int> onClicked)
    {
        _dataIndex = dataIndex;
        _avatar.sprite = DataManager.Instance.Avatars[_dataIndex];
        _check.SetActive(isChoosen);
        _onClicked = onClicked;
    }

    public void SetCheck(int dataIndex)
    {
        _check.SetActive(_dataIndex == dataIndex);
    }

    private void OnClicked()
    {
        _onClicked?.Invoke(_dataIndex);
    }
}
