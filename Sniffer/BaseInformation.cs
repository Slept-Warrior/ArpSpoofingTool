using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using SharpPcap;
using PacketDotNet;
namespace Sniffer
{
    class BaseInformation
    {       
        static public string myMacAddress;
        static public string myIpAddress;
        static public string gatewayMac;
        static public string gatewayIP;
        
        static public string targetIpAddress;
        static public string targetMacAddress;
        static public string EtherMacAddressForArpRequest = "FF-FF-FF-FF-FF-FF";
        static public string ArpMacAddressForArpRequest = "00-00-00-00-00-00";
        static public ICaptureDevice captureDevice;

        static public List<string> searched_IP_list = new List<string>();
        static public List<string> searched_MAC_list = new List<string>();
        static public List<string> attack_IP_list = new List<string>();
        static public List<string> attack_MAC_list = new List<string>();
        public void getDefaultGateway()
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
                             if(gatewayIP == null)
                             gatewayIP = address.Address.ToString();
                         }
                    }
                }

            }
        }
        public void sendArpRequest()
        {
            int readTimeout = 1000;

            // 현재 단말기의 네트워크 장치의 리스트들을 불러온다.

            // 무선 랜카드의 인덱스 번호는 1번(단말기 설정에 따라 다름)
            ICaptureDevice device = BaseInformation.captureDevice;


            // 무선 랜카드를 프러미스큐어스 모드로 연다.
            device.Open(DeviceMode.Promiscuous, readTimeout);
            IPAddress myIP = null;
            IPAddress gatewayIP = null;
            PhysicalAddress EtherArpRequestMac = null;
            PhysicalAddress srcMac = null;
            PhysicalAddress ArpRequestMac = null;
       
            EtherArpRequestMac = PhysicalAddress.Parse(BaseInformation.EtherMacAddressForArpRequest.ToUpper());
            ArpRequestMac = PhysicalAddress.Parse(BaseInformation.ArpMacAddressForArpRequest.ToUpper());
            myIP = IPAddress.Parse(BaseInformation.myIpAddress); 
            gatewayIP = IPAddress.Parse(BaseInformation.gatewayIP); //아이피에 게이트웨이 주소를 넣어주어야 한다.
            srcMac = PhysicalAddress.Parse(BaseInformation.myMacAddress.ToUpper());

            //타겟 Infection

            EthernetPacket eth = new EthernetPacket(srcMac, EtherArpRequestMac, EthernetPacketType.Arp);
            ARPPacket arp = new ARPPacket(ARPOperation.Request, ArpRequestMac, gatewayIP, srcMac, myIP);




            eth.PayloadPacket = arp;

            device.SendPacket(eth);
        }
        public void getMyMacAddress()
        {
            myMacAddress = captureDevice.MacAddress.ToString();
        }
        public void getMyIpAddress()
        {
            string Localip = "?";
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {

                var defaultGateway = from nics in NetworkInterface.GetAllNetworkInterfaces()


                                     from props in nics.GetIPProperties().GatewayAddresses
                                     where nics.OperationalStatus == OperationalStatus.Up
                                     select props.Address.ToString(); // this sets the default gateway in a variable

                GatewayIPAddressInformationCollection prop = netInterface.GetIPProperties().GatewayAddresses;

                if (defaultGateway.First() != null)
                {

                    IPInterfaceProperties ipProps = netInterface.GetIPProperties();

                    foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                    {

                        if (addr.Address.ToString().Contains(defaultGateway.First().Remove(defaultGateway.First().LastIndexOf(".")))) // The IP address of the computer is always a bit equal to the default gateway except for the last group of numbers. This splits it and checks if the ip without the last group matches the default gateway
                        {

                            if (Localip == "?") // check if the string has been changed before
                            {
                                myIpAddress = addr.Address.ToString(); // put the ip address in a string that you can use.
                                return;
                            }
                        }

                    }

                }

            }
           
        }
 
    }
}
