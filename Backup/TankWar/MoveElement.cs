using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    [Serializable]
    public class MoveElement : Element
    {
        private Direction direction;

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        private int xspeed;

        public int Xspeed
        {
            get { return xspeed; }
            set { xspeed = value; }
        }
        private int yspeed;

        public int Yspeed
        {
            get { return yspeed; }
            set { yspeed = value; }
        }

        public MoveElement(Graphics g,Point location, Size size, String[] imageurl, int xspeed, int yspeed)
            : base(g,location, size, imageurl)
        {
            this.Xspeed = xspeed;
            this.Yspeed = yspeed;
        }

        public virtual void Move()
        {
            this.X += this.Xspeed;
            this.Y += this.Yspeed;
        }
    }
}
