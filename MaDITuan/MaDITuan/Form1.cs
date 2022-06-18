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
    public partial class Form1 : Form
    {
        String s;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("cc ne");
            if(s == "a")
            {
                timer1.Stop();
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            s = "a";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s = "b";
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
