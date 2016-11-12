using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    public class Bomb : Element
    {
        private int step;

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        public Bomb(Graphics g, Point location, Size size, String[] imageurl)
            : base(g, location, size, imageurl)
        {
            this.Step = 3;
        }

        public void LiveRun()
        {
            this.Step--;
            if (this.Step <= 0)
            {
                this.Islife = false;
            }
        }
    }
}
