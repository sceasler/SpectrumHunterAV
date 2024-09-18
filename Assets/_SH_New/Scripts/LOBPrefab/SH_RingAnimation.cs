using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SH_RingAnimation script is applied to each "RingLine" game object of the RingLinePrefabLeft and RingLinePrefabRight
/// which makes up the RingArrowsPrefab. TheRingArrowsPrefab is instantiated by the SH_LOBShooter_RingArrows script of the LOB game object.
/// Each RingArrowsPrefab will destroy itself based on its SH_DestroyTimer.
/// The goal of this script is to cause the RingLine object (at 0,0,0) to rotate around from back to front following the curve of the RingLine.
/// This script uses an AnimationCurve to allow us to slowly increase the speed of the rotation over time.  Starts slow - ends fast.
/// There are 6 RingLine objects per RingLinePrefab and the AnimCurveKeyOffset is used to cause each SH_RingAnimation start time to be delayed
/// and therefore seem to start at 1 second intervals.  Each SH_RingAnimation object hides itself when the angle local rotation exceeds a certain angle.
/// 
/// </summary>
namespace SpectrumHunterClient
{
    public class SH_RingAnimation : MonoBehaviour
    {
        public bool isRightSide = false;
        public float AnimCurveKeyOffset = 0.0f;

        // This is a scriptable object that controls the curve, speed, and angle
        // To change values, change he instances of the scriptable object (holds data) in the project files.
        public SH_MotionCurve MotionCurve; 

        private float elapsedTime = 0.0f;
        private Quaternion startingAngle;
        private bool isChildActive = false;
        private AnimationCurve thisCurve;
        private bool isPaused = false;

        void Start()
        {
            SH_EventManager.Instance.PauseLOBAnimation += SH_EventManager_SetPause;

            thisCurve = new AnimationCurve();

            Keyframe[] keys = MotionCurve.Curve.keys;
            for (int i = 0; i < keys.Length; i++)
            {
                thisCurve.AddKey(keys[i].time += AnimCurveKeyOffset, keys[i].value);
                // Offset the time for each of these
            }

            startingAngle = transform.rotation; // Saved in case we wanted to recycle the object

            // Hide child objects at start
            int childNum = transform.childCount;
            for (int i = 0; i < childNum; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void SH_EventManager_SetPause(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        void Update()
        {
            if (!isPaused)
            {
                elapsedTime += Time.deltaTime;

                // We delay showing the child object until after offset and one second time
                if (elapsedTime > AnimCurveKeyOffset + 1 && !isChildActive)
                {
                    int childNum = transform.childCount;
                    for (int i = 0; i < childNum; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    isChildActive = true;
                }
                // Use the animation curve to evaluate the position and speed of the rotation.
                // Note: origin is the center of the ring.
                transform.Rotate(MotionCurve.Angle * thisCurve.Evaluate(elapsedTime) * Time.deltaTime * MotionCurve.Speed);

                // After its trip around the ring we use the angle to determines when to disable the child objects
                // The angle will be different depending on if it is going around to the left of right
                if (!isRightSide)
                {
                    if (transform.localRotation.eulerAngles.y >= 270)
                    {
                        transform.localRotation = startingAngle;  // In case we wanted to recycle the object (but we are not)
                        elapsedTime = 0.0f;
                        this.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (!(transform.localRotation.eulerAngles.y >= 90.0f && transform.localRotation.eulerAngles.y < 270.0f))
                    {
                        transform.localRotation = startingAngle; // In case we wanted to recycle the object (but we are not)
                        elapsedTime = 0.0f;
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}

