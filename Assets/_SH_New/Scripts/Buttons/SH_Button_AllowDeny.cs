using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_Button_AllowDeny : SH_ButtonBase
    {
        public bool isAccept = false;

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            base.OnInputClicked(eventData);

            SH_EventManager.Instance.DoAcceptDeny(isAccept);
            Destroy(transform.parent.gameObject);
        }

    }
}

