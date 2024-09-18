using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    /// <summary>
    /// 
    /// </summary>
    public class UIRotation : MonoBehaviour
    {
        public Transform Cam;
        public float DegreeMaxAngle = 25f;
        public float Smoothness = 1;

        private bool isCentered = true;

        private bool isPaused = false;

        void Start()
        {
            Vector3 rotation = new Vector3(transform.eulerAngles.x, Cam.eulerAngles.y, transform.eulerAngles.z);
            transform.eulerAngles = rotation;

            //SH_EventManager.Instance.PauseUIRotation += (bool pause) =>
            //{
            //    if (pause)
            //    {
            //        isPaused = true;
            //    }
            //    else
            //    {
            //        isPaused = false;
            //    }
            //};
        }

        void LateUpdate()
        {

            Vector3 currentRotation = transform.eulerAngles;
            // delta will be some number between -180 and + 180
            float delta = Delta_Bearing(currentRotation.y, Cam.eulerAngles.y);
            // diff will be a positive number from 0 to 180
            float diff = Mathf.Abs(delta);

            // If our UI is more than 25 degrees away from the direction the user is looking
            // we begin the procees of moving it back to center
            if (diff > DegreeMaxAngle)
            {
                isCentered = false;
            }
            else
            {
                isCentered = true;
            }

            if (!isCentered && !isPaused)
            {

                if (diff > 40.0f) // If user turns head rapidly you might get this and want to bring the UI around faster
                {
                    currentRotation.y = Mathf.LerpAngle(currentRotation.y, Cam.eulerAngles.y, Time.deltaTime * Smoothness * 3);
                    transform.eulerAngles = currentRotation;
                }
                else
                {
                    currentRotation.y = Mathf.LerpAngle(currentRotation.y, Cam.eulerAngles.y, Time.deltaTime * Smoothness);
                    transform.eulerAngles = currentRotation;
                }

                if (diff < 1.0f)
                {
                    isCentered = true;
                }
            }

            // Keep this locked at the position of the camera
            transform.position = Cam.position;
        }

        static float Delta_Bearing(float b1, float b2)
        {
            /*
             * Optimal solution
             *
            decimal d = 0;

            d = (b2-b1)%360;

            if(d>180)
                d -= 360;
            else if(d<-180)
                d += 360;

            return d;
             */

            float d = 0.0f;

            // Convert bearing to W.C.B
            if (b1 < 0.0f)
                b1 += 360.0f;
            if (b2 < 0.0f)
                b2 += 360.0f;

            ///Calculate delta bearing
            //and
            //Convert result value to Q.B.
            d = (b2 - b1) % 360.0f;

            if (d > 180.0f)
                d -= 360.0f;
            else if (d < -180.0f)
                d += 360.0f;

            return d;
        }
    }
}