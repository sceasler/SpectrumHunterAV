using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;

namespace SpectrumHunterClient
{
    public class SH_Button_Setup : SH_ButtonBase
    {
        private bool playOnClickSound = false;

        void Update()
        {
            if (playOnClickSound)
            {
                base.audioSource.clip = soundOnClick;
                base.audioSource.Play();
                playOnClickSound = false;
                this.gameObject.SetActive(false);
            }
        }

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            base.OnInputClicked(eventData);
            playOnClickSound = true;
            this.gameObject.SetActive(true);
        }
    }
}

