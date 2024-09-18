using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_PointerArrowAntenna : MonoBehaviour
    {
        public GameObject PointerRing;

        private Material[] pointerRing_Materials;
        private Renderer pointerRing_Renderer;

        private void Start()
        {
            SH_EventManager.Instance.UpdatePointerArrowRotation += LobRotate;
            ApplyAlphaAdjustmentToMaterials();
        }

        public void LobRotate(Quaternion rotation)
        {
            transform.rotation = transform.parent.rotation * rotation;
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
    }
}