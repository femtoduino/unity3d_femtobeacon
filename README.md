# Unity3D + FemtoBeacon

This is an example project to demonstrate how to read wireless FemtoBeacon coin sensors data to rotate a mesh in Unity3D.

Hardware available on Tindie: 

 * 1 Dongle, 1 Coin [https://www.tindie.com/products/femtoduino/femtobeacon-kit-basic/]
 * 1 Dongle, 5 Coins [https://www.tindie.com/products/femtoduino/femtobeacon-kit-starter/]

## Notes

Windows users will need to edit the setup in FemtoBeaconDongle.cs to specify a COM port.
GNU/Linux, Mac will need to specify the correct port path instead (Ubuntu "/dev/ttyACM0" when the dongle is connected first)

Plug in the dongle, start the Unity player, then click the mesh. Clicking the mesh toggles the serial port between open/close.

The console should show debug logs. You can read this to see the status of the serial port (open/close).

Once the serial port to the dongle is open, power up your coin. Give it a few seconds (standing still, antenna face down) for it to power up and connect to the mesh networked dongle.

The femtoBeacon coin will begin transmission after the IMU starts, at which point you should be able to rotate the mesh using a femtoBeacon coin.


