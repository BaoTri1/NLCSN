using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaDITuan
{
    public class FightingGame : KnightTrip
    {
        private int x_player, y_player;
        private int goal_user;

        private int[] col_player;
        private int[] row_player;

        private Boolean player_start;

        const String PLAYER = "Player";

        private TextBox txt_goal;
        private Label lblKQ;

        public int X_player { get => X_player1; set => X_player1 = value; }
        public int Y_player { get => Y_player1; set => Y_player1 = value; }
 
        public int X_player1 { get => x_player; set => x_player = value; }
        public int Y_player1 { get => y_player; set => y_player = value; }

        public FightingGame(int so_oco) : base(so_oco)
        {
            this.goal_user = 0;
            this.player_start = false;
            this.col_player = new int[8];
            this.row_player = new int[8];

            for(int i = 0; i < 8; i++)
            {
                this.col_player[i] = -1;
                this.row_player[i] = -1;
            }
        }

        public void setup(TextBox txt_goal, Label lblKQ )
        {
            this.txt_goal = txt_goal;
            this.lblKQ = lblKQ;
        }

        public new void Ve_BanCo(Panel panel)
        {
            base.Ve_BanCo(panel);
        }

        public void Ve_Goal(PictureBox pic)
        {
            pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintGoal_XDo_Event);
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
                base.getbanco().getMang()[X_player, Y_player].Check = 1;

                this.goal_user++;

                this.txt_goal.Text = Convert.ToString(this.goal_user);

                //Lấy các ô tiếp theo hợp lệ
                Tao_cacO_TiepTheo(this.X_player, this.Y_player);
            }
            else
            {
                PictureBox tmp = sender as PictureBox;

                base.getbanco().get_O_CO(tmp);

                int x_tmp = base.getbanco().getX();
                int y_tmp = base.getbanco().getY();

                if (base.getbanco().getMang()[x_tmp, y_tmp].Lock1 == true)
                {
                    MessageBox.Show("Ô đang đứng");
                }
                else
                {
                    if (Duyet_OCo(x_tmp, y_tmp) == false)
                    {
                        MessageBox.Show("Ô không hợp lệ!!!.", "Chọn đường đi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {    
                        if(base.getbanco().getMang()[x_tmp, y_tmp].Check == 1)
                        {
                            this.goal_user--;

                            //ghi kết quả lên Giao diện
                            this.txt_goal.Invalidate();

                            this.txt_goal.Text = Convert.ToString(this.goal_user);

                            //Xóa đồ họa của ô cờ đang click vào
                            base.Clear(base.getbanco().getMang()[x_tmp, y_tmp].getO_Co());

                            //đánh dấu ô cờ mà quân mã đàn đứng ko thuộc về người chơi.
                            base.getbanco().getMang()[this.X_player, this.Y_player].Check = 0;

                            //Xóa đồ họa ở ô cờ mà quân mã đang đứng
                            base.Clear(base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co());

                            //Yêu cầu vẽ lại ô cờ
                            base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co().Invalidate();

                            //Mở khóa ô cờ thể hiện quân mã ko còn đứng ở đó nữa
                            base.getbanco().getMang()[this.X_player, this.Y_player].Lock1 = false;

                            //Yêu cầu vẽ lại ô cờ
                            tmp.Invalidate();

                            //Vẽ quân mã lên ô Click
                            base.Ve_quanco(tmp, PLAYER);

                            //Lấy tạo độ x, y của ô đang click vào
                            this.X_player = x_tmp;
                            this.Y_player = y_tmp;

                            //khóa ô cờ lại
                            base.getbanco().getMang()[X_player, Y_player].Lock1 = true;

                            //Đánh dấu ô thuộc về người chơi Check = 1
                            base.getbanco().getMang()[X_player, Y_player].Check = 1;

                            resetMang();

                            Tao_cacO_TiepTheo(x_tmp, y_tmp);
                        }
                        else
                        {
                            base.Clear(base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co());

                            base.getbanco().getMang()[this.X_player, this.Y_player].Lock1 = false;

                            base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co().Invalidate();

                            Ve_Goal(base.getbanco().getMang()[this.X_player, this.Y_player].getO_Co());

                            this.goal_user++;
                            this.txt_goal.Invalidate();
                            this.txt_goal.Text = Convert.ToString(this.goal_user);

                            if (this.goal_user == base.So_o * base.So_o)
                            {
                                frm_FightingGame.Stop1 = false;
                                MessageBox.Show("Chúc mừng bạn đã chiến thắng.!!!", "Thông Báo Chiến Thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tmp.Invalidate();
                                base.Ve_quanco(tmp, PLAYER);
                                this.lblKQ.Text = "Đã chiến Thằng !!!";
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

                                //Đánh dấu ô thuộc về người chơi Check = 1
                                base.getbanco().getMang()[X_player, Y_player].Check = 1;

                                resetMang();

                                Tao_cacO_TiepTheo(x_tmp, y_tmp);
                            }
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

        public void resetMang()
        {
            for(int i = 0; i < 8; i++)
            {
                this.col_player[i] = -1;
                this.row_player[i] = -1;
            }
        }
    }
}
