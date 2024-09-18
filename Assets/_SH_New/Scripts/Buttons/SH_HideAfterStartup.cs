using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tool used to set game objects hidden at start.
/// Note: GameObject.Find() will only find "Active" objects, therefore make sure to
/// order the ObjectsToHideAtStartup array so that child objects are listed BEFORE the parent.
/// </summary>
public class SH_HideAfterStartup : MonoBehaviour {

    public bool doHideAfterStartup = true;
    public float hideAfterSeconds = 1f;
    [Tooltip("List of objects to hide at startup.")]
    public GameObject[] ToHideAtStartup;

    
    private float elapsedTime = 0f;

	void Update () {
        if (!doHideAfterStartup)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
		if (elapsedTime > hideAfterSeconds)
        {
            foreach (GameObject go in ToHideAtStartup)
            {
                go.SetActive(false);
            }
            doHideAfterStartup = false;
        }
	}
}
