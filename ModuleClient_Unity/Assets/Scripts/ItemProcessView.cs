using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemProcessView : MonoBehaviour
{
    public TextMeshProUGUI content;
    private int idProcess;
    private string ip;
    public void Setup(int id, string _content, string _ip)
    {
        ip = _ip;
        idProcess = id;
        content.text = id + " - " + _content;
    }
    public void OnClickShutdownProcess()
    {
        NetworkManager.Instance.ShutdowProcess(idProcess, ip, UIManager.Instance.LocalTestToggle.isOn);
        Destroy(this.gameObject);
    }
}
