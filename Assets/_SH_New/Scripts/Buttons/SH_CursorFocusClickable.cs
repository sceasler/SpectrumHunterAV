using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpectrumHunterClient
{
    public class SH_CursorFocusClickable : MonoBehaviour
    {
        public GameObject CursorFocusObject;
        public GameObject CursorFocusSpiral;
        public TextMeshProUGUI CursorButtonNameTextObject;

        private Animator anim;

        private bool isAnimationPlaying = false;
        private string buttonName = "";

        void Start()
        {
            anim = CursorFocusSpiral.GetComponent<Animator>();
            CursorButtonNameTextObject.text = "";

            SH_EventManager.Instance.CursorFocus += OnCursorFocus;
        }

        void Update()
        {
            if (isAnimationPlaying)
            {
                // This allows us to know when the animation is completed
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Finished"))
                {
                    CursorFocusObject.gameObject.SetActive(false);
                    CursorButtonNameTextObject.text = "";
                    isAnimationPlaying = false;

                    DoActionForButton(buttonName);
                }
            }
        }

        void OnCursorFocus(bool focusEnter, string button_name)
        {
            if (focusEnter)
            {
                CursorFocusObject.gameObject.SetActive(true);
                anim.SetTrigger("startSpiral");
                CursorButtonNameTextObject.text = button_name;
                buttonName = button_name;
                isAnimationPlaying = true;
            }
            else
            {
                Debug.Log("stopping animation");
                anim?.SetTrigger("stopSpiral");
                CursorFocusObject.gameObject.SetActive(false);
                CursorButtonNameTextObject.text = "";
                buttonName = "";
                isAnimationPlaying = false;
            }
        }

        void DoActionForButton(string button_name)
        {
            SH_EventManager.Instance.SetTriggerClick(button_name);
        }
    }
}

