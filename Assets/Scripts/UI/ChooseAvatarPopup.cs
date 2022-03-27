using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAvatarPopup : MonoBehaviour
{
    [SerializeField] private ChooseAvatarItem _itemPrefab;
    [SerializeField] private Transform _itemParent;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    private int _currentDataIndex;
    private System.Action<int> _onHide;
    private List<ChooseAvatarItem> _chooseAvatarItems = new List<ChooseAvatarItem>();

    private void OnEnable() 
    {
        _confirmButton.onClick.AddListener(OnButtonBackClicked);
        _cancelButton.onClick.AddListener(OnButtonBackClicked);
    }
    private void OnDisable() 
    {
        _confirmButton.onClick.RemoveListener(OnButtonBackClicked);
        _cancelButton.onClick.RemoveListener(OnButtonBackClicked);    
    }

    public ChooseAvatarPopup Show(int currentDataIndex = -1)
    {
        this.gameObject.SetActive(true);
        _currentDataIndex = currentDataIndex;
        if(_chooseAvatarItems.Count <= 0)
        {
            for (int i = 0; i < DataManager.Instance.Avatars.Count; i++)
            {
                ChooseAvatarItem item = Instantiate<ChooseAvatarItem>(_itemPrefab, _itemParent);
                item.Initialize(i, i == _currentDataIndex, ChangeCurrentDataIndex);
                _chooseAvatarItems.Add(item);
            }
            return this;
        }
        _chooseAvatarItems.ForEach(avatarItem =>
        {
            avatarItem.SetCheck(_currentDataIndex);
        });
        return this;
    }
    public void SetOnHide(System.Action<int> onHide)
    {
        _onHide = onHide;
    }
    public void Hide()
    {
        _onHide?.Invoke(_currentDataIndex);
        _onHide = null;
        this.gameObject.SetActive(false);
    }

    private void ChangeCurrentDataIndex(int newDataIndex)
    {
        _currentDataIndex = newDataIndex;
        _chooseAvatarItems.ForEach(avatarItem =>
        {
            avatarItem.SetCheck(_currentDataIndex);
        });
    }

    private void OnButtonBackClicked()
    {
        Hide();
    }
}
