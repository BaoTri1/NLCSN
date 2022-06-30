using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaDITuan
{
    public class Ban_Co
    {
        private O_Co[,] mango_co;
        private int so_oco;

        private int x, y;

        public Ban_Co(int so_o)
        {
            this.So_oco = so_o;

            //Tạo mảng các ô cờ
            mango_co = new O_Co[this.So_oco, this.So_oco];


            for (int i = 0; i < this.mango_co.GetLength(0); i++)
            {
                int count = 0;

                for (int j = 0; j < this.mango_co.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        O_Co o_co = new O_Co();
                        if (count % 2 == 0)
                        {
                            o_co.getO_Co().BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            o_co.getO_Co().BackColor = System.Drawing.Color.Black;
                        }
                        mango_co[i, j] = o_co;
                        count++;
                    }
                    else
                    {
                        O_Co o_co = new O_Co();
                        if (count % 2 == 0)
                        {
                            o_co.getO_Co().BackColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            o_co.getO_Co().BackColor = System.Drawing.Color.White;
                        }
                        mango_co[i, j] = o_co;
                        count++;
                    }
                }
            }
        }

        public int So_oco
        {
            get => so_oco;
            set => so_oco = value;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public O_Co[,] getMang()
        {
            return mango_co;
        }

        public void get_O_CO(PictureBox pic)
        {
            O_Co tmp = new O_Co();

            for(int i = 0; i < this.mango_co.GetLength(0); i++)
            {
                for(int j = 0; j < this.mango_co.GetLength(1); j++)
                {
                    if(this.mango_co[i, j].getO_Co() == pic)
                    {
                        this.x = i;
                        this.y = j;
                    }
                }
            }
        }

    }
}
