using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Shooter_RunwayLines : MonoBehaviour
{
    public GameObject RingArrowsPrefab;
    public float Rate;

    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > Rate)
        {
            Instantiate(RingArrowsPrefab, transform);
            elapsedTime = 0f;
        }
    }
}
