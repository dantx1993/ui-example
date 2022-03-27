using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListGuildPopup : MonoBehaviour
{
    [SerializeField] private GuildItemUI _itemPrefab;
    [SerializeField] private Transform _itemParent;
    [SerializeField] private Button _createButton;

    private UIController _uiController;
    private List<GuildItemUI> _guildItemUIs = new List<GuildItemUI>();

    public void Initialize(UIController uiController)
    {
        _uiController = uiController;
    }

    private void Start() 
    {
        ReloadUI();
    }
    private void OnEnable() 
    {
        _createButton.onClick.AddListener(OnCreateButtonClicked);
    }
    private void OnDisable() 
    {
        _createButton.onClick.RemoveListener(OnCreateButtonClicked);    
    }

    private void ReloadUI()
    {
        for (int i = 0; i < DataManager.Instance.GuildDatas.Count; i++)
        {
            if(i < _guildItemUIs.Count)
            {
                _guildItemUIs[i].Initialize(i, ShowCreatePopup);
                _guildItemUIs[i].gameObject.SetActive(true);
                continue;
            }
            GuildItemUI item = Instantiate<GuildItemUI>(_itemPrefab, _itemParent);
            item.Initialize(i, ShowCreatePopup);
            _guildItemUIs.Add(item);
        }
        if(DataManager.Instance.GuildDatas.Count < _guildItemUIs.Count)
        {
            for (int i = DataManager.Instance.GuildDatas.Count; i < _guildItemUIs.Count; i++)
            {
                _guildItemUIs[i].gameObject.SetActive(false);
            }
        }
    }

    private void ShowCreatePopup(int dataIndex)
    {
        _uiController.CreateAndEditGuildPopup.Show(dataIndex).SetOnHide(AddOrEditGuild);
    }
    private void AddOrEditGuild(GuildData data)
    {
        if(data.id < 0)
        {
            data.id = DataManager.Instance.GuildDatas.Count;
            DataManager.Instance.GuildDatas.Add(data);
            ReloadUI();
            return;
        }
        DataManager.Instance.GuildDatas[data.id] = data;
        ReloadUI();
    }

    private void OnCreateButtonClicked()
    {
        ShowCreatePopup(-1);
    }
}
