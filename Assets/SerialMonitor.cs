using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

/**
 * Examples used:
 *   Arduino Connector by Alan Zucconi
 *   http://www.alanzucconi.com/?p=2979
 * 
 * Requires:
 *  - Go to Edit
 *          > Player Settings
 * 
 *  ... API Compatibility must be set to ".NET 2.0"
 */

public class SerialMonitor : MonoBehaviour {
	[Tooltip("Serial Port")]
	public static string serialPort = "/dev/ttyACM0"; // Windows ports are enumerated as "COM1", "COM3", "COM4", etc..
	[Tooltip("Serial Baud Rate")]
	public static int serialBaudRate = 115200;
	[Tooltip("Serial Read Timeout")]
	public static int serialReadTimeout = 50;
	public static System.IO.Ports.SerialPort stream = null;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public static void Open() {
		stream = new SerialPort (serialPort, serialBaudRate);
		stream.ReadTimeout = serialReadTimeout;

		stream.Open ();
	}

	public static bool IsOpen() {
		return stream.IsOpen;
	}

	public static void Close() {
		stream.Close ();
	}

	public static string Read () {
		stream.ReadTimeout = serialReadTimeout;

		try {
			return stream.ReadLine();
		} catch (TimeoutException te) {
			Debug.Log("SerialMonitor.read() A timeout exception occurred");
			Debug.Log (te);
			return null;
		}
	}

	public static IEnumerator ReadAsync(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity) {
		DateTime initialTime = DateTime.Now;
		DateTime nowTime;
		TimeSpan diff = default(TimeSpan);

		string dataString = null;

		do {
			try {
				dataString = stream.ReadLine();
			} catch (TimeoutException te) {
				Debug.Log("SerialMonitor.readAsync() A timeout exception occurred" + te.ToString());

				dataString = null;
			}

			if (null != dataString) {
				callback(dataString);
				yield return null;
			} else {
				yield return new WaitForSeconds(0.05f);
			}
			
		} while (diff.Milliseconds < timeout);

		if (null != fail) {
			fail ();
		}

		yield return null;
	}
}
