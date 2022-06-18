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

        private int[] toadoX;
        private int[] toadoY;

        private int x, y, n, speed = 0, count = 0;

        private int x_lui, y_lui;

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
            if(txtnxn.Text == "" || txt_td_i.Text == "" || txt_td_j.Text == "")
            {
                MessageBox.Show("Các trường nhập không được bỏ trống!!!.", "Thông Báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                if(txtnxn.Text == "")
                {
                    txtnxn.Focus();
                }
                else if(txt_td_i.Text == "")
                {
                    txt_td_i.Focus();
                }
                else
                {
                    txt_td_j.Focus();
                }
            }
            else
            {
                try
                {
                    this.n = Convert.ToInt32(txtnxn.Text);

                    if(n >= 4 && n <= 10)
                    {
                        
                        try
                        {
                            this.x = Convert.ToInt32(txt_td_i.Text);
                            this.y = Convert.ToInt32(txt_td_j.Text);

                            if(x >= 0 && x < n && y >= 0 && y < n)
                            {
                                
                                knighttrip = new KnightTrip(n);

                                //Vẽ bàn cờ nxn
                                knighttrip.Ve_BanCo(panel_BanCo);

                                //Vẽ quân mã tại vị trí (x, y)
                                knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[x, y].getO_Co(), BOT);

                                //Xet bước đi ưu tiên của (x, y)
                                knighttrip.PriorityMove(x, y);

                                //Vẽ quân mã tại vị trí ưu tiên
                                knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co(), BOT_NEXTLOCATION);

                                btnBatDau.Enabled = true;
                                btnLamMoi.Enabled = true;
                                btnKhoiTao.Enabled = false;

                                playSound(sound_readyGo);

                                this.toadoX = new int[this.n * this.n];
                                this.toadoY = new int[this.n * this.n];

                                for(int i = 0; i < this.n * this.n; i++)
                                {
                                    this.toadoX[i] = -1;
                                    this.toadoY[i] = -1;
                                }

                                lbl_Text_BanCo.Text = Convert.ToString(this.n) + " x " + Convert.ToString(this.n);
                                lbl_Text_TongSoO.Text = Convert.ToString(this.n * this.n);
                                lbl_Text_ToaDoI.Text = txt_td_i.Text;
                                lbl_Text_ToaDoJ.Text = txt_td_j.Text;


                            }
                            else
                            {
                                MessageBox.Show("tọa độ i hoặc j không hợp lệ !!!. 0 <= i, j < ." + txtnxn.Text, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("tọa độ i hoặc j không hợp lệ !!!. i và j phải là số nguyên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
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
            if (knighttrip.duyetchienthang())
            {
                timer1.Enabled = false;
                DialogResult result = MessageBox.Show("Đã hoàn thành hành trình đi tuần của quân MÃ. ", "Chúc mừng Bạn!!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(result == DialogResult.OK)
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
                //Vẽ kết quả số bước đi lên ô đã đi qua
                knighttrip.Ve_goal(knighttrip.getbanco().getMang()[x, y].getO_Co());

                this.toadoX[this.count] = x;
                this.toadoY[this.count] = y;
                this.count++;

                //Đánh dấu ô đã đi qua rồi
                knighttrip.getbanco().getMang()[x, y].Check = 1;

                //Lấy điểm ưu tiên tiếp theo của (x, y)
                if (knighttrip.PriorityMove(x, y) == 0 && knighttrip.getGoal() < this.n * this.n)
                {
                    timer1.Enabled = false;
                    DialogResult result = MessageBox.Show("Đã hết đường tiến hành quay lui. ", "Đã hết đường đi!!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        timer2.Enabled = true;

                    }
                }
                else
                {
                    this.x_lui = this.x;
                    this.y_lui = this.y;

                    this.x = knighttrip.X_priorities;
                    this.y = knighttrip.Y_priorities;

                    //MessageBox.Show(Convert.ToString(x) + Convert.ToString(y));

                    if (knighttrip.getGoal() < n * n - 1)
                    {
                        //Xóa quân cờ ở điểm ưu tiên
                        knighttrip.Clear(knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co());

                        //Gọi Invalidate(); để vẽ lại ô cờ
                        knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co().Invalidate();

                        //Vẽ quân cờ lên điểm ưu tiên thể hiển quân cờ đã đi đến điểm ưu tiên;
                        knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co(), BOT);

                        //Lấy điểm ưu tiên tiếp theo dựa trên điểm ưu tiên ban đầu
                        if(knighttrip.PriorityMove(knighttrip.X_priorities, knighttrip.Y_priorities) == 1 && knighttrip.getGoal() < this.n * this.n)
                        {
                            knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co().Invalidate();

                            //Vẽ quân cờ thể hiện quân mã sẽ được ưu tiên đi cho bước tiếp theo
                            knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co(), BOT_NEXTLOCATION);
                        }
                    }
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
//            knighttrip.getbanco().getMang()[this.x, this.y].getO_Co().Controls.Clear();

            knighttrip.getbanco().getMang()[this.x, this.y].Check = 0;

            this.toadoX[count] = -1;
            this.toadoY[count] = -1;

            this.count--;

            Console.WriteLine(x_lui);
            Console.WriteLine(y_lui);

//            knighttrip.setGoal(knighttrip.getGoal() - 1);

            if(knighttrip.PriorityMove_Seconds(this.x_lui, this.y_lui, this.x, this.y) == 0 || knighttrip.getGoal() == (this.n * this.n - 2))
            {
                timer2.Enabled = false;
                DialogResult result = MessageBox.Show("Không tìm được hành trình quân Mã. ", "Chia Buồn với bạn!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                knighttrip.getbanco().getMang()[this.x, this.y].getO_Co().Controls.Clear();

                this.x = knighttrip.X_priorities;
                this.y = knighttrip.Y_priorities;


                knighttrip.setGoal(knighttrip.getGoal() - 1);

                knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co(), BOT);

                knighttrip.PriorityMove(knighttrip.X_priorities, knighttrip.Y_priorities);

                knighttrip.Ve_quanco(knighttrip.getbanco().getMang()[knighttrip.X_priorities, knighttrip.Y_priorities].getO_Co(), BOT_NEXTLOCATION); 

                timer2.Enabled = false;

                DialogResult result = MessageBox.Show("Đã chọn ra vi trí quay lui.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    timer1.Enabled = true;
                }

                Console.WriteLine(x);
                Console.WriteLine(y);
            }

        }

        private void chơiĐốiKhángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_FightingGame game = new frm_FightingGame();
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
                    streamWriter.WriteLine("Bước " + (i + 1) + ": (" + this.toadoX[i] + ", " + this.toadoY[i] + "). ");
                streamWriter.WriteLine("Kết quả: " + lbl_Text_Kq.Text);
                streamWriter.WriteLine("");
                streamWriter.WriteLine("Ghi chú: (-1, -1) là tọa độ ko tồn tại.");
                streamWriter.Close();
                fileStream.Close();
            }
        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            panel_BanCo.Controls.Clear();
            txtnxn.Text = "";
            txt_td_i.Text = "";
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
                if(this.toadoX[i] != -1 && this.toadoY[i] != -1)
                {
                    Label lbl = new Label();
                    {
                        lbl.Text = "Buoc " + (i + 1) + ": (" + this.toadoX[i] + ", " + this.toadoY[i] + "), ";
                        Font font = new Font("Microsoft Sans Serif", 9);
                        lbl.Font = font;
                        lbl.ForeColor = Color.Blue;
                    }

                    panel_cactoado.Controls.Add(lbl);
                }
            }
        }
    }
}
