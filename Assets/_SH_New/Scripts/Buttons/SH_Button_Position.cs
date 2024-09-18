using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;

namespace SpectrumHunterClient
{
    public class SH_Button_Position : SH_ButtonBase
    {
        private TwoHandManipulatable AntennaMinipulatable;

        public override void Start()
        {
            AntennaMinipulatable = GameObject.Find("Antenna").GetComponent<TwoHandManipulatable>();
            AntennaMinipulatable.enabled = false;
            base.Start();
        }

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            AntennaMinipulatable.enabled = true;
            base.OnInputClicked(eventData);
        }

        public override void ResetButton()
        {
            AntennaMinipulatable.enabled = false;
            base.ResetButton();
        }
    }
}