using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;

namespace SpectrumHunterClient
{
    public class SH_Button_PositionOkay : SH_ButtonBase
    {
        public GameObject Antenna;

        void LateUpdate()
        {
            transform.parent.position = Antenna.transform.position; 
        }

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            base.OnInputClicked(eventData);
            Antenna.GetComponent<TwoHandManipulatable>().GetComponent<TwoHandManipulatable>().enabled = false;
        }
    }
}