
using UnityEngine;
[CreateAssetMenu(fileName = "3DAppLauncher", menuName = "3D App Launcher")]
public class AppLauncher3D : ScriptableObject

{
    [Tooltip("Path to glb relative to Assets folder. Include file extension.")]
    public string Model;

    [Tooltip("Set to override center and bonding box of 3D asset.")]
    public bool OverrideBoundingBox = false;

    [Tooltip("Center used if override bounding box set.")]
    public Vector3 Center;

    [Tooltip("Bounding box extents used if override bounding box set.")]
    public Vector3 Extents = Vector3.one;
}