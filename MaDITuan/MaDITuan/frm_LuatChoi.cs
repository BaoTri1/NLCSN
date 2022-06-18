using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaDITuan
{
    public partial class frm_LuatChoi : Form
    {
        public frm_LuatChoi()
        {
            InitializeComponent();
        }

        private void rbtnMoPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMoPhong.Checked == true)
            {
                panelDoiKhang.Visible = false;
                panelMoPhong.Visible = true;
            }
        }

        private void rbtnDoiKhang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDoiKhang.Checked == true)
            {
                panelDoiKhang.Visible = true;
                panelMoPhong.Visible = false;
            }
        }

        private void lblGhiChu7_Click(object sender, EventArgs e)
        {

        }
    }
}
