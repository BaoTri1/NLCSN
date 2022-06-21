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

        private int[] STT_hang = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private String[] STT_cot = new string[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        private int[] dd_X;
        private int[] dd_Y;
        private Boolean win;

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

        public int So_o 
        { 
            get => so_o; 
            set => so_o = value; 
        }

        public KnightTrip(int so_oco)
        {
            this.So_o = so_oco;

            this.goal = 1;

            dd_X = new int[200];

            dd_Y = new int[200];

            for(int i = 0; i < 200; i++)
            {
                dd_X[i] = -1;
                dd_Y[i] = -1;
            }

            //Tạo mảng các ô cờ
            banco = new Ban_Co(So_o);

            //Tạo mảng các phần tử cộng vào j
            row = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

            //Tạo mảng các phần tử cộng vào i
            col = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };

        }

        public int[] getDD_X()
        {
            return dd_X;
        }

        public int[] getDD_Y()
        {
            return dd_Y;
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
            int x_lbl = 70, y_lbl = 0, kc_lbl = 0;

            for(int i = 0; i < banco.getMang().GetLength(1); i++)
            {
                Label lbl = new Label();
                {
                    lbl.Text = Convert.ToString(STT_hang[i]);
                    lbl.Font = new Font("Segoe Script", 20);
                    lbl.ForeColor = Color.Aqua;
                    lbl.Width = 60;
                    lbl.Height = 60;
                    lbl.Location = new Point(x_lbl + kc_lbl, y_lbl);
                }
                panel.Controls.Add(lbl);
                x_lbl = lbl.Location.X;
                y_lbl = lbl.Location.Y;
                kc_lbl = lbl.Width + 10;
            }

            x_lbl = 0;
            y_lbl = 70;

            int X = 60, Y = 60, KC = 0;

            for (int i = 0; i < banco.getMang().GetLength(0); i++)
            {
                Label lbl = new Label();
                {
                    lbl.Text = STT_cot[i];
                    lbl.Font = new Font("Segoe Script", 20);
                    lbl.ForeColor = Color.Aqua;
                    lbl.Width = 60;
                    lbl.Height = 60;
                    lbl.Location = new Point(x_lbl, y_lbl);
                }
                panel.Controls.Add(lbl);
                y_lbl += lbl.Height + 12;

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
                X = 60;
                Y += O_Co.height;
                KC = 0;
            }
        }

        public void Ve_quanco(PictureBox pic, String nameChess)
        {
            if (nameChess == "Bot")
                pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintChess1_Event);
            else if (nameChess == "Player")
                pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.PaintChess3_Event);
        }

        public void madituan(int x, int y)
        {
            banco.getMang()[x, y].Check = 1;

            dd_X[goal] = x;
            dd_Y[goal] = y;

            if (duyetchienthang())
            {
                this.win = true;
            }
            else
            {
                NuocDi[] LuaChon = new NuocDi[8];
                int so_nd = 0;

                for (int i = 0; i < 8; i++)
                {
                    int x_tmp = x + col[i];
                    int y_tmp = y + row[i];
                    if (x_tmp < this.so_o && x_tmp >= 0 && y_tmp < this.so_o && y_tmp >= 0 && banco.getMang()[x_tmp, y_tmp].Check == 0)
                    {
                        NuocDi tmp = new NuocDi(CountMove(x_tmp, y_tmp), x_tmp, y_tmp);
                        LuaChon[so_nd] = tmp;
                        so_nd++;

                    }
                }

                if (so_nd > 0)
                {
                    for (int i = 0; i < so_nd - 1; i++)
                    {
                        for (int j = i + 1; j < so_nd; j++)
                        {
                            if (LuaChon[i].So_nd > LuaChon[j].So_nd)
                            {
                                NuocDi tmp = LuaChon[i];
                                LuaChon[i] = LuaChon[j];
                                LuaChon[j] = tmp;

                            }
                        }
                    }
                }

                for (int i = 0; i < so_nd && !this.win; i++)
                {
                    this.goal++;
                    madituan(LuaChon[i].X, LuaChon[i].Y);
                    banco.getMang()[LuaChon[i].X, LuaChon[i].Y].Check = 0;
                    if (win != true)
                    {
                        this.goal--;
                    }

                }
            }
        } 

        public void Ve_goal(PictureBox pic, int goal)
        {
   //         this.goal++;
            pic.Paint += new System.Windows.Forms.PaintEventHandler(Paint_Event.Clear_Chess);
            pic.Invalidate();

            Label lbl = new Label();
            {
                lbl.Text = Convert.ToString(goal);
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