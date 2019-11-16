using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RpcHandler.Message;
public class UIManager : MonoBehaviour
{
    public GameObject IpViewContent;
    public GameObject AboutContent;
    
    public Toggle IpViewToggle;
    public Toggle AboutToggle;
    public Toggle LocalTestToggle;
    public static UIManager Instance;
    [Space(5)]
    public Transform ParentIpView;
    public GameObject IpContentPrefab;
    [Space(5)]
    public PopupIpView PopupIpView;
    public PopupChat PopupChat;

    private Dictionary<string, State> hostStateMap = new Dictionary<string, State>();
    public Dictionary<string, State> HostStateMap
    {
        get { return hostStateMap; }
        set { hostStateMap = value; }
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        NetworkManager.Init();
    }
    private void Update()
    {
        if (NetworkManager.Instance.ClientMonitor.GetAllResponse().Count != 0)
        {
            while(NetworkManager.Instance.ClientMonitor.GetAllResponse().Count != 0)
            {
                Response response = NetworkManager.Instance.ClientMonitor.GetAllResponse().Dequeue();
                ProcessResponse(response);
            }
        }
    }
    private void ProcessResponse(Response response)
    {
        if (response.Phrase == "Ping")
        {
            string ip = (string)response.Result;
            string[] res = new string[2];
            res = ip.Split('/');
            IpContentItem ipContentItem = ((GameObject)Instantiate(IpContentPrefab, ParentIpView)).GetComponent<IpContentItem>();
            if (!hostStateMap.ContainsKey(res[0])) hostStateMap.Add(res[0], new State());
            ipContentItem.Setup(res[0], res[1]);
        }
        else if (response.Phrase == "GetAllCurrentProcess")
        {
            string json = (string)response.Result;
            var entry = MiniJSON.Json.Deserialize(json);
            var keyValues = entry as Dictionary<string, object>;
            //Debug.Log(json);
            if (PopupIpView.gameObject.activeSelf)
            {
                PopupIpView.ShowPopup(keyValues);
            }
            else
            {
                Debug.Log("LOST");
            }
        }
    }
    public void ActivePopupIpView(string ip, string name)
    {
        PopupIpView.gameObject.SetActive(true);
        PopupIpView.IP = ip;
        PopupIpView.NAME = name;
    }
    public void OnClickIpView(bool enable)
    {
        IpViewContent.SetActive(IpViewToggle.isOn);
    }
    public void OnClickAbout(bool enable)
    {
        AboutContent.SetActive(AboutToggle.isOn);
    }
    public void OnClickLocalScan()
    {
        ResetIpContent();
        NetworkManager.Instance.LocalScan();
    }
    public void OnClickScan()
    {
        ResetIpContent();
        NetworkManager.Instance.ScanIP();
    }
    private void ResetIpContent()
    {
        Queue<Transform> temp = new Queue<Transform>();
        foreach (Transform transform in ParentIpView) temp.Enqueue(transform);
        while (temp.Count != 0) Destroy(temp.Dequeue().gameObject);

        hostStateMap.Clear();
    }
    public class State
    {
        public bool EnableUSB;
        public bool EnableClipboard;
        public bool EnableFileShare;
        public State()
        {
            EnableUSB = true;
            EnableClipboard = true;
            EnableFileShare = true;
        }
    }
}
