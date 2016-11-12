using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    public enum TankState
    {
        INVINCIBLE, DEATH, ORDINARY
    }

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    public enum PlayerFlag
    {
        PLAYER1, PLAYER2
    }
    [Serializable]
    public class PlayerTank : Tank
    {
        private PlayerFlag playerflag;

        public PlayerFlag Playerflag
        {
            get { return playerflag; }
            //set { playerflag = value; }
        }

        private bool isupmove;

        public bool Isupmove
        {
            get { return isupmove; }
            set { isupmove = value; }
        }
        private bool isdownmove;

        public bool Isdownmove
        {
            get { return isdownmove; }
            set { isdownmove = value; }
        }
        private bool isleftmove;

        public bool Isleftmove
        {
            get { return isleftmove; }
            set { isleftmove = value; }
        }
        private bool isrightmove;

        public bool Isrightmove
        {
            get { return isrightmove; }
            set { isrightmove = value; }
        }

        private int lifenum;

        public int Lifenum
        {
            get { return lifenum; }
            set { lifenum = value; }
        }

        private int ox;
        private int oy;

        public PlayerTank(Graphics g, Point location, Size size, String[] imageurl, int xspeed, int yspeed, Direction tankdirection, List<Bullet> bullets, PlayerFlag playerflag)
            : base(g, location, size, imageurl, xspeed, yspeed, tankdirection, bullets)
        {
            this.playerflag = playerflag;
            this.Lifenum = 3;
            ox = location.X;
            oy = location.Y;
        }

        public override void Move()
        {
            this.X = this.Tx;
            this.Y = this.Ty;
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (this.Playerflag == PlayerFlag.PLAYER1)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.Isleftmove = true;
                    this.Tankdirection = Direction.LEFT;
                }
                else if (e.KeyCode == Keys.D)
                {
                    this.Isrightmove = true;
                    this.Tankdirection = Direction.RIGHT;
                }
                else if (e.KeyCode == Keys.W)
                {
                    this.Isupmove = true;
                    this.Tankdirection = Direction.UP;
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.Isdownmove = true;
                    this.Tankdirection = Direction.DOWN;
                }
            }
            else if (this.Playerflag == PlayerFlag.PLAYER2)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.Isleftmove = true;
                    this.Tankdirection = Direction.LEFT;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.Isrightmove = true;
                    this.Tankdirection = Direction.RIGHT;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.Isupmove = true;
                    this.Tankdirection = Direction.UP;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.Isdownmove = true;
                    this.Tankdirection = Direction.DOWN;
                }
            }
        }
        public void KeyUp(KeyEventArgs e)
        {
            if (this.Playerflag == PlayerFlag.PLAYER1)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.Isleftmove = false;
                }
                else if (e.KeyCode == Keys.D)
                {
                    this.Isrightmove = false;
                }
                else if (e.KeyCode == Keys.W)
                {
                    this.Isupmove = false;
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.Isdownmove = false;
                }
                else if (e.KeyCode == Keys.J)
                {
                    this.Fire();
                }
            }
            else if (this.Playerflag == PlayerFlag.PLAYER2)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.Isleftmove = false;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.Isrightmove = false;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.Isupmove = false;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.Isdownmove = false;
                }
                else if (e.KeyCode == Keys.NumPad1)
                {
                    this.Fire();
                }
            }
        }

        public override Rectangle MoveTest()
        {
            if (this.Isleftmove)
            {
                this.Tx -= this.Xspeed;
            }
            if (this.Isrightmove)
            {
                this.Tx += this.Xspeed;
            }
            if (this.Isupmove)
            {
                this.Ty -= this.Yspeed;
            }
            if (this.Isdownmove)
            {
                this.Ty += this.Yspeed;
            }
            return new Rectangle(new Point(this.Tx, this.Ty), this.Size);
        }

        public override Bomb Death()
        {
            int lx = this.X;
            int ly = this.Y;
            //return base.Death();
            this.Lifenum--;
            //this.Islife = false;
            if (this.Lifenum < 0)
            {
                this.Islife = false;
            }
            else
            {
                this.X = this.ox;
                this.Y = this.oy;
                this.Tankdirection = Direction.UP;
                UndoMove();
            }
            this.sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
            this.sp.Play();
            return new Bomb(this.G, new Point(lx, ly), this.Size, new string[] { "others\\bomb.bmp" });
        }
    }
}
