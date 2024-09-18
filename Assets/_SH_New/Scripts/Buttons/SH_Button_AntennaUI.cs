using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;

namespace SpectrumHunterClient
{
    public class SH_Button_AntennaUI : MonoBehaviour, IFocusable, IInputClickHandler
    {
        public GameObject[] ToHideWhenClicked;
        public GameObject[] ToShowWhenClicked;
        public GameObject[] ToHideWhenReset;
        public GameObject[] ToShowWhenReset;

        private GameObject UIHolobarManipulationBG;

        public virtual void Start()
        {
            foreach (Transform child in this.GetComponentInChildren<Transform>())
            {
                if (child.name == "UIHolobarManipulationBG")
                {
                    UIHolobarManipulationBG = child.gameObject;
                }
            }

            if (UIHolobarManipulationBG != null)
            {
                UIHolobarManipulationBG.SetActive(false);
            }

        }

        public virtual void OnDisable()
        {
            if (UIHolobarManipulationBG != null)
            {
                UIHolobarManipulationBG.SetActive(false);
            }
        }

        public virtual void OnFocusEnter()
        {
            if (UIHolobarManipulationBG != null)
            {
                UIHolobarManipulationBG.SetActive(true);
            }
        }

        public virtual void OnFocusExit()
        {
            if (UIHolobarManipulationBG != null)
            {
                UIHolobarManipulationBG.SetActive(false);
            }
        }

        public virtual void OnInputClicked(InputClickedEventData eventData)
        {
            foreach (GameObject go in ToShowWhenClicked)
            {
                go.SetActive(true);
            }

            foreach (GameObject go in ToHideWhenClicked)
            {
                go.SetActive(false);
            }

            if (UIHolobarManipulationBG != null)
            {
                UIHolobarManipulationBG.SetActive(false);
            }

            this.gameObject.SetActive(false);
        }

        public virtual void ResetButton()
        {
            foreach (GameObject go in ToShowWhenReset)
            {
                go.SetActive(true);
            }

            foreach (GameObject go in ToHideWhenReset)
            {
                go.SetActive(false);
            }
        }
    }
}