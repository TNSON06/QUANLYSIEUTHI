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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetCBBMH();
            SetCBBNSX();
            SetCBBSort();
        }
        private void ShowDGV(string name)
        {
            dataGridView1.DataSource = BUS_ST.Instance.GetListSP(name);
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            ShowDGV(null);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            ShowDGV(txtB_Search.Text);
        }
        //cho MH
        public void SetCBBMH()
        {
           cbb_MH.Items.Add(new CBBItem
            {
                value = 0,
                Text = "All"
            });
            cbb_MH.Items.AddRange(BUS_ST.Instance.GetListCBBMH().ToArray());
        }
        // cho NSX
        public void SetCBBNSX()
        {
            cbb_NSX.Items.Add(new CBBItem
            {
                value = 0,
                Text = "All"
            });
            cbb_NSX.Items.AddRange(BUS_ST.Instance.GetListCBBNSX().ToArray());
        }
        //cho sort
        public void SetCBBSort()
        {
            cbb_Sort.Items.AddRange(BUS_ST.Instance.GetListCBBSort().ToArray());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy hàng đang được chọn
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                // Lấy giá trị của các ô trong hàng đang được chọn
                txtB_MaSP.Text = selectedRow.Cells["MaSP"].Value.ToString();
                txtB_TenSP.Text = selectedRow.Cells["TenSP"].Value.ToString();
                dateTimePicker1.Value = (DateTime)selectedRow.Cells["NNH"].Value;

                cbb_NSX.Text = selectedRow.Cells["NSX"].Value.ToString();
                if (selectedRow.Cells["TinhTrang"].Value.ToString() == "True")
                {
                    rbtn_ConHang.Checked = true;
                    rbtn_HetHang.Checked = false;

                }
                else if (selectedRow.Cells["TinhTrang"].Value.ToString() == "False")
                {
                    rbtn_ConHang.Checked = false;
                    rbtn_HetHang.Checked = true;
                }
                else
                {
                    rbtn_ConHang.Checked = false;
                    rbtn_HetHang.Checked = false;
                }
                int maMHToSelect = (int)selectedRow.Cells["MaMH"].Value;

                // Duyệt qua danh sách mục trong ComboBox
                for (int i = 0; i < cbb_MH.Items.Count; i++)
                {
                    CBBItem item = (CBBItem)cbb_MH.Items[i];
                    if (item.value == maMHToSelect)
                    {
                        // Nếu tìm thấy giá trị, chọn nó
                        cbb_MH.SelectedIndex = i;
                        break;
                    }
                }
            }

        }
       
        public void ClearComponents()
        {
            txtB_MaSP.Text = string.Empty;
            txtB_TenSP.Text = string.Empty;
            
            dateTimePicker1.Value = DateTime.Now;

            rbtn_ConHang .Checked = false;
            rbtn_HetHang.Checked = false;

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            bool? tt = null;
            if (rbtn_ConHang.Checked == false && rbtn_HetHang.Checked == true)
            {
                tt = false;
            }
            else if (rbtn_ConHang.Checked == true && rbtn_HetHang.Checked == false)
            {
                tt = true;
            }
            else if (rbtn_ConHang.Checked == false && rbtn_HetHang.Checked == false)
            {
                tt = null;
            }

            CBBItem selectedCBBItem = (CBBItem)cbb_MH.SelectedItem;

            int itemValueMH = (selectedCBBItem != null) ? selectedCBBItem.value : 0;

            SP s = new SP
            (
                 int.TryParse(txtB_MaSP.Text, out int maSP) ? maSP : 0,
                txtB_TenSP.Text.ToString(),
              (DateTime?)dateTimePicker1?.Value,
               cbb_NSX.Text,
                 tt,
              itemValueMH
            );

            if (s != null)
            {
                BUS_ST.Instance.ExcuteDB(s);
            }
            ShowDGV(null);
            MessageBox.Show("Update thanh cong");
            ClearComponents();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<string> LMSP = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {

                    LMSP.Add(row.Cells["MaSP"].Value.ToString());
                }
                BUS_ST.Instance.DelSV(LMSP);

                ShowDGV(null);
                MessageBox.Show("Xoa thanh cong");
            }
            else
            {
                MessageBox.Show("Chon hang di cu");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtB_MaSP.Enabled = false;
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            List<SP> listSP = new List<SP>();
            if (txtB_Search.Text.Length == 0)
            {
                listSP = BUS_ST.Instance.GetListSP(null);
            }
            else
            {
                listSP = BUS_ST.Instance.GetListSP(txtB_Search.Text);
            }





            switch (cbb_Sort.Text)
            {
                case "MaSP":
                    listSP.Sort((x, y) => x.MaSP.CompareTo(y.MaSP));
                    MessageBox.Show("Sap xep thanh cong");
                    break;
                case "TenSP":
                    listSP.Sort((x, y) => string.Compare(x.TenSP, y.TenSP));
                    MessageBox.Show("Sap xep thanh cong");
                    break;
                case "MaMH":
                    listSP.Sort((x, y) => x.MaMH.CompareTo(y.MaMH));
                    MessageBox.Show("Sap xep thanh cong");
                    break;
                case "NNH":
                    listSP = listSP.OrderBy(x => x.NNH).ToList();
                    MessageBox.Show("Sap xep thanh cong");
                    break;
                default:
                    MessageBox.Show("Chon thuoc tinh di cu");
                    break;
            }




            dataGridView1.DataSource = listSP;
            dataGridView1.Show();

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            AddForm add = new AddForm();
            add.d = new AddForm.MyDel(ShowDGV);
            add.Show();
        }
    }
}
