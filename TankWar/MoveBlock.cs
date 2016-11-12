using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// MoveBlock is parent of tanks and it defined direction and move action
    /// </summary>
    public abstract class MoveBlock : Block
    {
        /// <summary>
        /// Move direction
        /// </summary>
        private TowardDirection _direction;

        public TowardDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// Object's move rate on x
        /// </summary>
        private int _xrate;

        public int XRate
        {
            get { return _xrate; }
            set { _xrate = value; }
        }

        /// <summary>
        /// Object's move rate on y
        /// </summary>
        private int _yrate;

        public int YRate
        {
            get { return _yrate; }
            set { _yrate = value; }
        }

        /// <summary>
        /// Create move object with defined x,y position, size, and the move rate
        /// </summary>
        /// <param name="img">bitmap for object</param>
        /// <param name="position">x,y position before</param>
        /// <param name="blocksize">size of block</param>
        /// <param name="path">load file path</param>
        /// <param name="xrate">x move rate</param>
        /// <param name="yrate">y move rate</param>
        public MoveBlock(Graphics img, Point position, Size blocksize, String[] path, int xrate, int yrate)
            : base(img,position, blocksize, path)
        {
            this.XRate = xrate;
            this.YRate = yrate;
        }

        /// <summary>
        /// Move object by x,y rate
        /// </summary>
        public virtual void BlockMove()
        {
            this.PositionX += this.XRate;
            this.PositionY += this.YRate;
        }
    }
}
