using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemtoBeaconDongle : MonoBehaviour {
	// Attach this script to a mesh. The mesh needs to have a Box Collider attached, with "is trigger" checked.
	// Use this for initialization
	void Start () {
		SerialMonitor.serialPort = "/dev/ttyACM0";
		SerialMonitor.serialBaudRate = 115200;

		SerialMonitor.Open ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if (!SerialMonitor.IsOpen ()) {
			SerialMonitor.Open ();
			Debug.Log ("SerialMonitor.Open()");
		} else {
			SerialMonitor.Close ();
			Debug.Log ("SerialMonitor.Close()");
		}

	}
}
