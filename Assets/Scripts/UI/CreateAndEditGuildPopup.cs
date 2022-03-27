using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class CreateAndEditGuildPopup : MonoBehaviour
{
    [SerializeField] private Image _avatar;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _descInput;
    [SerializeField] private TMP_InputField _ruleInput;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _chooseAvatarButton;

    private UIController _uiController;
    private GuildData _data;
    private System.Action<GuildData> _onHide;
    private TextMeshProUGUI _createButtonLabel;

    private TextMeshProUGUI CreateButtonLabel
    {
        get
        {
            if(_createButtonLabel == null)
            {
                _createButtonLabel = _createButton.GetComponentInChildren<TextMeshProUGUI>();
            }
            return _createButtonLabel;
        }
    }

    private void OnEnable() 
    {
        _closeButton.onClick.AddListener(() => Hide(false));
        _createButton.onClick.AddListener(OnConfirmButtonClicked);
        _chooseAvatarButton.onClick.AddListener(OnAvatarButtonClicked);
    }
    private void OnDisable() 
    {
        _closeButton.onClick.RemoveListener(() => Hide(false));
        _createButton.onClick.RemoveListener(OnConfirmButtonClicked);
        _chooseAvatarButton.onClick.RemoveListener(OnAvatarButtonClicked);    
    }

    public void Initialize(UIController uiController)
    {
        _uiController = uiController;
    }

    public CreateAndEditGuildPopup Show(int dataIndex)
    {
        this.gameObject.SetActive(true);
        if(dataIndex < 0)
        {
            _avatar.sprite = null;
            _nameInput.interactable = true;
            _nameInput.text = "";
            _descInput.text = "";
            _ruleInput.text = "";
            _data = new GuildData(-1, "", "", "", -1);
            CreateButtonLabel.text = "Create";
            return this;
        }
        _data = DataManager.Instance.GuildDatas[dataIndex];
        ChangeAvatar(_data.avatarIndex);
        _nameInput.text = _data.name;
        _nameInput.interactable = false;
        _descInput.text = _data.description;
        _ruleInput.text = _data.rule;
        CreateButtonLabel.text = "Confirm";
        return this;
    }
    public void SetOnHide(System.Action<GuildData> onHide)
    {
        _onHide = onHide;
    }
    private void Hide(bool isInvokeOnHide)
    {
        if(isInvokeOnHide)
        {
            _onHide?.Invoke(_data);
        }
        this.gameObject.SetActive(false);
    }

    private void ChangeAvatar(int avatarIndex)
    {
        _data.avatarIndex = avatarIndex;
        if (avatarIndex < 0)
        {
            _avatar.sprite = null;
            return;
        }
        _avatar.sprite = DataManager.Instance.Avatars[_data.avatarIndex];
    }
    
    private void OnConfirmButtonClicked()
    {
        _data.name = _nameInput.text;
        _data.description = _descInput.text;
        _data.rule = _ruleInput.text;
        string msg;
        if(!IsValidate(out msg))
        {
            _uiController.MessagePopup.Show(msg);
            return;
        }
        this.Hide(true);
    }
    private void OnAvatarButtonClicked()
    {
        _uiController.ChooseAvatarPopup.Show(_data.avatarIndex).SetOnHide(ChangeAvatar);
    }
    private bool IsValidate(out string msg)
    {
        bool result = true;
        msg = "Please input";
        if(_data.avatarIndex < 0)
        {
            msg += (result ? " " : ", ") + "avatar";
            result &= false;
        }
        if (string.IsNullOrEmpty(_data.name))
        {
            msg += (result ? " " : ", ") + "name";
            result &= false;
        }
        if (string.IsNullOrEmpty(_data.rule))
        {
            msg += (result ? " " : ", ") + "rule";
            result &= false;
        }
        return result;
    }
}
