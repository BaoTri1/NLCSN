using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaDITuan
{
    public class O_Co
    {
        public static int width = 70;
        public static int height = 70;
        private int check;
        private PictureBox o_co;
        private Boolean Lock;

        public O_Co()
        {
            this.Check = 0;
            this.Lock = false;

            this.o_co = new PictureBox();
            {
                o_co.Width = width;
                o_co.Height = height;
                o_co.BorderStyle = BorderStyle.Fixed3D;

            }

        }

        public int Check
        {
            get => check;
            set => check = value;
        }

        public bool Lock1 
        { 
            get => Lock; 
            set => Lock = value; 
        }

        public PictureBox getO_Co()
        {
            return o_co;
        }

    }
}
