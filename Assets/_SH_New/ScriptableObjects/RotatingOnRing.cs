using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New File", menuName = "Custom Files/Create RotatingOnRing", order =0)]
public class RotatingOnRing : ScriptableObject {

    public AnimationCurve AnimationCurve;
    public float Speed = 1f;
    public Vector3 Angle = Vector3.zero;

}
