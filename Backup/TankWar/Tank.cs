using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace TankWar
{
    [Serializable]
    public class Tank :MoveElement
    {
        //#region Moveable ≥…‘±

        private int step = 0;

        public SoundPlayer sp;

        private TankState tankstate;

        public TankState Tankstate
        {
            get { return tankstate; }
            set { tankstate = value; }
        }

        private Direction tankdirection;

        public Direction Tankdirection
        {
            get { return tankdirection; }
            set { tankdirection = value; }
        }

        public List<Bullet> bullets;

        public Tank(Graphics g, Point location, Size size, String[] imageurl, int xspeed, int yspeed, Direction tankdirection, List<Bullet> bullets)
            : base(g,location, size, imageurl, xspeed, yspeed)
        {
            this.Tankdirection = tankdirection;
            this.bullets = bullets;
            this.Tankstate = TankState.ORDINARY;
            sp = new SoundPlayer();
            UndoMove();
        }

        /*public override void Move()
        {
            //base.Move();
        }*/

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

        public void Fire()
        {
            int bxspeed = 0;
            int byspeed = 0;
            if (this.Tankdirection == Direction.UP)
            {
                bxspeed = 0;
                byspeed = -20;
            }
            else if (this.Tankdirection == Direction.DOWN)
            {
                bxspeed = 0;
                byspeed = 20;
            }
            else if (this.Tankdirection == Direction.LEFT)
            {
                bxspeed = -20;
                byspeed = 0;
            }
            else if (this.Tankdirection == Direction.RIGHT)
            {
                bxspeed = 20;
                byspeed = 0;
            }
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\DropCell.wav";
            sp.Play();
            this.bullets.Add(new Bullet(this.G, new Point(this.X + (this.Size.Width - 10) / 2, this.Y + (this.Size.Height - 10) / 2), new Size(5, 5), new string[] { "others\\bullet.bmp" }, bxspeed, byspeed));
        }

        public override Bomb Death()
        {
            //return base.Death();
            this.Islife = false;
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
            sp.Play();
            return new Bomb(this.G, new Point(this.X, this.Y), this.Size, new string[] { "others\\bomb.bmp" });
        }

        public override void Show()
        {
            if (this.Tankdirection == Direction.UP)
            {
                this.Imagestate = 0;
            }
            else if (this.Tankdirection == Direction.DOWN)
            {
                this.Imagestate = 2;
            }
            else if (this.Tankdirection == Direction.LEFT)
            {
                this.Imagestate = 3;
            }
            else if (this.Tankdirection == Direction.RIGHT)
            {
                this.Imagestate = 1;
            }
            base.Show();
        }

        public override void Move()
        {
            this.X = this.Tx;
            this.Y = this.Ty;
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

        public virtual Rectangle MoveTest()
        {
            switch (this.Tankdirection)
            {
                case Direction.UP:
                    this.Ty -= this.Yspeed;
                    break;
                case Direction.DOWN:
                    this.Ty += this.Yspeed;
                    break;
                case Direction.LEFT:
                    this.Tx -= this.Xspeed;
                    break;
                case Direction.RIGHT:
                    this.Tx += this.Xspeed;
                    break;
            }
            return new Rectangle(new Point(this.Tx, this.Ty), this.Size);
        }

        public void UndoMove()
        {
            this.Tx = this.X;
            this.Ty = this.Y;
        }

        public void SetTankDirection(Direction tankdirection)
        {
            this.Tankdirection = tankdirection;
        }

        public void AIRoundDirection()
        {
            if (this.Tankdirection == Direction.RIGHT)
            {
                this.Tankdirection = Direction.DOWN;
            }
            else if (this.Tankdirection == Direction.DOWN)
            {
                this.Tankdirection = Direction.LEFT;
            }
            else if (this.Tankdirection == Direction.LEFT)
            {
                this.Tankdirection = Direction.UP;
            }
            else
            {
                this.Tankdirection = Direction.RIGHT;
            }
        }

        public void AIRoundDirection1()
        {
            if (this.Tankdirection == Direction.RIGHT)
            {
                this.Tankdirection = Direction.UP;
            }
            else if (this.Tankdirection == Direction.DOWN)
            {
                this.Tankdirection = Direction.RIGHT;
            }
            else if (this.Tankdirection == Direction.LEFT)
            {
                this.Tankdirection = Direction.DOWN;
            }
            else
            {
                this.Tankdirection = Direction.LEFT;
          
            }
        }

        /*public void AIRoundDirection2()
        {
            if (this.Tankdirection == Direction.RIGHT)
            {
                this.Tankdirection = Direction.LEFT;
            }
            else if (this.Tankdirection == Direction.LEFT)
            {
                this.Tankdirection = Direction.RIGHT;
            }
            else if (this.Tankdirection == Direction.UP)
            {
                this.Tankdirection = Direction.DOWN;
            }
            else
            {
                this.Tankdirection = Direction.UP;
            }
        }*/

        //#endregion
    }
}
