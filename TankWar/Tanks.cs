using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace TankWar
{
    /// <summary>
    /// Tanks is for enemies, moreover it is the player tank's parent.
    /// </summary>
    public class Tanks : MoveBlock
    {
        private int step = 0;
        public SoundPlayer sp;
        private TankStatus tankstate;
        public TankStatus Tankstate
        {
            get { return tankstate; }
            set { tankstate = value; }
        }

        private TowardDirection _tankdirection;
        public TowardDirection Tankdirection
        {
            get { return _tankdirection; }
            set { _tankdirection = value; }
        }

        public List<TankShot> bullets;

        /// <summary>
        /// Create enemy tank with bullets, bitmap image, location, size, imageurl, speed and direction
        /// </summary>
        /// <param name="g">graphic to draw bitmap</param>
        /// <param name="location">x,y position</param>
        /// <param name="size">block size</param>
        /// <param name="imageurl">path for the bitmap</param>
        /// <param name="xspeed">x move rate</param>
        /// <param name="yspeed">y move rate</param>
        /// <param name="tankdirection">direction type</param>
        /// <param name="bullets">tank shot</param>
        public Tanks(Graphics g, Point location, Size size, String[] imageurl, int xspeed, int yspeed, TowardDirection tankdirection, List<TankShot> bullets)
            : base(g,location, size, imageurl, xspeed, yspeed)
        {
            this.Tankdirection = tankdirection;
            this.bullets = bullets;
            this.Tankstate = TankStatus.ORDINARY;
            sp = new SoundPlayer();
            UndoMove();
        }

        private int tx;

        public int Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        private int ty;

        public int Ty
        {
            get { return ty; }
            set { ty = value; }
        }

        /// <summary>
        /// When enemy fires, the shot will move by different directions, and they will be drawn on the screen, with sound effect
        /// </summary>
        public void Fire()
        {
            int bxspeed = 0;
            int byspeed = 0;
            if (this.Tankdirection == TowardDirection.UP)
            {
                bxspeed = 0;
                byspeed = -20;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                bxspeed = 0;
                byspeed = 20;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                bxspeed = -20;
                byspeed = 0;
            }
            else if (this.Tankdirection == TowardDirection.RIGHT)
            {
                bxspeed = 20;
                byspeed = 0;
            }
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\DropCell.wav";
            sp.Play();
            this.bullets.Add(new TankShot(this.Img, new Point(this.PositionX + (this.BlockSize.Width - 10) / 2, this.PositionY + (this.BlockSize.Height - 10) / 2), new Size(5, 5), new string[] { "others\\bullet.bmp" }, bxspeed, byspeed));
        }

        /// <summary>
        /// When enemy's state is not alive then eliminated it, return a explosion effect, and play sound effect
        /// </summary>
        /// <returns>explosion effect object</returns>
        public override Explosion Eliminate()
        {
            this.Isalive = false;
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
            sp.Play();
            return new Explosion(this.Img, new Point(this.PositionX, this.PositionY), this.BlockSize, new string[] { "others\\bomb.bmp" });
        }

        /// <summary>
        /// Draw enemy tank by different directions
        /// </summary>
        public override void DrawBlock()
        {
            if (this.Tankdirection == TowardDirection.UP)
            {
                this.Imagestate = 0;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                this.Imagestate = 2;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                this.Imagestate = 3;
            }
            else if (this.Tankdirection == TowardDirection.RIGHT)
            {
                this.Imagestate = 1;
            }
            base.DrawBlock();
        }

        /// <summary>
        /// Enemy Tank's AI move pattern
        /// </summary>
        public override void BlockMove()
        {
            this.PositionX = this.Tx;
            this.PositionY = this.Ty;
            step++;
            Random rd = new Random();
            int r = rd.Next(0, 2);
            if (step == 15)
            {
                if (r == 0)
                {
                    AIRoundDirection1();
                }
                else
                {
                    AIRoundDirection();
                }
                step = 0;
            }
        }

        /// <summary>
        /// Update the position of block by different directions
        /// </summary>
        /// <returns>new Block with size and position</returns>
        public virtual Rectangle MoveTest()
        {
            switch (this.Tankdirection)
            {
                case TowardDirection.UP:
                    this.Ty -= this.YRate;
                    break;
                case TowardDirection.DOWN:
                    this.Ty += this.YRate;
                    break;
                case TowardDirection.LEFT:
                    this.Tx -= this.XRate;
                    break;
                case TowardDirection.RIGHT:
                    this.Tx += this.XRate;
                    break;
            }
            return new Rectangle(new Point(this.Tx, this.Ty), this.BlockSize);
        }

        /// <summary>
        /// Transfer the block postion to itself
        /// </summary>
        public void UndoMove()
        {
            this.Tx = this.PositionX;
            this.Ty = this.PositionY;
        }

        /// <summary>
        /// AI direction control scheme 1
        /// </summary>
        public void AIRoundDirection()
        {
            if (this.Tankdirection == TowardDirection.RIGHT)
            {
                this.Tankdirection = TowardDirection.DOWN;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                this.Tankdirection = TowardDirection.LEFT;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                this.Tankdirection = TowardDirection.UP;
            }
            else
            {
                this.Tankdirection = TowardDirection.RIGHT;
            }
        }

        /// <summary>
        /// AI direction control scheme 2
        /// </summary>
        public void AIRoundDirection1()
        {
            if (this.Tankdirection == TowardDirection.RIGHT)
            {
                this.Tankdirection = TowardDirection.UP;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                this.Tankdirection = TowardDirection.RIGHT;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                this.Tankdirection = TowardDirection.DOWN;
            }
            else
            {
                this.Tankdirection = TowardDirection.LEFT;
            }
        }
    }
}
