using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_ArrowBullet : MonoBehaviour
    {
        public float speed = 1f;
        public float TimeTillDisable = 20f;

        private float elapsedTime = 0f;
        private bool isPaused = false;
        private bool isStale = false;
        private float staleTime = 0f;

        private void Start()
        {
            SH_EventManager.Instance.PauseLOBAnimation += SH_EventManager_SetPause;
            SH_EventManager.Instance.StaleLOBAnimation += SH_EventManager_SetStale;
        }

        private void SH_EventManager_SetStale(bool isStale)
        {
            this.isStale = isStale;
        }

        private void SH_EventManager_SetPause(bool isPaused)
        {
            this.isPaused = isPaused;
            if (!isPaused)
            {
                isStale = false;
                staleTime = 0f;
            }
        }

        void Awake()
        {
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (!isPaused)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > TimeTillDisable)
                {
                    elapsedTime = 0f;
                    gameObject.SetActive(false);
                }

                this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            if (isStale)
            {
                staleTime += Time.deltaTime;
                if (staleTime > TimeTillDisable - elapsedTime)
                {
                    gameObject.SetActive(false);
                    isStale = false;
                }
            }
        }
    }
}
