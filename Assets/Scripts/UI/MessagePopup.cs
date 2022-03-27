using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MessagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _msg;
    [SerializeField] private Button _confirmButton;

    private void OnEnable() 
    {
        _confirmButton.onClick.AddListener(Hide);
    }
    private void OnDisable() 
    {
        _confirmButton.onClick.RemoveListener(Hide);
    }

    public void Show(string msg)
    {
        this.gameObject.SetActive(true);
        _msg.text = msg;
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
