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
    public class Paint_Event
    {

        private static int x = 5;
        private static int y = 5;
        private static int width = 70;
        private static int height = 70;

        public static void PaintChess1_Event(Object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            Image img = Image.FromFile(Application.StartupPath + "\\Img\\ma_do.png");
            g.DrawImage(img, x, y, width, height);
        }

        public static void PaintChess2_Event(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image img = Image.FromFile(Application.StartupPath + "\\Img\\ma_xanh.png");
            g.DrawImage(img, x, y, width, height);
        }

        public static void PaintChess3_Event(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image img = Image.FromFile(Application.StartupPath + "\\Img\\ma_xanh.png");
            g.DrawImage(img, x, y, width, height);
        }

        public static void PaintGoal_XDo_Event(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image img = Image.FromFile(Application.StartupPath + "\\Img\\x_do.png");
            g.DrawImage(img, x, y, width, height);
        }

        public static void PaintGoal_XXanh_Event(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image img = Image.FromFile(Application.StartupPath + "\\Img\\x_xanh.png");
            g.DrawImage(img, x, y, width, height);
        }

        public static void Clear_Chess(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            PictureBox tmp = (PictureBox)sender;
            g.Clear(tmp.BackColor);
        }
    }
}