using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaDITuan
{
    public partial class frm_PlayerToPlay : Form
    {
        private System.Media.SoundPlayer soundplayer;
        private String sound_wait = Application.StartupPath + "\\Music\\action-110116.wav";
        private String sound_toplay = Application.StartupPath + "\\Music\\let-the-games-begin-21858.wav";

        private PlayerToPlay game;

        private int n = 0;

        private int TGcho = 10000;

        private static String namePlayer;

        private  static Boolean Stop;

        private SqlConnection conn;

        private SqlCommand cmd;

        Decimal kq_tmp = 0;

        public static string NamePlayer { 
            get => namePlayer; 
            set => namePlayer = value; 
        }

        public static bool Stop1 {
            get => Stop;
            set => Stop = value; 
        }

        public frm_PlayerToPlay()
        {
            InitializeComponent();
            Stop1 = false; 
        }

        private void frm_FightingGame_Load(object sender, EventArgs e)
        {
            playSound(sound_wait);

            try
            {
                conn = new SqlConnection("Server = MSI\\SQLEXPRESS; Initial Catalog = GameMDT; User Id = sa; pwd = 123456");
                cmd = new SqlCommand("Select * from KiLuc;", conn);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

            btnLamMoi.Enabled = false;
            btnLuu.Enabled = false;

        }

        private void playSound(String path)
        {
            soundplayer = new System.Media.SoundPlayer(path);
            soundplayer.PlayLooping();
        }

        private void luậtChơiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_LuatChoi lc = new frm_LuatChoi();
            lc.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            conn.Close();
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            if (cmbBanCo.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bàn cờ để bắt đầu tạo ván chơi.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cmbBanCo.Text == "8 x 8")
            {
                this.n = 8;
            }
            else if (cmbBanCo.Text == "5 x 5")
            {
                this.n = 5;
            }
            else if (cmbBanCo.Text == "10 x 10")
            {
                this.n = 10;
            }

            if (txtName.Text == "")
            {
                MessageBox.Show("Tên Người chơi không được để trống!!!.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                NamePlayer = txtName.Text;
                int flag = 0;

                try
                {
                    cmd = new SqlCommand("Select * from Times where Name = N'" + NamePlayer + "';", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr.GetString(2) == cmbBanCo.Text)
                        {
                            kq_tmp = dr.GetDecimal(1);
                            int a = (int)(kq_tmp) / 60;
                            int b = (int)(kq_tmp) % 60;
                            if (b < 10)
                            {
                                lbl_Giay_KL.Text = "0" + b;
                            }
                            else
                            {
                                lbl_Giay_KL.Text = Convert.ToString(b);
                            }

                            if (a < 10)
                            {
                                lbl_Phut_KL.Text = "0" + a;
                            }
                            else
                            {
                                lbl_Phut_KL.Text = Convert.ToString(a);
                            }

                            flag = 1;
                        }
                    }
                    dr.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }

                if(flag == 0)
                {
                    lbl_Phut_KL.Text = "00";
                    lbl_Giay_KL.Text = "00";
                }

                if (n != 0)
                {
                    soundplayer.Stop();

                    playSound(sound_toplay);

                    game = new PlayerToPlay(this.n);
                    game.Ve_BanCo(panel_BanCo);
                    game.add_actionClick();
                    game.setup(txt_Diem, lblKQ, btnLuu, panel_BanCo, proBarCount);

                    txt_Name.Text = NamePlayer;

                    proBarCount.Maximum = TGcho;
                    cmbTGcho.Enabled = false;

                    timer1.Start();
                    timer2.Start();

                    btnLamMoi.Enabled = true;
                    btnKhoiTao.Enabled = false;
                }
            }
        }

        private void môPhỏngMãĐiTuầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            conn.Close();
            frm_MPmadituan mophong = new frm_MPmadituan();
            mophong.ShowDialog();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            panel_BanCo.Controls.Clear();

            soundplayer.Stop();

            timer1.Stop();

            Stop1 = false;

            playSound(sound_wait);

            btnKhoiTao.Enabled = true;
            btnLuu.Enabled = false;
            panel_BanCo.Enabled = true;
            cmbTGcho.Enabled = true;

            TGcho = 10000;
            txt_Name.Text = "";
            txt_Diem.Text = "";
            lblPhut.Text = "00";
            lblGiay.Text = "00";

            lbl_Phut_KL.Text = "00";
            lbl_Giay_KL.Text = "00";

            lblKQ.Text = "Chưa có kết quả";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Stop1 != true)
            {
                int giay = Convert.ToInt32(lblGiay.Text);
                int phut = Convert.ToInt32(lblPhut.Text);
                giay++;

                if (giay > 59)
                {
                    giay = 0;
                    phut++;
                }

                if (giay < 10)
                {
                    lblGiay.Text = "0" + giay;
                }
                else
                {
                    lblGiay.Text = Convert.ToString(giay);
                }

                if (phut < 10)
                {
                    lblPhut.Text = "0" + phut;
                }
                else
                {
                    lblPhut.Text = Convert.ToString(phut);
                }

                if(phut == 30)
                {
                    timer1.Stop();
                    MessageBox.Show("Đã thua cuộc!!!.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblKQ.Text = "Đã Thua Cuộc!!!";
                }
            }
        }

        private void chơiĐốiKhángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            conn.Close();
            frm_PlayerToPlay game = new frm_PlayerToPlay();
            game.ShowDialog();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int giay = Convert.ToInt32(lblGiay.Text);
            int phut = Convert.ToInt32(lblPhut.Text);

            int kq = phut * 60 + giay;

            int flag = 0;

            decimal kq_tmp = 9999;

            try
            {
                cmd = new SqlCommand("Select * from Times where Name = N'" + NamePlayer + "';", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if(dr.GetString(2) == cmbBanCo.Text)
                    {
                        kq_tmp = dr.GetDecimal(1);
                        flag = 1;
                    }                
                }
                dr.Close();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

            if(flag == 1)
            {
                if(kq_tmp > kq)
                {
                    cmd = new SqlCommand("Update Times set Timer = " + kq + " where Name = N'" + NamePlayer + "' and BanCo = '" + cmbBanCo.Text + "';", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã cập nhật kết quả thành công.!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                cmd = new SqlCommand("Insert into Times values (N'" + NamePlayer + "', " + kq + ", '" + cmbBanCo.Text + "')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã lưu kết quả thành công.!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(game.Player_start == true)
            {
                proBarCount.PerformStep();
                if (proBarCount.Value >= proBarCount.Maximum)
                {
                    Stop1 = true;
                    timer2.Stop();
                    proBarCount.Value = 0;
                    MessageBox.Show("Bạn đã thua cuộc.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblKQ.Text = "Thất bại!!!";
                    btnLuu.Enabled = false;
                    panel_BanCo.Enabled = false;
                }
            }
        }

        private void cmbTGcho_SelectedIndexChanged(object sender, EventArgs e)
        {
            TGcho = Convert.ToInt32(cmbTGcho.Text) * 1000;
        }
    }
}
