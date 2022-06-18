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
    public partial class frm_Menu : Form
    {
        private System.Media.SoundPlayer player;

        public frm_Menu()
        {
            InitializeComponent();
        }

        private void frm_Menu_Load(object sender, EventArgs e)
        {
            player = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\cutesong-21289.wav"); ;
            player.PlayLooping();
        }

        private void btnMoPhong_Click(object sender, EventArgs e)
        {
            player.Stop();
            frm_MPmadituan mophong = new frm_MPmadituan();
            mophong.ShowDialog();
            player.PlayLooping();
        }

        private void btnDoiKhang_Click(object sender, EventArgs e)
        {
            player.Stop();
            frm_FightingGame game = new frm_FightingGame();
            game.ShowDialog();
            player.PlayLooping();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
