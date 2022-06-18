using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace MaDITuan
{
    public class FightingGame : KnightTrip
    {
        private int x_player, y_player, x_bot, y_bot;
        private int goal_user;
        private int goal_bot;
        private int back_user = 5;
        private int back_bot = 5;

        private int[] col_player;
        private int[] row_player;

        private Boolean player_start;
        private Boolean Bot_start;
        private String KQ;

        const String BOT = "Bot";
        const String PLAYER = "Player";


        private Label lbl_goal_May;
        private Label lbl_goal_Player;
        private TextBox txt_Name;
        private TextBox txt_goal;
        private TextBox txt_quaylui;
        private ProgressBar proBarCoolDown;
        private PictureBox pic;
        private Panel panel;
        private Label lblKQ;

        public string KQ1
        {
            get => KQ;
            set => KQ = value;
        }

        public int getGoal_user()
        {
            return this.goal_user;
        }

        public int getGoal_bot()
        {
            return this.goal_bot;
        }

        public int getBack_user()
        {
            return this.back_user;
        }

        public int getBack_bot()
        {
            return this.back_bot;
        }

        public int X_player { get => X_player1; set => X_player1 = value; }
        public int Y_player { get => Y_player1; set => Y_player1 = value; }
        public int X_bot { get => X_bot1; set => X_bot1 = value; }
        public int Y_bot { get => Y_bot1; set => Y_bot1 = value; }
        public int X_player1 { get => x_player; set => x_player = value; }
        public int Y_player1 { get => y_player; set => y_player = value; }
        public int X_bot1 { get => x_bot; set => x_bot = value; }
        public int Y_bot1 { get => y_bot; set => y_bot = value; }

        public FightingGame(int so_oco) : base(so_oco)
        {
            this.goal_user = 0;
            this.goal_bot = 0;
            this.player_start = false;
            this.Bot_start = false;
            this.col_player = new int[8];
            this.row_player = new int[8];

            for(int i = 0; i < 8; i++)
            {
                this.col_player[i] = -1;
                this.row_player[i] = -1;
            }
        }

        public void setup(Label lbl_goal_May, Label lbl_goal_Player, TextBox txt_Name, TextBox txt_goal, TextBox txt_quaylui, ProgressBar proBarCoolDown, PictureBox pic, Panel panel, Label lblKQ)
        {
            this.lbl_goal_May = lbl_goal_May;
            this.lbl_goal_Player = lbl_goal_Player;
            this.txt_goal = txt_goal;
            this.txt_quaylui = txt_quaylui;
            this.txt_Name = txt_Name;
            this.proBarCoolDown = proBarCoolDown;
            this.pic = pic;
            this.panel = panel;
            this.lblKQ = lblKQ;
        }

        public new void Ve_BanCo(Panel panel)
        {
            base.Ve_BanCo(panel);
        }

        public void Ve_Goal(PictureBox pic, String nameChess)
        {


            if (nameChess == "Bot")
            {
                pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintGoal_XDo_Event);
            }
            else if (nameChess == "Player")
            {
                pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintGoal_XXanh_Event);
            }
        }

        public void action_click(Object sender, EventArgs e)
        {
            if (this.player_start == false)
            {
                this.player_start = true;

                PictureBox tmp = sender as PictureBox;

                tmp.Invalidate();

                //Vẽ quân mã của Player
                base.Ve_quanco(tmp, PLAYER);

                //Lấy tọa độ của ô cờ trong mảng
                base.getbanco().get_O_CO(tmp);
                this.X_player = base.getbanco().getX();
                this.Y_player = base.getbanco().getY();

                //khóa ô cờ lại
                base.getbanco().getMang()[X_player, Y_player].Lock1 = true;

                //Đánh dấu ô thuộc về người chơi Check = 2
                base.getbanco().getMang()[X_player, Y_player].Check = 2;

                this.goal_user++;

                this.lbl_goal_Player.Text = "x" + Convert.ToString(this.goal_user);

                //Lấy các ô tiếp theo hợp lệ
                Tao_cacO_TiepTheo(this.X_player, this.Y_player);

                if(this.Bot_start == false)
                {
                    Random rd = new Random();
                    int x = rd.Next(0, base.So_o);
                    int y = rd.Next(0, base.So_o);

                    if (x == this.X_player && y == this.Y_player)
                    {
                        x = rd.Next(0, base.So_o);
                        y = rd.Next(0, base.So_o);
                    }

                    this.X_bot = x;
                    this.Y_bot = y;

                    this.proBarCoolDown.Value = 0;

                    this.pic.Invalidate();
                    this.pic.Image = new Bitmap(Application.StartupPath + "\\Img\\ma_do.png");
                    this.txt_Name.Text = "Knight Dark";
                    this.txt_goal.Text = Convert.ToString(this.goal_bot);
                    this.txt_quaylui.Text = Convert.ToString(this.back_bot);

                    this.panel.Enabled = false;

                    frm_FightingGame.Action = true;


                }

            }
            else
            {
                PictureBox tmp = sender as PictureBox;

                base.getbanco().get_O_CO(tmp);

                int x_tmp = base.getbanco().getX();
                int y_tmp = base.getbanco().getY();

                if (CountMove(x_tmp, y_tmp, PLAYER) == 0 && this.back_user == 0)
                {
                    MessageBox.Show("Chia buồn bạn thua rồi. Máy đã thắng!!!.", "Kết quả ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setupKQ(BOT);
                    this.panel.Enabled = false;
                    return;
                }

                if (base.getbanco().getMang()[x_tmp, y_tmp].Lock1 == true)
                {
                    MessageBox.Show("Ô đã khóa");
                    this.proBarCoolDown.Value = 0;

                    frm_FightingGame.Action = true;

                    this.pic.Invalidate();
                    this.pic.Image = new Bitmap(Application.StartupPath + "\\Img\\ma_do.png");
                    this.txt_Name.Text = "Knight Dark";
                    this.txt_goal.Text = Convert.ToString(this.goal_bot);
                    this.txt_quaylui.Text = Convert.ToString(this.back_bot);

                    this.panel.Enabled = false;
                }
                else
                {
                    if (Duyet_OCo(x_tmp, y_tmp) == false)
                    {
                        MessageBox.Show("Ô không hợp lệ!!!.", "Chọn đường đi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {

                        if (base.getbanco().getMang()[x_tmp, y_tmp].Check == 1)
                        {
                            this.goal_bot--;
                            this.lbl_goal_May.Text = "x" + Convert.ToString(this.goal_bot);

                            base.Clear(base.getbanco().getMang()[x_tmp, y_tmp].getO_Co());
                        }
                        else if (base.getbanco().getMang()[x_tmp, y_tmp].Check == 2)
                        {
                            if (this.back_user != 0)
                            {
                                //MessageBox.Show("Bạn có muốn quay lui?");
                                this.goal_user--;
                                this.back_user--;
                                this.lbl_goal_Player.Text = "x" + Convert.ToString(this.goal_user);
                                base.Clear(base.getbanco().getMang()[x_tmp, y_tmp].getO_Co());
                            }
                            else
                            {
                                MessageBox.Show("Bạn không còn lượt quay lui nữa!!!. ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        base.Clear(base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co());

                        base.getbanco().getMang()[this.X_player, this.Y_player].Lock1 = false;

                        base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co().Invalidate();

                        Ve_Goal(base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co(), PLAYER);

                        this.goal_user++;

                        this.lbl_goal_Player.Text = "x" + Convert.ToString(this.goal_user);

                        if (this.goal_user + this.goal_bot == base.So_o * base.So_o)
                        {
                            DuyetChienThang();
                            this.panel.Enabled = false;
                            return;
                        }
                        else if (this.goal_user > (base.So_o * base.So_o) / 2 || this.goal_bot > (base.So_o * base.So_o) / 2)
                        {
                            DuyetChienThang();
                            this.panel.Enabled = false;
                            return;
                        }
                        else
                        {
                            tmp.Invalidate();

                            //Vẽ quân mã của Player
                            base.Ve_quanco(tmp, PLAYER);

                            this.X_player = x_tmp;
                            this.Y_player = y_tmp;

                            //khóa ô cờ lại
                            base.getbanco().getMang()[X_player, Y_player].Lock1 = true;

                            //Đánh dấu ô thuộc về người chơi Check = 2
                            base.getbanco().getMang()[X_player, Y_player].Check = 2;

                            Tao_cacO_TiepTheo(x_tmp, y_tmp);

                            this.proBarCoolDown.Value = 0;

                            frm_FightingGame.Action = true;

                            this.pic.Invalidate();
                            this.pic.Image = new Bitmap(Application.StartupPath + "\\Img\\ma_do.png");
                            this.txt_Name.Text = "Knight Dark";
                            this.txt_goal.Text = Convert.ToString(this.goal_bot);
                            this.txt_quaylui.Text = Convert.ToString(this.back_bot);

                            this.panel.Enabled = false;
                        }
                    }
                }
            }
        }

        public void add_actionClick()
        {
            for (int i = 0; i < base.getbanco().getMang().GetLength(0); i++)
            {
                for (int j = 0; j < base.getbanco().getMang().GetLength(1); j++)
                {
                    base.getbanco().getMang()[i, j].getO_Co().Click += action_click;
                }
            }
        }

        public void Action_Bot(int x, int y)
        {
            if (this.Bot_start == false)
            {
                this.Bot_start = true;

                base.getbanco().getMang()[x, y].getO_Co().Invalidate();

                //Vẽ quân cờ BOT
                base.Ve_quanco(base.getbanco().getMang()[x, y].getO_Co(), BOT);

                //Khóa ô cờ
                base.getbanco().getMang()[x, y].Lock1 = true;

                //Đánh dấu ô thuộc về người chơi Check = 1
                base.getbanco().getMang()[x, y].Check = 1;

                this.goal_bot++;

                this.lbl_goal_May.Text = "x" + Convert.ToString(this.goal_bot);

                base.PriorityMove(x, y);

                this.X_bot = x;
                this.Y_bot = y;

                this.pic.Invalidate();
                this.pic.Image = new Bitmap(Application.StartupPath + "\\Img\\ma_xanh.png");
                this.txt_Name.Text = frm_FightingGame.NamePlayer;
                this.txt_goal.Text = Convert.ToString(this.goal_user);
                this.txt_quaylui.Text = Convert.ToString(this.back_user);

                this.panel.Enabled = true;

            }
            else
            {

                PriorityMove(this.X_bot, this.Y_bot);

                if (base.getbanco().getMang()[base.X_priorities, base.Y_priorities].Lock1 == true)
                {
                    MessageBox.Show("da khóa");
                    return;
                }
                else
                {

                    base.Clear(base.getbanco().getMang()[this.X_bot, this.Y_bot].getO_Co());

                    base.getbanco().getMang()[this.X_bot, this.Y_bot].getO_Co().Invalidate();

                    Ve_Goal(base.getbanco().getMang()[this.X_bot, this.Y_bot].getO_Co(), BOT);

                    base.getbanco().getMang()[this.X_bot, this.Y_bot].Lock1 = false;

                    this.goal_bot++;

                    this.lbl_goal_May.Text = "x" + Convert.ToString(this.goal_bot);

                    if (this.goal_user + this.goal_bot == base.So_o * base.So_o)
                    {
                        DuyetChienThang();
                        this.panel.Enabled = false;
                        return;
                    }
                    else if (this.goal_user > (base.So_o * base.So_o) / 2 || this.goal_bot > (base.So_o * base.So_o) / 2)
                    {
                        DuyetChienThang();
                        this.panel.Enabled = false;
                        return;
                    }
                    else
                    {
                        // Chọn bước đi tiếp theo
                        //PriorityMove(this.X_bot, this.Y_bot);

                        if (base.getbanco().getMang()[base.X_priorities, base.Y_priorities].Check == 2)
                        {
                            this.goal_user--;
                            this.lbl_goal_Player.Text = "x" + Convert.ToString(this.goal_user);

                            base.Clear(base.getbanco().getMang()[base.X_priorities, base.Y_priorities].getO_Co());
                        }
                        else if (base.getbanco().getMang()[base.X_priorities, base.Y_priorities].Check == 1)
                        {
                            if (this.back_bot != 0)
                            {
                                //MessageBox.Show("Bạn có muốn quay lui?");
                                this.goal_bot--;
                                this.back_bot--;
                                this.lbl_goal_May.Text = "x" + Convert.ToString(this.goal_bot);
                                base.Clear(base.getbanco().getMang()[base.X_priorities, base.Y_priorities].getO_Co());
                            }
                            else
                            {
                                MessageBox.Show("Máy không còn lượt quay lui nữa!!!. ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (CountMove(base.X_priorities, base.Y_priorities, BOT) == 0)
                                {
                                    MessageBox.Show("Chúc mừng!!!. Bạn thắng rồi.", "Kết quả ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.panel.Enabled = false;
                                    setupKQ(PLAYER);
                                }

                                return;
                            }

                        }

                        base.getbanco().getMang()[base.X_priorities, base.Y_priorities].getO_Co().Invalidate();

                        base.Ve_quanco(base.getbanco().getMang()[base.X_priorities, base.Y_priorities].getO_Co(), BOT);

                        base.getbanco().getMang()[base.X_priorities, base.Y_priorities].Lock1 = true;

                        //Đánh dấu ô thuộc về Máy Check = 1
                        base.getbanco().getMang()[base.X_priorities, base.Y_priorities].Check = 1;

                        this.X_bot = base.X_priorities;
                        this.Y_bot = base.Y_priorities;

                        this.panel.Enabled = true;
                    }
                }

            }
        }

        public void DuyetChienThang()
        {
            if (this.goal_bot > this.goal_user)
            {
                MessageBox.Show("Chia buồn bạn thua rồi. Máy đã thắng!!!.", "Kết quả ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setupKQ(BOT);
            }
            else if (this.goal_bot < this.goal_user)
            {
                MessageBox.Show("Chúc mừng!!!. Bạn thắng rồi.", "Kết quả ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setupKQ(PLAYER);
            }
            else
            {
                MessageBox.Show("Bạn và Máy đã hòa nhau!!!.", "Kết quả ván đấu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setupKQ("");
            }
        }

        public override int PriorityMove(int x, int y)
        {
            int flag = 0;
            int min = 9999;
            for (int i = 0; i < 8; i++)
            {
                int x_tmp = x + base.getMangCol()[i];
                int y_tmp = y + base.getMangRow()[i];
                if (x_tmp >= 0 && x_tmp < this.So_o && y_tmp >= 0 && y_tmp < this.So_o && (base.getbanco().getMang()[x_tmp, y_tmp].Check == 0 || base.getbanco().getMang()[x_tmp, y_tmp].Check == 2))
                {
                    if (CountMove(x_tmp, y_tmp, BOT) < min)
                    {
                        min = CountMove(x_tmp, y_tmp, BOT);
                        this.X_priorities = x_tmp;
                        this.Y_priorities = y_tmp;
                        flag = 1;
                    }
                }
            }

            return flag;
        }

        public int CountMove(int x, int y, String str)
        {
            int count = 0;

            if(str == BOT)
            {
                for (int i = 0; i < 8; i++)
                {
                    int x_tmp = x + base.getMangCol()[i];
                    int y_tmp = y + base.getMangRow()[i];
                    if (x_tmp >= 0 && x_tmp < this.So_o && y_tmp >= 0 && y_tmp < this.So_o && (base.getbanco().getMang()[x_tmp, y_tmp].Check == 0 || base.getbanco().getMang()[x_tmp, y_tmp].Check == 2))
                    {
                        count++;
                    }
                }
            }
            else if(str == PLAYER)
            {
                for (int i = 0; i < 8; i++)
                {
                    int x_tmp = x + base.getMangCol()[i];
                    int y_tmp = y + base.getMangRow()[i];
                    if (x_tmp >= 0 && x_tmp < this.So_o && y_tmp >= 0 && y_tmp < this.So_o && (base.getbanco().getMang()[x_tmp, y_tmp].Check == 0 || base.getbanco().getMang()[x_tmp, y_tmp].Check == 1))
                    {
                        count++;
                    }
                }
            }

            return count;

        }

        public void Tao_cacO_TiepTheo(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                int x_tmp = x + base.getMangCol()[i];
                int y_tmp = y + base.getMangRow()[i];
                if (x_tmp >= 0 && x_tmp < this.So_o && y_tmp >= 0 && y_tmp < this.So_o)
                {
                    this.col_player[i] = x_tmp;
                    this.row_player[i] = y_tmp;
                }
            }
        }

        public Boolean Duyet_OCo(int x, int y)
        {
            for(int i = 0; i < 8; i++)
            {
                if(x == this.col_player[i] && y == this.row_player[i])
                {
                    return true;
                }
            }

            return false;
        }

        public void setupKQ(String str)
        {
            if (str == BOT)
            {
                this.lblKQ.Text = "Máy Đã Chiến Thắng!!!";
                this.lblKQ.ForeColor = Color.DeepPink;
            }
            else if(str == PLAYER)
            {
                this.lblKQ.Text = this.txt_Name.Text + " Đã Chiến Thắng!!!";
                this.lblKQ.ForeColor = Color.MediumBlue;
            }
            else
            {
                this.lblKQ.Text = "Hòa nhau";
            }
        }
    }
}
