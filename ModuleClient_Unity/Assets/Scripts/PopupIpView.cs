using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupIpView : MonoBehaviour
{
    public Transform ProcessViewPr;
    public RectTransform PopupRectTransform;
    public GameObject ProcessItemPrefab;

    public TextMeshProUGUI Title;
    public Toggle EnableClipboard;
    public Toggle EnableUsb;
    public Toggle EnableShareFile;
    
    public string IP { get; set; }
    public string NAME { get; set; }
    private void OnEnable()
    {
        ResetPopup();
    }
    private void ResetPopup()
    {
        PopupRectTransform.localScale = Vector3.zero;
        // 
        Queue<Transform> temp = new Queue<Transform>();
        foreach (Transform child in ProcessViewPr) temp.Enqueue(child);
        while (temp.Count != 0) Destroy(temp.Dequeue().gameObject);
    }
    public void ShowPopup(Dictionary<string, object> keyValues)
    {
        if (UIManager.Instance.HostStateMap.ContainsKey(IP))
        {
            EnableClipboard.isOn = UIManager.Instance.HostStateMap[IP].EnableClipboard;
            //OnChangedOnEnableClipboard(true);
        }
        if (UIManager.Instance.HostStateMap.ContainsKey(IP))
        {
            EnableUsb.isOn = UIManager.Instance.HostStateMap[IP].EnableUSB;
            //OnChangedOnEnableUSB(true);
        }
        if (UIManager.Instance.HostStateMap.ContainsKey(IP))
        {
            EnableShareFile.isOn = UIManager.Instance.HostStateMap[IP].EnableFileShare;
            //OnChangedOnEnableShareFile(true);
        }

        PopupRectTransform.localScale = Vector3.one;
        Title.text = IP + "/" + NAME;
        foreach(var process in keyValues)
        {
            ItemProcessView itemProcessView = (Instantiate(ProcessItemPrefab, ProcessViewPr) as GameObject).GetComponent<ItemProcessView>();
            itemProcessView.Setup(int.Parse(process.Key), process.Value.ToString(), IP);
        }
    }
    public void OnCloseButton()
    {
        this.gameObject.SetActive(false);
    }
    public void OnChangedOnEnableClipboard(bool v)
    {
        NetworkManager.Instance.EnableClipboard(EnableClipboard.isOn, IP, UIManager.Instance.LocalTestToggle.isOn);
        if (UIManager.Instance.HostStateMap.ContainsKey(IP))
            UIManager.Instance.HostStateMap[IP].EnableClipboard = EnableClipboard.isOn;
    }
    public void OnChangedOnEnableUSB(bool v)
    {
        NetworkManager.Instance.EnableUSB(EnableUsb.isOn, IP, UIManager.Instance.LocalTestToggle.isOn);
        if (UIManager.Instance.HostStateMap.ContainsKey(IP))
            UIManager.Instance.HostStateMap[IP].EnableUSB = EnableUsb.isOn;
    }
    public void OnChangedOnEnableShareFile(bool v)
    {
        NetworkManager.Instance.EnableShareFile(EnableShareFile.isOn, IP, UIManager.Instance.LocalTestToggle.isOn);
        if (UIManager.Instance.HostStateMap.ContainsKey(IP))
            UIManager.Instance.HostStateMap[IP].EnableFileShare = EnableShareFile.isOn;
    }

}
