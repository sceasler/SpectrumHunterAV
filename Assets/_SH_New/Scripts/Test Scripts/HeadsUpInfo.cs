using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This is currently just displaying the time for the user
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class HeadsUpInfo : MonoBehaviour {

    private TextMeshProUGUI dateTimeText;

	// Use this for initialization
	void Start () {
        dateTimeText = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        //TimeZoneInfo localZone = TimeZoneInfo.Local;
        //string s = localZone.StandardName;
        dateTimeText.text = DateTime.Now.ToString($"HH:mm");
        //dateTimeText.text = DateTime.Now.ToShortTimeString($"HH:mm");

    }
}
