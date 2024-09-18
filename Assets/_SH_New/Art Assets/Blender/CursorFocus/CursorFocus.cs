using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpectrumHunterClient
{
    public class CursorFocus : MonoBehaviour
    {
        public GameObject CursorFocusObject;
        public GameObject CursorFocusSpiral;
        public TextMeshProUGUI CursorButtonNameTextObject;

        private Animator anim;
        
        private bool isAnimationPlaying = false;
        private string buttonName = "";

        // Use this for initialization
        void Start()
        {
            anim = CursorFocusSpiral.GetComponent<Animator>();
            CursorButtonNameTextObject.text = "";

            // What does this do?  
            // SetActive the CursorFocus object and start animation
            // SetInactive the CursorFocus and reset animation
            // Passed the name of the button to display in the text object
            // Need to know when animation ends and then trigger other event.

            // Pass a button object?
            // Could be used to control the button

            // Button object
            // string button_name
            // 
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

        void OnCursorFocus(bool focus, string button_name)
        {
            if (focus)
            {
                CursorFocusObject.gameObject.SetActive(true);
                anim.SetTrigger("startSpiral");
                CursorButtonNameTextObject.text = button_name;
                buttonName = button_name;
                isAnimationPlaying = true;
            }
            else
            {
                anim.SetTrigger("stopSpiral");
                CursorFocusObject.gameObject.SetActive(false);
                CursorButtonNameTextObject.text = "";
                buttonName = "";
                isAnimationPlaying = false;
            }
        }

        void DoActionForButton(string button_name)
        {
            SH_EventManager.Instance.SetTriggerClick(button_name);

            //switch (button_name)
            //{
            //    case "Settings":
            //        if (UI_MenuManager.MainMenuState == MainMenuState.Closed)
            //        {
            //            // Get rid of this
            //            LAV_VR_EventManager.Instance.ScreenStateSet(ScreenState.None);

            //            LAV_VR_EventManager.Instance.SettingsOpen();
            //            UI_MenuManager.SetMenuState(MainMenuState.Open, ScreenState.None);
            //        }
            //        else // MainMenu is Open so we close it
            //        {
            //            // Get rid of this
            //            LAV_VR_EventManager.Instance.ScreenStateSet(ScreenState.None);

            //            LAV_VR_EventManager.Instance.SettingsClose();
            //            UI_MenuManager.SetMenuState(MainMenuState.Closed, ScreenState.None);
            //        }
            //        break;
            //    case "Pivot":
            //        GameObject pivot = GameObject.Find("Pivot") as GameObject;
            //        if (pivot.transform.localEulerAngles.y == 0f)
            //        {
            //            LAV_VR_EventManager.Instance.PivotCamWindow(true);
            //        }
            //        else
            //        {
            //            LAV_VR_EventManager.Instance.PivotCamWindow(false);
            //        }
            //        break;
            //    case "Resize":
            //        LAV_VR_EventManager.Instance.EnlargeCamQuad();
            //        break;
            //    case "Next Cam":
            //        LAV_VR_EventManager.Instance.GoToNextCam();
            //        break;
            //    case "Orientation":
            //        LAV_VR_EventManager.Instance.ScreenStateSet(ScreenState.Orientation);
            //        break;
            //    case "Display":
            //        LAV_VR_EventManager.Instance.ScreenStateSet(ScreenState.Display);
            //        break;
            //    case "MiniMap":
            //        LAV_VR_EventManager.Instance.ScreenStateSet(ScreenState.MiniMap);
            //        break;
            //    case "About":
            //        LAV_VR_EventManager.Instance.ScreenStateSet(ScreenState.About);
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}

