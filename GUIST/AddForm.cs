using BUSST;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIST
{
    public partial class AddForm : Form
    {
        private string MSP;
        public delegate void MyDel(string name);
        public MyDel d { get; set; }
        public AddForm()
        {
            InitializeComponent();
            SetCBBMH();
            SetCBBNSX();
        }
        public void SetCBBMH()
        {
            cbb_MH_add.Items.Add(new CBBItem
            {
                value = 0,
                Text = "All"
            });
            cbb_MH_add.Items.AddRange(BUS_ST.Instance.GetListCBBMH().ToArray());
        }
        public void SetCBBNSX()
        {
            cbb_NSX_add.Items.Add(new CBBItem
            {
                value = 0,
                Text = "All"
            });
            cbb_NSX_add.Items.AddRange(BUS_ST.Instance.GetListCBBNSX().ToArray());
        }

        private void btn_OK_add_Click(object sender, EventArgs e)
        {
            bool? tt = null;
            if (rbtn_ConHang_add.Checked == false && rbtn_HetHang_add.Checked == true)
            {
                tt = false;
            }
            else if (rbtn_ConHang_add.Checked == true && rbtn_HetHang_add.Checked == false)
            {
                tt = false;
            }
            else if (rbtn_ConHang_add.Checked == false && rbtn_HetHang_add.Checked == false)
            {
                tt = null;
            }

            CBBItem selectedCBBItem = (CBBItem)cbb_MH_add.SelectedItem;

            int itemValueMH = (selectedCBBItem != null) ? selectedCBBItem.value : 0;

            SP s = new SP
            (
                 int.TryParse(txtB_MaSP_add.Text, out int maSP) ? maSP : 0,
                txtB_TenSP_add.Text,
              (DateTime?)dateTimePicker1?.Value,
               cbb_NSX_add.Text,
                 tt,
              itemValueMH
            );
            if (s != null)
            {
                BUS_ST.Instance.ExcuteDB(s);

            }
            MessageBox.Show("Them thanh cong");
            d(null);
            this.Dispose();
        }

        private void btn_Cal_add_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
