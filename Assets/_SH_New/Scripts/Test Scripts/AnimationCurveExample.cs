using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class AnimationCurveExample : MonoBehaviour {

    public float Speed = 1f;
    public Vector3 Direction = Vector3.zero;
    public AnimationCurve Curve;

    private float elapsedTime = 0.0f;
    private float curveTime;

	void Start () {
        curveTime = Curve.keys[Curve.length - 1].time;
	}
	
	void Update () {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= curveTime)
        {
            Direction *= -1;
            elapsedTime = 0.0f;
        }
        transform.position += Direction.normalized * Speed * Curve.Evaluate(Time.time) * Time.deltaTime;
	}
}
