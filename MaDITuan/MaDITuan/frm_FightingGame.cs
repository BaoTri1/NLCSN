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
    public partial class frm_FightingGame : Form
    {
        private System.Media.SoundPlayer soundplayer;
        private String sound_wait = Application.StartupPath + "\\Music\\action-110116.wav";
        private String sound_toplay = Application.StartupPath + "\\Music\\let-the-games-begin-21858.wav";
        private String sound_readyGo = Application.StartupPath + "\\Music\\y2mate.com-Ready-Go-Memes.wav";

        const String BOT = "Bot";
        const String PLAYER = "Player";

        private FightingGame game;

        private static Boolean action;

        private int n;

        private static String namePlayer;

        public static bool Action 
        { 
            get => action; 
            set => action = value; 
        }

        public static string NamePlayer { 
            get => namePlayer; 
            set => namePlayer = value; 
        }

        public frm_FightingGame()
        {
            InitializeComponent();
            action = false;
        }

        private void frm_FightingGame_Load(object sender, EventArgs e)
        {
            playSound(sound_wait);

            btnLamMoi.Enabled = false;

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
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            if(cmbBanCo.Text == "")
            {
                DialogResult = MessageBox.Show("Bạn phải chọn bàn cờ để bắt đầu tạo ván chơi.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(cmbBanCo.Text == "8 x 8")
            {
                soundplayer.Stop();

                playSound(sound_toplay);

                this.n = 8;
                game = new FightingGame(this.n);
                game.Ve_BanCo(panel_BanCo);
                game.add_actionClick();
                game.setup(lbl_goal_May, lbl_goal_Player, txt_Name, txt_Diem, txt_QuayLui, proBarCoolDown, picHoanDoi, panel_BanCo, lblKQ);

                if (txtName.Text == "")
                {
                    NamePlayer = "Player";
                }
                else
                {
                    NamePlayer = txtName.Text;
                }

                DialogResult r = MessageBox.Show("Bạn có muốn đi trước không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(r == DialogResult.No)
                {
                    action = true;
                    picHoanDoi.Invalidate();
                    picHoanDoi.Image = new Bitmap(Application.StartupPath + "\\Img\\ma_do.png");
                    txt_Name.Text = "Knight Dart";
                    txt_Diem.Text = Convert.ToString(game.getGoal_bot());
                    txt_QuayLui.Text = Convert.ToString(game.getBack_bot());
                    proBarCoolDown.Value = 0;
                    panel_BanCo.Enabled = true;

                }
                else
                {
                    picHoanDoi.Invalidate();
                    picHoanDoi.Image = new Bitmap(Application.StartupPath + "\\Img\\ma_xanh.png");
                    txt_Name.Text = NamePlayer;
                    txt_Diem.Text = Convert.ToString(game.getGoal_user());
                    txt_QuayLui.Text = Convert.ToString(game.getBack_user());
                    proBarCoolDown.Value = 0;
                }

                timer1.Start();

                timer2.Start();

                btnLamMoi.Enabled = true;
                btnKhoiTao.Enabled = false;

            }

        }

        private void môPhỏngMãĐiTuầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_MPmadituan mophong = new frm_MPmadituan();
            mophong.ShowDialog();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            panel_BanCo.Controls.Clear();

            soundplayer.Stop();

            timer1.Stop();
            timer2.Stop();

            playSound(sound_wait);

            btnKhoiTao.Enabled = true;
            txtName.Text = "";
            txt_Name.Text = "";
            txt_Diem.Text = "";
            txt_QuayLui.Text = "";
            cmbBanCo.Text = "";

            picHoanDoi.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.Clear_Chess);
            picHoanDoi.Invalidate();

            lblKQ.Text = "Chưa có kết quả";
            lblKQ.ForeColor = Color.Black;

            lbl_goal_May.Text = "x0";
            lbl_goal_Player.Text = "x0";

            proBarCoolDown.Value = 0;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            proBarCoolDown.PerformStep();

            if(game.getGoal_user() + game.getGoal_bot() == this.n * this.n)
            {
                timer1.Stop();
                timer2.Stop();
                proBarCoolDown.Value = 0;
                panel_BanCo.Enabled = false;
            }
            else if(game.getGoal_user() > (this.n * this.n)/2 || game.getGoal_bot() > (this.n * this.n) / 2)
            {
                timer1.Stop();
                timer2.Stop();
                proBarCoolDown.Value = 0;
                panel_BanCo.Enabled = false;
            }

            if(proBarCoolDown.Value >= proBarCoolDown.Maximum)
            {
                action = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(action == true)
            {
                game.Action_Bot(game.X_bot, game.Y_bot);
                picHoanDoi.Invalidate();
                picHoanDoi.Image = new Bitmap(Application.StartupPath + "\\Img\\ma_xanh.png");
                txt_Name.Text = NamePlayer;
                txt_Diem.Text = Convert.ToString(game.getGoal_user());
                txt_QuayLui.Text = Convert.ToString(game.getBack_user());
                proBarCoolDown.Value = 0;
                if(game.getGoal_user() + game.getGoal_bot() == this.n * this.n)
                {
                    panel_BanCo.Enabled = false;
                }
                else if (game.getGoal_user() > (this.n * this.n) / 2 || game.getGoal_bot() > (this.n * this.n) / 2)
                {
                    panel_BanCo.Enabled = false;
                }
                else
                {
                    panel_BanCo.Enabled = true;
                }
                
                action = false;
            }
        }

        private void chơiĐốiKhángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_FightingGame game = new frm_FightingGame();
            game.ShowDialog();
        }
    }
}
