using GHIElectronics.TinyCLR.Devices.I2c;
using GHIElectronics.TinyCLR.Pins;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace I2Cscanner
{
    class Program
    {
        static void Main()
        {
            int address = 0;

            byte[] write = { 0, 1, 2 };
            byte[] read = new byte[3];
            for (address = 0; address < 127; address++)
            {
                var settings = new I2cConnectionSettings((byte)(address));
                settings.SharingMode = I2cSharingMode.Shared;
                settings.BusSpeed = I2cBusSpeed.FastMode;

                I2cDevice DisplayDevice = I2cDevice.FromId(FEZ.I2cBus.I2c1, settings);
                DisplayDevice.Write(write);
                Thread.Sleep(100);
                DisplayDevice.Read(read);
                if (read[0] != 0 || read[1] != 0 || read[2] != 0)
                {
                    for(var i = 0; i < read.Length; i++)
                        read[i] = 0;
                    Debug.WriteLine($"It is device on {address.ToString("X2")} address");
                }
                    
                //Debug.WriteLine(read[0].ToString() + " " + read[1].ToString() + " " + read[2].ToString() + "  " + address.ToString("X2"));
            }
            Debug.WriteLine("Finish");
        }
    }
}
