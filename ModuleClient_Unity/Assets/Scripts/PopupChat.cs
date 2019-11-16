using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupChat : MonoBehaviour
{
    public TMP_InputField TitleMessage;
    public TMP_InputField Message;
    public TextMeshProUGUI Title;

    public string IP { get; set; }
    public string NAME { get; set; }

    public void Setup(string ip, string name)
    {
        IP = ip;
        NAME = name;
        Title.text = IP + "/" + NAME;
        Message.text = "";
        TitleMessage.text = "";
    }

    public void OnSend()
    {
        string title = TitleMessage.text;
        string mess = Message.text;
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(mess)) return;

        NetworkManager.Instance.Chat(title, mess, IP, UIManager.Instance.LocalTestToggle.isOn);
        OnClose();
    }
    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
}
