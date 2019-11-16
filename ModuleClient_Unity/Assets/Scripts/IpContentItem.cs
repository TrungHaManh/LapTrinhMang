using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IpContentItem : MonoBehaviour
{
    public TextMeshProUGUI TextMesh;
    private string host_ip;
    private string host_name;
    public void Setup(string ip, string name)
    {
        TextMesh.text = ip + " / " + name;
        host_ip = ip;
        host_name = name;
    }
    public void OnClick()
    {
        UIManager.Instance.ActivePopupIpView(host_ip, host_name);
        NetworkManager.Instance.GetAllCurrentProcess(host_ip, UIManager.Instance.LocalTestToggle.isOn);
    }
    public void OnChat()
    {
        UIManager.Instance.PopupChat.gameObject.SetActive(true);
        UIManager.Instance.PopupChat.Setup(host_ip, host_name);
    }
}
