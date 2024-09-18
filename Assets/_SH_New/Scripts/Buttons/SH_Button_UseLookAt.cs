using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_Button_UseLookAt : SH_ButtonBase
    {
        public TMPro.TextMeshProUGUI TextMesh;

        private bool isUsingLookAt = false;

        public override void Start()
        {
            TextMesh.text = "Enable Offset";
            base.Start();
        }

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            if (isUsingLookAt)
            {
                SH_EventManager.Instance.SetLookAt(false);
                TextMesh.text = "Enable Offset";
                isUsingLookAt = false;
            }
            else
            {
                SH_EventManager.Instance.SetLookAt(true);
                TextMesh.text = "Disable Offset";
                isUsingLookAt = true;
            }
            
            base.OnInputClicked(eventData);
            gameObject.SetActive(true);
            
        }
    }
}

