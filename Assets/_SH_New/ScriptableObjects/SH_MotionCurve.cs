using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpectrumHunterClient
{
    [CreateAssetMenu(fileName = "Motion Curve Name", menuName = "SH/Create Motion Curve", order = 0)]
    public class SH_MotionCurve : ScriptableObject
    {
        public AnimationCurve Curve;
        public float Speed = 1f;
        public Vector3 Angle = Vector3.zero;
    }
}

