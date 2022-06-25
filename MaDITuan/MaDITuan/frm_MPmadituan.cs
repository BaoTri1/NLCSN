using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MaDITuan
{
    public partial class frm_MPmadituan : Form
    {
        private System.Media.SoundPlayer soundplayer;
        private String sound_wait = Application.StartupPath + "\\Music\\action-110116.wav";
        private String sound_toplay = Application.StartupPath + "\\Music\\durante-9005.wav";
        private String sound_readyGo = Application.StartupPath + "\\Music\\y2mate.com-Ready-Go-Memes.wav";
        private KnightTrip knighttrip;

        const String BOT = "Bot";
        const String BOT_NEXTLOCATION = "Bot_NextLocation";
        const String PLAYER = "Player";

        private String[] toadoX;
        private int[] toadoY;

        
        private int x, y, n, speed = 0, count = 0;

        private int buocdi;

        public frm_MPmadituan()
        {
            InitializeComponent();
        }

        private void frm_MPmadituan_Load(object sender, EventArgs e)
        {
            playSound(sound_wait);
            btnBatDau.Enabled = false;
            btnNgung.Enabled = false;
            btnLamMoi.Enabled = false;
            btnLuuTru.Enabled = false;
            txt_td_i.ReadOnly = true;
            txt_td_j.ReadOnly = true;
        }

        private void playSound(String path)
        {
            soundplayer = new System.Media.SoundPlayer(path);
            soundplayer.PlayLooping();
        }

        private void luâtChơiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_LuatChoi lc = new frm_LuatChoi();
            lc.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            if(txtnxn.Text == "")
            {
                MessageBox.Show("Chưa nhập n!!!.", "Thông Báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtnxn.Focus();
            }
            else
            {
                try
                {
                    this.n = Convert.ToInt32(txtnxn.Text);

                    if(n >= 4 && n <= 10)
                    {

                        knighttrip = new KnightTrip(n);

                        //Vẽ bàn cờ nxn
                        knighttrip.Ve_BanCo(panel_BanCo);

                        btnBatDau.Enabled = true;
                        btnLamMoi.Enabled = true;
                        btnKhoiTao.Enabled = false;
                        txt_td_i.ReadOnly = false;
                        txt_td_j.ReadOnly = false;

                    }
                    else
                    {
                        MessageBox.Show("n không hợp lệ !!!. 4 <= n <= 10.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("n không hợp lệ !!!. n phải là số nguyên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = knighttrip.getDD_X()[this.buocdi];
            int y = knighttrip.getDD_Y()[this.buocdi];

            knighttrip.Clear(knighttrip.getbanco().getMang()[x, y].getO_Co());
            knighttrip.getbanco().getMang()[x, y].getO_Co().Invalidate();
            knighttrip.Ve_goal(knighttrip.getbanco().getMang()[x, y].getO_Co(), this.buocdi);


            this.toadoX[this.count] = chuyendoi_chu(x);
            this.toadoY[this.count] = y;
            this.count++;

            if (this.buocdi == this.n * this.n)
            {
                timer1.Enabled = false;
                DialogResult result = MessageBox.Show("Đã hoàn thành hành trình đi tuần của quân MÃ. ", "Chúc mừng Bạn!!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    soundplayer.Stop();
                    playSound(sound_wait);

                    btnLuuTru.Enabled = true;

                    lbl_Text_TongSoODaDi.Text = Convert.ToString(knighttrip.getGoal());
                    lbl_Text_Kq.Text = "Đã hoàn thành.";
                    in_toado();
                }
            }
            else
            {
                this.buocdi++;

                int x_next = knighttrip.getDD_X()[this.buocdi];
                int y_next = knighttrip.getDD_Y()[this.buocdi];

                if(x_next == -1 && y_next == -1)
                {
                    timer1.Enabled = false;
                    DialogResult result = MessageBox.Show("Không tìm thấy hành trình đi tuần của quân MÃ. ", "Rất tiếc!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        soundplayer.Stop();
                        playSound(sound_wait);

                        btnLuuTru.Enabled = true;

                        lbl_Text_TongSoODaDi.Text = Convert.ToString(knighttrip.getGoal());
                        lbl_Text_Kq.Text = "Không hoàn thành.";
                        in_toado();
                    }
                }
                else
                {
                    knighttrip.getbanco().getMang()[x_next, y_next].getO_Co().Invalidate();

                    knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[x_next, y_next].getO_Co(), BOT);
                }

            }
        }

        private void btnNgung_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btnNgung.Text = "Tiếp Tục";
            btnNgung.Click += btnTiepTuc_Click;
        }

        private void btnTiepTuc_Click(Object sender, EventArgs e)
        {
            timer1.Enabled = true;
            btnNgung.Text = "Tạm Dừng";
            btnNgung.Click += btnNgung_Click;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void chơiĐốiKhángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_PlayerToPlay game = new frm_PlayerToPlay();
            game.ShowDialog();
        }

        private void môPhỏngMãĐiTuầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_MPmadituan mophong = new frm_MPmadituan();
            mophong.ShowDialog();
        }

        private void btnLuuTru_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|*.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(save.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Kích thước bàn cờ: " + txtnxn.Text + " x " + txtnxn.Text);
                streamWriter.WriteLine("Tọa độ xuất phát: (" + txt_td_i.Text + ", " + txt_td_j.Text + ")");
                streamWriter.WriteLine("Tổng số ô: " + lbl_Text_TongSoO.Text);
                streamWriter.WriteLine("Tổng số ô đi được: " + lbl_Text_TongSoODaDi.Text);
                streamWriter.WriteLine("Toạ độ các điểm đi lần lượt là: ");
                for (int i = 0; i < this.n * this.n; i++)
                    streamWriter.WriteLine("Bước " + (i + 1) + ": (" + this.toadoX[i] + ", " + this.toadoY[i] + ").");
                streamWriter.WriteLine("Kết quả: " + lbl_Text_Kq.Text);
                streamWriter.WriteLine("");
                streamWriter.WriteLine("Ghi chú: (null, -1) là tọa độ ko tồn tại.");
                streamWriter.Close();
                fileStream.Close();
            }

            MessageBox.Show("Đã lưu file thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {

            try
            {
                this.x = chuyendoi_so(txt_td_i.Text);
                this.y = Convert.ToInt32(txt_td_j.Text) - 1;

                if (x >= 0 && x < n && y >= 0 && y < n)
                {
                    knighttrip.getbanco().getMang()[x, y].getO_Co().Invalidate();
                    //Vẽ quân mã tại vị trí (x, y)
                    knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[x, y].getO_Co(), BOT);

                    //Tạo đường đi
                    knighttrip.madituan(this.x, this.y);

                    this.buocdi = 1;

                    this.toadoX = new String[this.n * this.n];
                    this.toadoY = new int[this.n * this.n];

                    for (int i = 0; i < this.n * this.n; i++)
                    {
                        this.toadoX[i] = null;
                        this.toadoY[i] = -1;
                    }

                    lbl_Text_BanCo.Text = Convert.ToString(this.n) + " x " + Convert.ToString(this.n);
                    lbl_Text_TongSoO.Text = Convert.ToString(this.n * this.n);
                    lbl_Text_ToaDoI.Text = txt_td_i.Text;
                    lbl_Text_ToaDoJ.Text = txt_td_j.Text;

                    //Tắt nhạc chờ
                    soundplayer.Stop();

                    //Tạo âm thanh mới;            
                    playSound(sound_toplay);

                    //Gọi thuật toán bắt đầu đi 

                    timer1.Enabled = true;

                    btnNgung.Enabled = true;
                    btnLuuTru.Enabled = true;
                    btnBatDau.Enabled = false;

                }
                else
                {
                    MessageBox.Show("tọa độ i hoặc j không hợp lệ !!!. i phải là chữ cái In Hoa (A -> J), 0 <= j < ." + txtnxn.Text, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("tọa độ i hoặc j không hợp lệ !!!. i phải là chữ cái in Hoa và j phải là số nguyên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            panel_BanCo.Controls.Clear();
            txtnxn.Text = "";
            txt_td_i.Text = "";
            txt_td_i.ReadOnly = true;
            txt_td_j.ReadOnly = true;
            txt_td_j.Text = "";
            btnLamMoi.Enabled = false;
            btnKhoiTao.Enabled = true;

            lbl_Text_BanCo.Text = "_____";
            lbl_Text_TongSoO.Text = "_____";
            lbl_Text_TongSoODaDi.Text = "_____";
            lbl_Text_ToaDoI.Text = "_____";
            lbl_Text_ToaDoJ.Text = "_____";
            lbl_Text_Kq.Text = "_____";
            this.count = 0;

            panel_cactoado.Controls.Clear();

        }

        private void cmbSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (cmbSpeed.Text == "0.5")
            {
                this.speed = 500;
                timer1.Interval = this.speed;
                timer2.Interval = this.speed;

            }
           else if(cmbSpeed.Text == "0.1")
           {
                this.speed = 100;
                timer1.Interval = this.speed;
                timer2.Interval = this.speed;
            }
           else
           {
                this.speed = Convert.ToInt32(cmbSpeed.Text) * 1000;
                timer1.Interval = this.speed;
                timer2.Interval = this.speed;

           }
        }

        public void in_toado()
        {
            for(int i = 0; i < this.n * this.n; i++)
            {
                if(this.toadoX[i] != null && this.toadoY[i] != -1)
                {
                    Label lbl = new Label();
                    {
                        lbl.Text = "Bước " + (i + 1) + ": (" + this.toadoX[i] + ", " + (this.toadoY[i] + 1) + "), ";
                        Font font = new Font("Microsoft Sans Serif", 9);
                        lbl.Font = font;
                        lbl.ForeColor = Color.Blue;
                    }

                    panel_cactoado.Controls.Add(lbl);
                }
            }
        }

        public String  chuyendoi_chu(int n)
        {
            if (n == 0)
            {
                return "A";
            }
            else if (n == 1)
            {
                return "B";
            }
            else if (n == 2)
            {
                return "C";
            }
            else if (n == 3)
            {
                return "D";
            }
            else if (n == 4)
            {
                return "E";
            }
            else if (n == 5)
            {
                return "F";
            }
            else if (n == 6)
            {
                return "G";
            }
            else if (n == 7)
            {
                return "H";
            }
            else if (n == 8)
            {
                return "I";
            }
            else if (n == 9)
            {
                return "J";
            }
            else return null;
        }
        public int chuyendoi_so(String str)
        {
            if (str == "A")
            {
                return 0;
            }
            else if (str == "B")
            {
                return 1;
            }
            else if (str == "C")
            {
                return 2;
            }
            else if (str == "D")
            {
                return 3;
            }
            else if (str == "E")
            {
                return 4;
            }
            else if (str == "F")
            {
                return 5;
            }
            else if (str == "G")
            {
                return 6;
            }
            else if (str == "H")
            {
                return 7;
            }
            else if (str == "I")
            {
                return 8;
            }
            else if (str == "J")
            {
                return 9;
            }
            else return -1;
        }
    }
}
