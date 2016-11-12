using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// Explosion effect when some tank (whether player or enemies) is shot and eliminated
    /// </summary>
    public class Explosion : Block
    {
        /// <summary>
        /// Stay time for explosion effect
        /// </summary>
        private int _alivetime;

        public int AliveTime
        {
            get { return _alivetime; }
            set { _alivetime = value; }
        }

        /// <summary>
        /// Create explosion with defined image, at defined x,y position, with defined size, and defined bitmap path
        /// </summary>
        /// <param name="img">defined bitmap image for object</param>
        /// <param name="position">defined x,y position</param>
        /// <param name="blocksize">defined object size</param>
        /// <param name="path">defined file path</param>
        public Explosion(Graphics img, Point position, Size blocksize, String[] path)
            : base(img, position, blocksize, path)
        {
            this.AliveTime = 3;
        }

        /// <summary>
        /// When stay time down to 0, set the object's state to false, let game delete it
        /// </summary>
        public void StayAlive()
        {
            this.AliveTime--;
            if (this.AliveTime <= 0)
            {
                this.Isalive = false;
            }
        }
    }
}
