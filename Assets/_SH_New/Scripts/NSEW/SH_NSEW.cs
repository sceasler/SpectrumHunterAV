using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_NSEW : MonoBehaviour
    {
        public GameObject Antenna;

        private void LateUpdate()
        {
            transform.position = Camera.main.transform.position;
            transform.rotation = Antenna.transform.rotation;
        }
    }
}


