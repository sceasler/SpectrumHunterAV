using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_Button_Close : SH_ButtonBase
    {
        private Animator anim;

        public override void Start()
        {
            if (isFocusClickEnabled)
            {
                SH_EventManager.Instance.TriggerClick += (string button_name) =>
                {
                    if (button_name == FocusClickButtonName)
                    {
                        anim.SetTrigger("Close");
                        TriggerOnInputClicked();
                        // Note: Will not disable the button because that is being done by the
                        // "Close" animation
                    }
                };
            }

            anim = GameObject.Find("RingUIPanel").GetComponent<Animator>();
            base.Start();
        }

        public override void OnFocusEnter()
        {
            base.OnFocusEnter();
            if (isFocusClickEnabled)
            {
                SH_EventManager.Instance.SetCursorFocus(true, FocusClickButtonName);
            }
        }

        public override void OnFocusExit()
        {
            base.OnFocusExit();
            if (isFocusClickEnabled)
            {
                SH_EventManager.Instance.SetCursorFocus(false, FocusClickButtonName);
            }
        }

        public override void OnInputClicked(InputClickedEventData eventData)
        {
            anim.SetTrigger("Close");
            base.OnInputClicked(eventData);
            this.gameObject.SetActive(true);
        }
    }
}

