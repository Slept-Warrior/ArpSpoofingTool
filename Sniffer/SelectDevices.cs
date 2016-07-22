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
using PacketDotNet;
namespace Sniffer
{
    public partial class SelectDevices : Form
    {
        BaseInformation baseInformation;
        CaptureDeviceList devicelist;
        int selectedDeviceIndex;
        public SelectDevices()
        {
            devicelist = CaptureDeviceList.Instance;
            baseInformation = new BaseInformation();
            InitializeComponent();
        }
        
        private void CaptureOption_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < devicelist.Count; i++)
                checkedListBox_Devices.Items.Add(devicelist[i].Description);
            
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            selectedDeviceIndex = checkedListBox_Devices.SelectedIndex;

            string item = checkedListBox_Devices.SelectedItem.ToString();
            this.Hide();
        }
        public int returnDeviceIndex
        {
            get { return selectedDeviceIndex; }
        }
    }
}
