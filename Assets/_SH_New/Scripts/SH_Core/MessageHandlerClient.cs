using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using HoloToolkit.Unity;
//using HoloToolkit.UI.Keyboard;
using SpectrumHunterComms;
using CI.WSANative.Dispatchers;
using System;
using TMPro;

namespace SpectrumHunterClient
{
    public enum Status { Neutral, Good, Bad };

    public class MessageHandlerClient : SingleInstance<MessageHandlerClient>
    {
        // public fields
        public bool EnableLogging = false;

        // For testing only
        public bool sendMessage = false;
        //public Text screenText;

        // UI popup for user to allow or deny connection from server
        public GameObject AllowOrDenyPrefab;
        public GameObject DFSystemPrefab;
        public Transform DFSystemPrefabStart;

        public SignalData SignalData; // Currently we are just tracking one signal
        public StatusBar StatusBarData;

        // fields related to the udpClientServer
        private UdpClientServer udpClientServer;
        private readonly int receivePort = 2580;
        private readonly string sendPort = "2583";

        private Decoder decoder;

        // fields related to server access
        private List<Server> Servers;
        private bool serverPermitted = false;
        private bool serverRequestRaised = false;
        private bool doDelay = false;
        private float elapsedDelayTime = 0.0f;
        private float delayTime = 15.0f;

        private float timeSinceLastMessage = 0f;
        private float timeSinceLastMessageThreshold = 10f;

        private void Start()
        {
            SH_EventManager.Instance.AcceptDeny += SH_EventManager_AcceptDeny;
            InitStatusBarData();
            SignalData = new SignalData();
            Servers = new List<Server>();
            WSANativeDispatcher.Initialise();
            decoder = new Decoder();
            StartListening();
        }

        // Event rasied when user clicks either the Accept or Deny button
        private void SH_EventManager_AcceptDeny(bool doAccept)
        {
            if (!doAccept)
            {
                serverPermitted = false;
                serverRequestRaised = false;
                doDelay = true;
                Debug.Log("User did not accept connection from server.");
            }
            else
            {
                serverPermitted = true;
                serverRequestRaised = false;
                Debug.Log("User accepted connection from server.");
                //Vector3 dir = Camera.main.transform.forward.normalized;
                //Vector3 pos = Camera.main.transform.position + dir * 2f;
                //Vector3 target = Camera.main.transform.rotation * Vector3.forward;
                //Quaternion tar = Quaternion.Euler(target);
                Instantiate(DFSystemPrefab, DFSystemPrefabStart.transform.position, DFSystemPrefab.transform.rotation);
            }
        }

        private void Update()
        {
            timeSinceLastMessage += Time.deltaTime;
            if (SignalData != null)
            {
                SignalData.age = (int)timeSinceLastMessage;
            }
            
            if (StatusBarData.CurrentStatus == Status.Good || StatusBarData.CurrentStatus == Status.Bad)
            {
                //if (statusBar_Time != null)
                //{
                //    statusBar_Time.text = SignalData.GetAge();
                //}
                // Publish update about SignalData
                StatusBarData.TimeOfLastMessage = SignalData.GetAge();
                SH_EventManager.Instance.DoUpdateSignalDataSubscribers(SignalData);
            }

            
            if (timeSinceLastMessage > timeSinceLastMessageThreshold && StatusBarData.CurrentStatus == Status.Good)
            {
                UpdateStatusBar_Bad();
                SH_EventManager.Instance.DoPauseLOBAnimation(true);
            }

            if (timeSinceLastMessage > timeSinceLastMessageThreshold + 15.0f && StatusBarData.CurrentStatus == Status.Bad)
            {
                SH_EventManager.Instance.DoStaleLOBAnimation(true);
            }
            /// !!! FOR TESTING ONLY
            if (sendMessage)  // For testing only
            {
                MsgPosRotUpdate PosRotMsg = new MsgPosRotUpdate("PosRotMsg", 11, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f);
                string jsonText = JsonUtility.ToJson(PosRotMsg);
                Byte[] message = Encoding.ASCII.GetBytes(jsonText);
                foreach (Server s in Servers)
                {
                    udpClientServer.SendMessage(s.ServerIp, sendPort, message);
                }
                sendMessage = false;
            }
            /// !!! FOR TESTING ONLY

            if (doDelay) // User denied adding server
            {
                if (elapsedDelayTime == 0f)
                {
                    udpClientServer.MessageReceivedCallback -= UdpComms_MessageReceivedCallback;
                }
                elapsedDelayTime += Time.deltaTime;
                if (elapsedDelayTime > delayTime)
                {
                    udpClientServer.MessageReceivedCallback += UdpComms_MessageReceivedCallback;
                    elapsedDelayTime = 0;
                    doDelay = false;
                }
            }
        }

        // Callback for all incoming messages from the Magic Computer
        private void UdpComms_MessageReceivedCallback(object sender, MessageReceivedEventArgs e)
        {
            if (EnableLogging)
            {
                Debug.Log("MessageReceivedCallback triggered.");
            }

            if (e.RemoteIp == null)
            {
                return;
            }
            else // Check if we are allowing a new server
            {
                if (serverPermitted)
                {  // Check if it is indeed a new sever
                    bool exists = Servers.Exists(x => x.ServerIp == e.RemoteIp.ToString());
                    if (!exists)
                    {
                        Server newServer = new Server(e.RemoteIp.ToString());
                        Servers.Add(newServer);
                        Debug.Log("New server added.");
                    }
                    serverPermitted = false;
                    serverRequestRaised = false;
                }
            }

            string ip = e.RemoteIp.ToString();
     
            if (!Servers.Exists(x => x.ServerIp == ip)) // If it doesn't exist then we ask the user to allow it
            {
                if (!serverRequestRaised) // If a server request has not yet been raised
                {
                    WSANativeDispatcher.Invoke(() =>
                    {
                        GameObject go = Instantiate(AllowOrDenyPrefab, GameObject.Find("UI-Menus").transform) as GameObject;
                        var IpAddressGO = go.transform.Find("ipAddress").gameObject;
                        IpAddressGO.GetComponent<TMPro.TextMeshProUGUI>().text = ip;
                    });
                    serverRequestRaised = true; // AllowOrDeny UI Menu has been activated
                }
            }
            else  // Server does exist and is allowed
            {
                string jsontext = Encoding.ASCII.GetString(e.Message);
                string decodedMessage = decoder.DecodeToString(jsontext) + Environment.NewLine;

                WSANativeDispatcher.Invoke(() =>
                {
                    decoder.Decode(jsontext);

                    UpdateStatusBar_Good(ip, decodedMessage);
                    SH_EventManager.Instance.DoPauseLOBAnimation(false);
                    timeSinceLastMessage = 0f;

                    if (EnableLogging)
                    {
                        Debug.Log($"Ip: {ip} Message: {jsontext}");
                    }
                });
            }
        }

        private void StartListening()
        {
            udpClientServer = new UdpClientServer(receivePort);
            udpClientServer.MessageReceivedCallback += UdpComms_MessageReceivedCallback;
            try
            {
                udpClientServer.StartListening();
                Debug.Log($"UdpClientServer is now listening on port { receivePort }");
            }
            catch (Exception e)
            {
                udpClientServer.MessageReceivedCallback -= UdpComms_MessageReceivedCallback;
                udpClientServer = null;
                Debug.Log($"Error: UdpClientServer could not start listening on port { receivePort }. Exception: { e.ToString() }");
            }
        }

        private void InitStatusBarData()
        {
            StatusBarData = new StatusBar();
            StatusBarData.TextualStatus = "No Server";
            StatusBarData.CurrentStatus = Status.Neutral;
            StatusBarData.LastMessageReceived = "";
            StatusBarData.ServerIp = "";
            SH_EventManager.Instance.DoUpdateStatusBar(StatusBarData, true);
        }

        private void UpdateStatusBar_Good(string ip, string message)
        {
            if (StatusBarData.CurrentStatus != Status.Good)
            {
                StatusBarData.CurrentStatus = Status.Good;
                StatusBarData.ServerIp = ip;
                StatusBarData.TextualStatus = "Message Received";
                SH_EventManager.Instance.DoUpdateStatusBar(StatusBarData, true);
            }
            else
            {
                StatusBarData.CurrentStatus = Status.Good;
                StatusBarData.ServerIp = ip;
                StatusBarData.TextualStatus = "Message Received";
                SH_EventManager.Instance.DoUpdateStatusBar(StatusBarData, false);
            }

        }

        private void UpdateStatusBar_Bad()
        {
            if (StatusBarData.CurrentStatus != Status.Bad)
            {
                StatusBarData.CurrentStatus = Status.Bad;
                StatusBarData.TextualStatus = "No Message Received";
                SH_EventManager.Instance.DoUpdateStatusBar(StatusBarData, true);
            }
            else
            {
                StatusBarData.CurrentStatus = Status.Bad;
                StatusBarData.TextualStatus = "No Message Received";
                SH_EventManager.Instance.DoUpdateStatusBar(StatusBarData, false);
            }
        }
    }
}

