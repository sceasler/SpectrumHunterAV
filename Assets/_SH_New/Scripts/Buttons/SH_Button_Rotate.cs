using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;

namespace SpectrumHunterClient
{
    public class SH_Button_Rotate : SH_ButtonBase
    {
        public GameObject Antenna;
        public bool isCounterClockwise;
        public float RotationDegrees = 0f;

        private bool playOnClickSound = false;

        void LateUpdate()
        {
            transform.parent.position = Antenna.transform.position;

        }
        void Update()
        {
            if (playOnClickSound)
            {
                base.audioSource.clip = soundOnClick;
                base.audioSource.Play();
                playOnClickSound = false;
            }
        }

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            base.OnInputClicked(eventData);
            if (!isCounterClockwise)
            {
                Antenna.transform.Rotate(Vector3.up, RotationDegrees);
            }
            else
            {
                Antenna.transform.Rotate(Vector3.up, -(RotationDegrees));
            }
            this.gameObject.SetActive(true);
            playOnClickSound = true;
        }
    }
}