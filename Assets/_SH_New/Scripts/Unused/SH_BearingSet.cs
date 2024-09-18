using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_BearingSet : MonoBehaviour
    {
        public GameObject Antenna; // Antenna used as rotation starting point when we get the LOB angle from the MC

        void Update()
        {
            Vector3 direction = Antenna.transform.rotation * Vector3.forward;
            Debug.DrawRay(transform.position, direction, Color.green);
        }
    }
}

