using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemtoBeaconDongle : MonoBehaviour {
	// Attach this script to a mesh. The mesh needs to have a Box Collider attached, with "is trigger" checked.
	// Use this for initialization

	private IEnumerator coroutineRead;
	private bool isCoroutineRunning = false;

	void Start () {
		SerialMonitor.serialPort = "/dev/ttyACM0";
		SerialMonitor.serialBaudRate = 115200;

		SerialMonitor.Open ();

		if (SerialMonitor.IsOpen ()) {
			isCoroutineRunning = true;
			StartCoroutine (SerialMonitor.ReadAsync(femtoBeaconRx, femtoBeaconFail));
		}
	}

	void femtoBeaconRx(string strLine) {
		Debug.Log ("FemtoBeacon Rx: " + strLine);
	}

	void femtoBeaconFail() {
		Debug.Log ("FemtoBeacon read failed.");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		if (!SerialMonitor.IsOpen ()) {
			SerialMonitor.Open ();

			if (isCoroutineRunning == false) {
				isCoroutineRunning = true;
				StartCoroutine (SerialMonitor.ReadAsync (femtoBeaconRx, femtoBeaconFail));
			}
			Debug.Log ("SerialMonitor.Open()");
		} else {
			SerialMonitor.Close ();
			if (isCoroutineRunning == true) {
				isCoroutineRunning = false;
				StopCoroutine (SerialMonitor.ReadAsync (femtoBeaconRx, femtoBeaconFail));
			}

			Debug.Log ("SerialMonitor.Close()");
		}

	}
}
