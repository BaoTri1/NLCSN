using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace MaDITuan
{
    public class KnightTrip
    {
        private Ban_Co banco;
        private int goal;
        private int x_priorities;
        private int y_priorities;
        private int so_o;
        private int[] row;
        private int[] col;

        const String BOT = "Bot";
        const String BOT_NEXTLOCATION = "Bot_NextLocation";
        const String PLAYER = "Player";

        private int x_priorities_tmp;
        private int y_priorities_tmp;

        public int X_priorities
        {
            get => x_priorities;
            set => x_priorities = value;
        }

        public int Y_priorities
        {
            get => y_priorities;
            set => y_priorities = value;
        }

        public int X_priorities_tmp 
        { 
            get => x_priorities_tmp; 
            set => x_priorities_tmp = value; 
        }

        public int Y_priorities_tmp 
        { 
            get => y_priorities_tmp; 
            set => y_priorities_tmp = value; 
        }
        public int So_o 
        { 
            get => so_o; 
            set => so_o = value; 
        }

        public KnightTrip(int so_oco)
        {
            this.So_o = so_oco;
            this.goal = 0;


            //Tạo mảng các ô cờ
            banco = new Ban_Co(So_o);

            //Tạo mảng các phần tử cộng vào j
            row = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

            //Tạo mảng các phần tử cộng vào i
            col = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };

        }

        public Ban_Co getbanco()
        {
            return this.banco;
        }

        public int getGoal()
        {
            return goal;
        }

        public void setGoal(int g)
        {
             this.goal = g;
        }

        public int[] getMangRow()
        {
            return row;
        }
        public int[] getMangCol()
        {
            return col;
        }

        public void Ve_BanCo(Panel panel)
        {
            int X = 0, Y = 0, KC = 0;

            for (int i = 0; i < banco.getMang().GetLength(0); i++)
            {
                for (int j = 0; j < banco.getMang().GetLength(1); j++)
                {
                    PictureBox tmpPic = banco.getMang()[i, j].getO_Co();
                    {
                        tmpPic.Location = new System.Drawing.Point(X + KC, Y);

                    }
                    panel.Controls.Add(tmpPic);
                    X = tmpPic.Location.X;
                    Y = tmpPic.Location.Y;
                    KC = tmpPic.Width;
                }
                X = 0;
                Y += O_Co.height;
                KC = 0;
            }
        }

        public void Ve_quanco(PictureBox pic, String nameChess)
        {
            if (nameChess == "Bot")
                pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintChess1_Event);
            else if (nameChess == "Bot_NextLocation")
                pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintChess2_Event);
            else if (nameChess == "Player")
                pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintChess3_Event);
        }

    /*    public void madituan(int x, int y)
        {
                        MessageBox.Show("");
            Ve_goal(banco.getMang()[x, y].getO_Co());
            banco.getMang()[x, y].Check = 1;

            if (duyetchienthang())
            {
                return;
            }
            else
            {
                int flag = PriorityMove(x, y);
                if (flag == 1)
                {
                    X_priorities_tmp = x_priorities;
                    Y_priorities_tmp = y_priorities;
                                        MessageBox.Show("");

                    banco.getMang()[X_priorities_tmp, Y_priorities_tmp].getO_Co().Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.Clear_Chess);
                                        MessageBox.Show("");

                    banco.getMang()[X_priorities_tmp, Y_priorities_tmp].getO_Co().Invalidate();
                    Ve_quanco(banco.getMang()[X_priorities_tmp, Y_priorities_tmp].getO_Co(), BOT);

                    PriorityMove(X_priorities_tmp, Y_priorities_tmp);
                                       MessageBox.Show("");

                    banco.getMang()[x_priorities, y_priorities].getO_Co().Invalidate();
                    Ve_quanco(banco.getMang()[x_priorities, y_priorities].getO_Co(), BOT_NEXTLOCATION);

                    madituan(X_priorities_tmp, Y_priorities_tmp);
                }
                else
                {
                    banco.getMang()[x, y].Check = 0;
                                       MessageBox.Show("");

                    banco.getMang()[x, y].getO_Co().Controls.Clear();
                    this.goal--;
                    return;
                }

                int x1 = x_priorities_tmp;
                int y1 = y_priorities_tmp;

                for (int i = 0; i < 8; i++)
                {
                    int x_tmp = x + col[i];
                    int y_tmp = y + row[i];
                    if (x_tmp == x1 && y_tmp == y1)
                        continue;
                    if (x_tmp >= 0 && x_tmp < this.so_o && y_tmp >= 0 && y_tmp < this.so_o && banco.getMang()[x_tmp, y_tmp].Check == 0)
                    {
                        banco.getMang()[x_tmp, y_tmp].getO_Co().Invalidate();
                                               MessageBox.Show("");

                        Ve_quanco(banco.getMang()[x_tmp, y_tmp].getO_Co(), BOT);

                        PriorityMove(x_tmp, y_tmp);
                                               MessageBox.Show("");

                        banco.getMang()[x_priorities, y_priorities].getO_Co().Invalidate();
                        Ve_quanco(banco.getMang()[x_priorities, y_priorities].getO_Co(), BOT_NEXTLOCATION);

                        madituan(x_tmp, y_tmp);
                    }
                }
            }

        } */

        public void Ve_goal(PictureBox pic)
        {
            this.goal++;
            pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.Clear_Chess);
            pic.Invalidate();

            Label lbl = new Label();
            {
                lbl.Text = Convert.ToString(this.goal);
                lbl.Font = new Font("Segoe Script", 20);
                lbl.ForeColor = Color.Red;
                lbl.Width = 80;
                lbl.Height = 80;
                lbl.Location = new Point(2, 17);
            }
            pic.Controls.Add(lbl);
        }

        public void Clear(PictureBox pic)
        {
            pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.Clear_Chess);
        }

        public virtual int CountMove(int x, int y)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                int x_tmp = x + col[i];
                int y_tmp = y + row[i];
                if (x_tmp >= 0 && x_tmp < this.So_o && y_tmp >= 0 && y_tmp < this.So_o && banco.getMang()[x_tmp, y_tmp].Check == 0)
                {
                    count++;
                }
            }
            return count;
        }

        public virtual int PriorityMove(int x, int y)
        {
            int flag = 0;
            int min = 9999;
            for (int i = 0; i < 8; i++)
            {
                int x_tmp = x + col[i];
                int y_tmp = y + row[i];
                if (x_tmp >= 0 && x_tmp < this.So_o && y_tmp >= 0 && y_tmp < this.So_o && banco.getMang()[x_tmp, y_tmp].Check == 0)
                {
                    if (CountMove(x_tmp, y_tmp) < min)
                    {
                        min = CountMove(x_tmp, y_tmp);
                        this.X_priorities = x_tmp;
                        this.Y_priorities = y_tmp;
                        flag = 1;
                    }
                }
            }

            return flag;
        }

        public int PriorityMove_Seconds(int x, int y, int x_lui, int y_lui)
        {
            int flag = 0;
            int min = 9999;
            for (int i = 0; i < 8; i++)
            {
                int x_tmp = x + col[i];
                int y_tmp = y + row[i];
                if (x_tmp >= 0 && x_tmp < this.So_o && y_tmp >= 0 && y_tmp < this.So_o && banco.getMang()[x_tmp, y_tmp].Check == 0)
                {
                    if((x_tmp == x_lui && y_tmp != y_lui) || (x_tmp != x_lui && y_tmp == y_lui) || (x_tmp != x_lui && y_tmp != y_lui))
                    {
                        if (CountMove(x_tmp, y_tmp) < min)
                        {
                            min = CountMove(x_tmp, y_tmp);
                            this.X_priorities = x_tmp;
                            this.Y_priorities = y_tmp;
                            flag = 1;
                        }
                    }

                }
            }

            return flag;
        }

        public virtual Boolean duyetchienthang()
        {
            if (this.goal == this.So_o * this.So_o)
                return true;
            else return false;
        }
    }
}