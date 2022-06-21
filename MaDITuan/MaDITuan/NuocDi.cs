using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaDITuan
{
    public class NuocDi
    {
        private int so_nd;
        private int x;
        private int y;

        public NuocDi(int so_nd, int x, int y)
        {
            this.So_nd = so_nd;
            this.X = x;
            this.Y = y;
        }

        public int So_nd
        {
            get => so_nd;
            set => so_nd = value;
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }
    }
}
