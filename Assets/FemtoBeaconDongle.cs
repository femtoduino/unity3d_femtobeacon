using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemtoBeaconDongle : MonoBehaviour {
	// Attach this script to a mesh. The mesh needs to have a Box Collider attached, with "is trigger" checked.
	// Use this for initialization

	private IEnumerator coroutineRead;
	private bool isCoroutineRunning = false;

	private float eulerX = 0.0f;
	private float eulerY = 0.0f;
	private float eulerZ = 0.0f;

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
		
		string[] arrData;
		// Process the incomming data

		if (strLine.Trim() != "") {
			// Data format is:

			// APP_ADDRESS (the dongle node ID)
			// APP_PANID
			// APP_CHANNEL
			// Source Node ID (the coin node ID)
			// timestamp (relative to when the coin was powered on)
			// yaw (relative to Earth)
			// pitch (relative to Earth)
			// roll (relative to Earth)
			// euler 1
			// euler 2
			// euler 3
			// accel X
			// accel Y
			// accel Z

			arrData = strLine.Split (',');

			var appAddress = arrData [0];
			var appPanId = arrData [1];
			var appChannel = arrData [2];
			var sourceNodeId = arrData [3];
			var timestamp = arrData [4];
			var yaw = arrData [5];
			var pitch = arrData [6];
			var roll = arrData [7];
			var euler1 = arrData [8];
			var euler2 = arrData [9];
			var euler3 = arrData [10];
			var accelX = arrData [11];
			var accelY = arrData [12];
			var accelZ = arrData [13];

			Debug.Log ("femtoBeaconRx() called:" + strLine);

			// Update our euler values!

			eulerX = float.Parse(euler1);
			eulerY = float.Parse(euler2);
			eulerZ = float.Parse(euler3);
		}
	}

	void femtoBeaconFail() {
		Debug.Log ("FemtoBeacon read failed.");
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(eulerX, eulerY, eulerZ);
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
