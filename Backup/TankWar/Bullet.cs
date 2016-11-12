using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    [Serializable]
    public class Bullet :MoveElement    //×Óµ¯Àà
    {
        public Bullet(Graphics g,Point location, Size size, String[] imageurl, int xspeed, int yspeed)
            : base(g,location, size, imageurl, xspeed, yspeed)
        {
 
        }

        public override void Move()
        {
            base.Move();
        }
    }
}
