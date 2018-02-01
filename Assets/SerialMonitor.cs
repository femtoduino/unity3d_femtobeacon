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
	public string serialPort = "/dev/ttyACM0"; // Windows ports are enumerated as "COM1", "COM3", "COM4", etc..
	public int serialBaudRate = 115200;
	public int serialReadTimeout = 50;
	public System.IO.Ports.SerialPort stream = null;

	// Use this for initialization
	void Start () {
		stream = new SerialPort (serialPort, serialBaudRate);
		stream.ReadTimeout = serialReadTimeout;

		stream.Open ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public string read (int timeout = 0) {
		stream.ReadTimeout = timeout || serialReadTimeout;

		try {
			return stream.ReadLine();
		} catch (TimeoutException te) {
			Debug.Log("SerialMonitor.read() A timeout exception occurred");
			return null;
		}
	}

	public IEnumerator readAsync(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity) {
		DateTime initialTime = DateTime.Now;
		DateTime nowTime;
		TimeSpan diff = default(TimeSpan);

		string dataString = null;

		do {
			try {
				dataString = stream.ReadLine();
			} catch (TimeoutException) {
				Debug.Log("SerialMonitor.readAsync() A timeout exception occurred");
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
