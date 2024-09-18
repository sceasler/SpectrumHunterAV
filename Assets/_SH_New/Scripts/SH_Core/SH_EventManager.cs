using System;
using HoloToolkit.Unity;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_EventManager : SingleInstance<SH_EventManager>
    {
        public delegate void UseLookAtEventHandler(bool useLookAt);
        public event UseLookAtEventHandler UseLookAt;

        public void SetLookAt(bool useLookAt)
        {
            OnUseLookAt(useLookAt);
        }
        private void OnUseLookAt(bool useLookAt) => UseLookAt?.Invoke(useLookAt);

        /// <summary>
        /// Event to show/hide the CursorFocus object which tells the user they are looking at a button
        /// Publisher: The OnFocusEnter/OnFocusExit of any button if isFocusClickEnabled is true
        /// 
        /// Subscriber: CursorFocus.cs
        /// </summary>
        /// <param name="focusEnter"></param>
        /// <param name="button_name"></param>
        public delegate void CursorFocusEventHandler(bool focusEnter, string button_name);
        public event CursorFocusEventHandler CursorFocus;

        public void SetCursorFocus(bool focusEnter, string button_name)
        {
            OnCursorFocus(focusEnter, button_name);
        }
        private void OnCursorFocus(bool focusEnter, string button_name) => CursorFocus?.Invoke(focusEnter, button_name);

        /// <summary>
        /// Event to programatically trigger an OnClick action for any button
        /// Publisher: SH_CursorFocusClickable
        /// 
        /// Subscriber: The TriggerOnInputClicked() of any button where isFocusClickEnabled is true
        ///  (SH_Button_More, SH_Button_Close)
        /// </summary>
        /// <param name="button_name"></param>
        public delegate void TriggerClickEventHandler(string button_name);
        public event TriggerClickEventHandler TriggerClick;

        public void SetTriggerClick(string button_name)
        {
            OnTriggerClick(button_name);
        }
        private void OnTriggerClick(string button_name) => TriggerClick?.Invoke(button_name);

        /// <summary>
        /// Event to update status bar data from the MessageHandlerClient to the status bar
        /// Publisher: MessageHandlerClient
        /// 
        /// Subscribers: SH_Statusbar.cs
        /// </summary>
        /// <param name="statusBarData"></param>
        public delegate void UpdateStatusBarEventHandler(StatusBar statusBarData, bool updateColor);
        public event UpdateStatusBarEventHandler UpdateStatusBar;

        public void DoUpdateStatusBar(StatusBar statusBarData, bool updateColor)
        {
            OnUpdateStatusBar(statusBarData, updateColor);
        }
        private void OnUpdateStatusBar(StatusBar statusBarData, bool updateColor) => UpdateStatusBar?.Invoke(statusBarData, updateColor);


        /// <summary>
        /// Event to trigger update to any signal data subscribers
        /// Publisher: MessageHandlerClient
        /// 
        /// Subscribers: 
        /// </summary>
        /// <param name="sigData"></param>
        public delegate void UpdateSignalDataSubscribersEventHandler(SignalData sigData);
        public event UpdateSignalDataSubscribersEventHandler UpdateSignalDataSubscribers;

        public void DoUpdateSignalDataSubscribers(SignalData sigData)
        {
            OnUpdateSignalDataSubscribers(sigData);
        }
        private void OnUpdateSignalDataSubscribers(SignalData sigData) => UpdateSignalDataSubscribers?.Invoke(sigData);


        /// <summary>
        /// Event to trigger update of the arrow pointer rotation.
        /// Publisher: Decoder
        /// 
        /// Subscriber: SH_PointerArrow & SH_PointerArrowAntenna
        /// </summary>
        public delegate void UpdatePointerArrowRotationEventHandler(Quaternion lob);
        public event UpdatePointerArrowRotationEventHandler UpdatePointerArrowRotation;

        public void DoUpdatePointerArrowRotation(Quaternion lob)
        {
            OnUpdatePointerArrowRotation(lob);
        }
        private void OnUpdatePointerArrowRotation(Quaternion lob) => UpdatePointerArrowRotation?.Invoke(lob);


        /// <summary>
        /// Event for Accept/Deny button to tell the MessageHandlerClient weather to allow accept of messages from the server
        /// Publisher: SH_Button_AcceptDeny
        /// 
        /// Subscriber: MessageHandlerClient
        /// </summary>
        /// <param name="doAccept"></param>
        public delegate void AcceptDenyButtonEventHandler(bool doAccept);
        public event AcceptDenyButtonEventHandler AcceptDeny;

        public void DoAcceptDeny(bool doAccept)
        {
            OnAcceptDeny(doAccept);
        }
        private void OnAcceptDeny(bool doAccept) => AcceptDeny?.Invoke(doAccept);

        /// <summary>
        /// Event to Pause the motion of the LOB Animation
        /// Publisher: MessageHandlerClient
        /// 
        /// Subscriber: SH_LobShooter_ArrowBullets, SH_ArrowBullet, SH_LobShooter_RingArrows, SH_RingAnimation
        /// </summary>
        public delegate void PauseLOBAnimationEventHandler(bool isPaused);
        public event PauseLOBAnimationEventHandler PauseLOBAnimation;

        public void DoPauseLOBAnimation(bool isPaused)
        {
            OnPauseLOBAnimation(isPaused);
        }
        private void OnPauseLOBAnimation(bool isPaused) => PauseLOBAnimation?.Invoke(isPaused);

        /// <summary>
        /// Event to indicate that the LOB information is stale
        /// Publisher: MessageHandlerClient
        /// 
        /// Subscribers: SH_ArrowBullet
        /// </summary>
        /// <param name="isStale"></param>
        public delegate void StaleLOBAnimationEventHandler(bool isStale);
        public event StaleLOBAnimationEventHandler StaleLOBAnimation;

        public void DoStaleLOBAnimation(bool isStale)
        {
            OnStaleLOBAnimation(isStale);
        }
        private void OnStaleLOBAnimation(bool isStale) => StaleLOBAnimation?.Invoke(isStale);

        ///// <summary>
        ///// Event allows caller to disable the UI auto-rotation
        ///// Publisher: CamQuad.cs  //Not yet
        ///// 
        ///// Subscriber: UIRotation.cs
        ///// </summary>
        ///// <param name="pause"></param>
        //public delegate void PauseUIRotationEventHandler(bool pause);
        //public event PauseUIRotationEventHandler PauseUIRotation;

        //public void UIRotation(bool pause)
        //{
        //    OnPauseUIRotation(pause);
        //}

        //private void OnPauseUIRotation(bool pause) => PauseUIRotation?.Invoke(pause);

    }
}
