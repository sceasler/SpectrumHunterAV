using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform Target;
    public float RotateSpeed = 100f;
	
	// Update is called once per frame
	void Update () {
        // Start tracking target
        StartCoroutine(TrackRotation(Target));
	}

    IEnumerator TrackRotation(Transform Target)
    {
        while (true)
        {
            if (this.GetComponent<Transform>() != null && Target != null)
            {
                // Get direction to target
                Vector3 relativePos = Target.position - transform.position;

                //Calculate rotation to target
                Quaternion NewRotation = Quaternion.LookRotation(relativePos);

                // Rotate to target by speed
                transform.rotation = Quaternion.RotateTowards(transform.rotation, NewRotation, RotateSpeed * Time.deltaTime);
            }

            //wait for the next frame
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.forward.normalized * 5f);
    }


    // BELOW IS JUST ANOTHER EXAMPLE OF USING A CO-ROUTINE AND SPECIFICALLY TO USE WAITFORSECONDS TO SAVE RESOURSES
    //bool ProximityCheck()
    //{
    //    for (int i = 0; i < enemies.Length; i++)
    //    {
    //        if (Vector3.Distance(transform.position, enemies[i].transform.position) < dangerDistance)
    //        {
    //            // Enemy was detected too close
    //            return true;
    //        }
    //    }
    //    // No enemies detected
    //    return false;
    //}

    //IEnumerator DoCheck()
    //{
    //    for (; ; )
    //    {
    //        ProximityCheck();
    //        yield return new WaitForSeconds(0.1f);  // Greatly reduces the number of checks without any noticable effect on gameplay.
    //    }
    //}
}
