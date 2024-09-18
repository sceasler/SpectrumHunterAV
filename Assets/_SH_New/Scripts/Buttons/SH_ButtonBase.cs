using System;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using System.Collections;

namespace SpectrumHunterClient
{
    public class SH_ButtonBase: MonoBehaviour, IFocusable, IInputClickHandler
    {
        public GameObject[] ToHideWhenClicked;
        public GameObject[] ToShowWhenClicked;
        public GameObject[] ToHideWhenReset;
        public GameObject[] ToShowWhenReset;

        // For buttons that want to be triggered by OnFocus
        public bool isFocusClickEnabled = false;
        public string FocusClickButtonName = "";

        private GameObject UIHolobarManipulationBG;
        protected AudioSource audioSource;
        protected AudioClip soundOnFocus;
        protected AudioClip soundOnClick;

        public virtual void Start()
        {
            audioSource = GameObject.Find("SH_Audio").GetComponent<AudioSource>();
            soundOnFocus = Resources.Load<AudioClip>("Audio/OnFocus");
            soundOnClick = Resources.Load<AudioClip>("Audio/OnClick");

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

            if (!audioSource.isPlaying)
            {
                audioSource.clip = soundOnFocus;
                audioSource.Play();
            }
        }

        public virtual void OnFocusExit()
        {
            audioSource.Stop();
            if (UIHolobarManipulationBG != null)
            {
                UIHolobarManipulationBG.SetActive(false);
            }
        }

        public virtual void OnInputClicked(InputClickedEventData eventData)
        {
            audioSource.clip = soundOnClick;
            audioSource.Play();

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

        // Programatic trigger for buttons that are enabled for Focus Clicking
        public virtual void TriggerOnInputClicked()
        {
            audioSource.clip = soundOnClick;
            audioSource.Play();

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
            SH_EventManager.Instance.SetCursorFocus(false, FocusClickButtonName);
            
            // Note: Not disabling because that may be done by the animation
            //this.gameObject.SetActive(false);
        }
    }
}