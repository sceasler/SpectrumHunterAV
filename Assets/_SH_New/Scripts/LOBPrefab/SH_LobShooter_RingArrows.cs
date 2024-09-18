using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_LobShooter_RingArrows : MonoBehaviour
    {

        public GameObject RingArrowsPrefab;
        public float Rate;

        private float elapsedTime = 0f;
        private bool isPaused = false;

        private void Start()
        {
            SH_EventManager.Instance.PauseLOBAnimation += SH_EventManager_SetPause;
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
                if (elapsedTime > Rate)
                {
                    Instantiate(RingArrowsPrefab, transform);
                    elapsedTime = 0f;
                }
            }
        }
    }
}


