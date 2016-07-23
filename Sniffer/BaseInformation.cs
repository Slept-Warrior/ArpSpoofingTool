using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using SharpPcap;
namespace Sniffer
{
    class BaseInformation
    {
        static public string gatewayMac;
        static public string gatewayIP;
        static public string myMacAddress;
        static public string targetIpAddress;
        static public string targetMacAddress;
        static public ICaptureDevice captureDevice;
        public void getDefaultGateway()     //게이트웨이의 IP와 MAC 주소를 구해온다.
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
                if (addresses.Count > 0)
                {
                    Console.WriteLine(adapter.Description);
                    foreach (GatewayIPAddressInformation address in addresses)
                    {
                         gatewayIP = address.Address.ToString();
                         System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                         pProcess.StartInfo.FileName = "arp";
                         pProcess.StartInfo.Arguments = "-a " + gatewayIP;
                         pProcess.StartInfo.UseShellExecute = false;
                         pProcess.StartInfo.RedirectStandardOutput = true;
                         pProcess.StartInfo.CreateNoWindow = true;
                         pProcess.Start();
                         string strOutput = pProcess.StandardOutput.ReadToEnd();
                         string[] substrings = strOutput.Split('-');
                         if (substrings.Length >= 8)
                         { 
                             gatewayMac = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                             + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                             + "-" + substrings[7] + "-" + substrings[8].Substring(0, 2);
                         }
                    }
                }

            }
        }

        public void getMyMacAddress()
        {
            myMacAddress = captureDevice.MacAddress.ToString();
        }
    }
}
