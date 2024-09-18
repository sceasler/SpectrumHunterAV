using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpectrumHunterClient
{
    public class SH_RingUIPanel : MonoBehaviour
    {
        public TextMeshProUGUI ageData;
        public TextMeshProUGUI bearingData;
        void Start()
        {
            SH_EventManager.Instance.UpdateSignalDataSubscribers += SH_EventManager_UpdateSignalDataSubscribers;
        }

        private void Update()
        {
            
        }

        private void SH_EventManager_UpdateSignalDataSubscribers(SignalData sigData)
        {
            if (ageData != null)
            {
                ageData.text = sigData.GetAge();
                bearingData.text = sigData.GetBearing();
            }
        }
    }
}

