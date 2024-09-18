using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SpectrumHunterClient
{
/// <summary>
/// This is not being used.  Just keeping for reference for how to use Unity provided UnityEvents for the inspector.
/// </summary>
    public class SH_BearingTrigger : MonoBehaviour
    {
        [Header("Callbacks")]
        [SerializeField] public UnityEvent OnEnter;
        [SerializeField] public UnityEvent OnExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == this.name)
            {
                OnEnter?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name == this.name)
            {
                OnEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == this.name)
            {
                OnExit?.Invoke();
            }
        }
    }
}

