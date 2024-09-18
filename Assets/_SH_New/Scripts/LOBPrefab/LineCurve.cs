using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    /// <summary>
    /// LineCurve script is applied to each "RingLine" game object of the RingLinePrefabLeft and RingLinePrefabRight
    /// which makes up the RingArrowsPrefab. TheRingArrowsPrefab is instantiated by the SH_LOBShooter_RingArrows script of the LOB game object.
    /// Each RingArrowsPrefab will destroy itself based on its SH_DestroyTimer.
    /// The goal of this script is to cause the RingLine object (at 0,0,0) to rotate around from back to front matching the curve of the RingLine.
    /// This script uses an AnimationCurve to allow us to slowly increase the speed of the rotation over time.  Starts slow - ends fast.
    /// There are 6 RingLine objects per RingLinePrefab and the AnimCurveKeyOffset is used to cause each LineCurve start time to be delayed
    /// and therefore seem to start at 1 second intervals.  Each LineCurve object hides itself when the angle local rotation exceeds a certain angle.
    /// 
    /// </summary>
    public class LineCurve : MonoBehaviour
    {
        public float Speed = 1f;
        public bool isRightSide = false;
        public Vector3 Angle = Vector3.zero;

        // ToDo: Make animation curve a seperate single object
        //public AnimationCurveVariable Curve;

        public AnimationCurve Curve;
        public float AnimCurveKeyOffset = 0.0f;

        private float elapsedTime = 0.0f;
        //private float curveTime;
        private Quaternion startingAngle;
        private bool isChildActive = false;

        void Start()
        {
            //curveTime = Curve.keys[Curve.length - 1].time;

            Keyframe[] keys = Curve.keys;
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].time += AnimCurveKeyOffset; // Offset the time for each of these
            }
            Curve.keys = keys;

            startingAngle = transform.rotation; // Saved in case we wanted to recycle the object

            // Hide child objects at start
            int childNum = transform.childCount;
            for (int i = 0; i < childNum; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        void Update()
        {
            elapsedTime += Time.deltaTime;

            // We don't show the child object until after the first second
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
            transform.Rotate(Angle * Curve.Evaluate(elapsedTime) * Time.deltaTime * Speed);

            // Determines when to disable the child objects
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
                if (transform.localRotation.eulerAngles.y > 90.0f && transform.localRotation.eulerAngles.y <= 270.0f)
                {
                    transform.localRotation = startingAngle; // In case we wanted to recycle the object (but we are not)
                    elapsedTime = 0.0f;
                    this.gameObject.SetActive(false);
                }
            }

            //if (elapsedTime >= curveTime + AnimCurveKeyOffset)
        }
    }
}
