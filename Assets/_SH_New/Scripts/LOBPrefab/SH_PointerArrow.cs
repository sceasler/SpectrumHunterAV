using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_PointerArrow : MonoBehaviour
    {
        public GameObject Antenna;
        public GameObject PointerRing;

        // Testing Lookat Option
        public GameObject LookAt;
        public bool doLookAt = false;
        private float elapsedTime = 0;
        private float interval = 1;

        // Used for testing only
        public bool doTest = false;
        public float TestValue = 0.0f;
        
        private Material[] pointerRing_Materials;
        private Renderer pointerRing_Renderer;

        private GameObject FOVpointer;
        private GameObject heightmarker;

        private float speed = 0.0f;

        private void Start()
        {
            SH_EventManager.Instance.UpdatePointerArrowRotation += LobRotate;
            SH_EventManager.Instance.UseLookAt += (bool useLookAt) => { doLookAt = useLookAt; };
            ApplyAlphaAdjustmentToMaterials();

            FOVpointer = GameObject.Find("FOVpointer");
            heightmarker = GameObject.Find("heightmarker");
        }

        private void Update()
        {
            if (doTest)
            {
                doTest = false;
                Quaternion lob = Quaternion.Euler(new Vector3(0, TestValue, 0));
                MessageHandlerClient.Instance.SignalData.bearing = lob.eulerAngles.y;
                MessageHandlerClient.Instance.SignalData.age = 0;
                LobRotate(lob);
            }
        }


        void LateUpdate()
        {
            // Maintain position to be same as the user
            transform.position = Camera.main.transform.position;

            // Set FOVpointer height - Idea is to keep indicator of where signal signal is in fov.
            if (FOVpointer != null && heightmarker != null)
            {
                Vector3 pointer = FOVpointer.transform.position;
                Vector3 marker = heightmarker.transform.position;
                FOVpointer.transform.position = new Vector3(pointer.x, marker.y, pointer.z);
            }

            // Need to create indicators for left and right which may be better.

            // TESTING LOOKAT
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= interval)
            {
                if (doLookAt)
                {
                    Vector3 lookRotation = Quaternion.LookRotation(LookAt.transform.position - transform.position).eulerAngles;
                    Quaternion lob = Quaternion.Euler(Vector3.Scale(lookRotation, Vector3.up));
                    if (transform.rotation != lob)
                    {
                        StopAllCoroutines(); // Stops any coroutines started by this script - doesn't affect other scripts
                        StartCoroutine(RotateTowards(lob));
                    }
                }
                elapsedTime = 0f;
            }
            // TESTING LOOKAT

        }

        // Triggered when LOB Update message is received by and decoded
        public void LobRotate(Quaternion rotation)
        {
            if (!doLookAt)
            {
                Quaternion lob = Antenna.transform.rotation * rotation;
                if (transform.rotation != lob)
                {
                    StopAllCoroutines(); // Stops any coroutines started by this script - doesn't affect other scripts
                    StartCoroutine(RotateTowards(lob));
                }
            }
        }

        private IEnumerator RotateTowards(Quaternion lob)
        {
            while (transform.rotation != lob)
            {
                // delta will be some number between -180 and + 180
                float delta = Delta_Bearing(transform.eulerAngles.y, lob.eulerAngles.y);
                // diff will be a positive number from 0 to 180
                float diff = Mathf.Abs(delta);

                if (diff > 0 && diff < 5)
                {
                    speed = 2;
                }
                else if (diff >= 5 && diff < 10 )
                {
                    speed = 5;
                }
                else if (diff >= 10 && diff < 40)
                {
                    speed = 10;
                }
                else if (diff >=40 && diff < 80)
                {
                    speed = 20;
                }
                else if (diff >= 80 && diff < 120)
                {
                    speed = 30;
                }
                else
                {
                    speed = 40;
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, lob, speed * Time.deltaTime);
                yield return null;
            }
            transform.rotation = lob;
        }

        private void ApplyAlphaAdjustmentToMaterials()
        {
            // Get ref to renderer
            pointerRing_Renderer = PointerRing.GetComponent<Renderer>();

            if (pointerRing_Renderer != null)
            {
                // Copy material array object from renderer
                pointerRing_Materials = pointerRing_Renderer.materials;

                // Decrease transparancy alpha values
                float starting_alpha = 1;
                for (int i = 0; i < pointerRing_Materials.Length; i++)
                {
                    starting_alpha -= 0.04f;
                    Color col = pointerRing_Materials[i].color;
                    col.a = starting_alpha;
                    pointerRing_Materials[i].color = col;
                }
            }

            // Assign back
            pointerRing_Renderer.materials = pointerRing_Materials;
        }

        private float Delta_Bearing(float b1, float b2)
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

