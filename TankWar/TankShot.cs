using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// Create tank shot/bullets with bitmap, x,y location, size, image path, and x,y moving rate
    /// </summary>
    public class TankShot : MoveBlock
    {
        /// <summary>
        /// Create tank shot/bullets with bitmap, x,y location, size, image path, and x,y moving rate
        /// </summary>
        public TankShot(Graphics g,Point location, Size size, String[] imageurl, int xspeed, int yspeed)
            : base(g,location, size, imageurl, xspeed, yspeed)
        {
 
        }

        public override void BlockMove()
        {
            base.BlockMove();
        }
    }
}
