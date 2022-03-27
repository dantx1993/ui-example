using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class GuildItemUI : MonoBehaviour
{
    private const string NAME_PATTERN = "<b>Name:</b> <i><color=\"red\">{0}</i>";
    private const string DESC_PATTERN = "<b>Description:</b> <i><color=\"yellow\">{0}</i>";

    [SerializeField] private Image _avatar;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Button _button;

    private int _dataIndex;
    private System.Action<int> _onClicked;

    public int DataIndex => _dataIndex;

    private void OnEnable() 
    {
        _button.onClick.AddListener(OnButtonClicked);
    }
    private void OnDisable() 
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Initialize(int dataIndex, System.Action<int> onClicked)
    {
        _dataIndex = dataIndex;
        _onClicked = onClicked;
        GuildData data = DataManager.Instance.GuildDatas[_dataIndex];
        _avatar.sprite = DataManager.Instance.Avatars[data.avatarIndex];
        _name.text = string.Format(NAME_PATTERN, data.name);
        _description.text = string.Format(DESC_PATTERN, data.description);
    }

    private void OnButtonClicked()
    {
        _onClicked?.Invoke(_dataIndex);
    }
}
