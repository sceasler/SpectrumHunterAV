using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SpectrumHunterClient
{
    public class SH_StatusBar : MonoBehaviour
    {
        public GameObject StatusLight;

        // Status Bar
        public TextMeshProUGUI statusBar_Status;
        public TextMeshProUGUI statusBar_Ip;
        public TextMeshProUGUI statusBar_Time;
        public TextMeshProUGUI statusBar_Message;

        // Status Bar Status Lights
        public Material StatusLight_Neutral;
        public Material StatusLight_Good;
        public Material StatusLight_Bad;

        private Renderer statusBar_Renderer;
        private Material[] statusBar_Materials_Neutral;
        private Material[] statusBar_Materials_Good;
        private Material[] statusBar_Materials_Bad;

        void Start()
        {
            SH_EventManager.Instance.UpdateStatusBar += SH_EventManager_UpdateStatusBar;
            SH_EventManager.Instance.UpdateSignalDataSubscribers += SH_EventManager_UpdateSignalDataSubscribers;

            // Init renderer variable
            statusBar_Renderer = StatusLight.GetComponent<Renderer>();
            // Init Material array variables
            statusBar_Materials_Neutral = statusBar_Renderer.materials;
            statusBar_Materials_Good = statusBar_Renderer.materials;
            statusBar_Materials_Bad = statusBar_Renderer.materials;

            // Set custom materials for the material arrays
            statusBar_Materials_Neutral[1] = StatusLight_Neutral;
            statusBar_Materials_Good[1] = StatusLight_Good;
            statusBar_Materials_Bad[1] = StatusLight_Bad;
        }

        private void SH_EventManager_UpdateSignalDataSubscribers(SignalData sigData)
        {
            statusBar_Time.text = sigData.GetAge();
        }

        private void SH_EventManager_UpdateStatusBar(StatusBar statusBarData, bool updateColor)
        {
            statusBar_Status.text = statusBarData.TextualStatus;
            statusBar_Ip.text = statusBarData.ServerIp;
            statusBar_Message.text = statusBarData.LastMessageReceived;

            if (updateColor)
            {
                switch (statusBarData.CurrentStatus)
                {
                    case Status.Neutral:
                        statusBar_Renderer.materials = statusBar_Materials_Neutral;
                        break;
                    case Status.Good:
                        statusBar_Renderer.materials = statusBar_Materials_Good;
                        break;
                    case Status.Bad:
                        statusBar_Renderer.materials = statusBar_Materials_Bad;
                        break;
                }
            }
        }

        void Update()
        {
        }
    }
}

