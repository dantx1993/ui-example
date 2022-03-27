using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ChooseAvatarPopup _chooseAvatarPopup;
    [SerializeField] private CreateAndEditGuildPopup _createAndEditGuildPopup;
    [SerializeField] private ListGuildPopup _listGuildPopup;
    [SerializeField] private MessagePopup _messagePopup;

    public ChooseAvatarPopup ChooseAvatarPopup => _chooseAvatarPopup;
    public CreateAndEditGuildPopup CreateAndEditGuildPopup => _createAndEditGuildPopup;
    public MessagePopup MessagePopup => _messagePopup;

    private void Awake() 
    {
        _createAndEditGuildPopup.Initialize(this);
        _listGuildPopup.Initialize(this);
        _listGuildPopup.gameObject.SetActive(true);
        _createAndEditGuildPopup.gameObject.SetActive(false);
        _chooseAvatarPopup.gameObject.SetActive(false);
        _messagePopup.gameObject.SetActive(false);
    }
}
