using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TankWar
{
    /// <summary>
    /// There are three status of tanks, INVINCIBLE: can not be eliminated (when obtain bonus, still working on it); DEATH: been shot and eliminated; ORDINARY: normal state, stay alive
    /// </summary>
    public enum TankStatus
    {
        INVINCIBLE, DEATH, ORDINARY
    }

    /// <summary>
    /// Four directions for the tanks' movement, up, down , left, right
    /// </summary>
    public enum TowardDirection
    {
        UP, DOWN, LEFT, RIGHT,
    }

    /// <summary>
    /// Players' labels to define player 1 or 2
    /// </summary>
    public enum PlayerSymbol
    {
        PLAYER1, PLAYER2
    }

    /// <summary>
    /// Create player tank which is child of Tanks (enemy), defined move direction, Graphics to draw, x,y location, block size image url, x moving speed, y moving speed, tank bullets, player's symbol (player 1 or 2)
    /// </summary>
    public class TankPlayer : Tanks
    {
        private PlayerSymbol _playersymbol;

        public PlayerSymbol PlayerSymbol
        {
            get { return _playersymbol; }
        }

        private bool _ismoveup;

        public bool IsMoveUp
        {
            get { return _ismoveup; }
            set { _ismoveup = value; }
        }

        private bool _ismovedown;

        public bool IsMoveDown
        {
            get { return _ismovedown; }
            set { _ismovedown = value; }
        }

        private bool _ismoveleft;

        public bool IsMoveLeft
        {
            get { return _ismoveleft; }
            set { _ismoveleft = value; }
        }

        private bool _ismoveright;

        public bool IsMoveRight
        {
            get { return _ismoveright; }
            set { _ismoveright = value; }
        }

        private int lifenum;

        public int Lifenum
        {
            get { return lifenum; }
            set { lifenum = value; }
        }

        private int ox;
        private int oy;

        /// <summary>
        /// Constructor defined move direction, Graphics to draw, x,y location, block size image url, x moving speed, y moving speed, tank bullets, player's symbol (player 1 or 2)
        /// </summary>
        /// <param name="g">graphic object for drawing</param>
        /// <param name="location">x,y location</param>
        /// <param name="size">block size</param>
        /// <param name="imageurl">image path</param>
        /// <param name="xspeed">x moving rate</param>
        /// <param name="yspeed">y moving rate</param>
        /// <param name="tankdirection">direction of player tank</param>
        /// <param name="bullets">bullet of player tank</param>
        /// <param name="playerflag">symbol of player tank (playwe 1 or 2)</param>
        public TankPlayer(Graphics g, Point location, Size size, String[] imageurl, int xspeed, int yspeed, TowardDirection tankdirection, List<TankShot> bullets, PlayerSymbol playerflag)
            : base(g, location, size, imageurl, xspeed, yspeed, tankdirection, bullets)
        {
            this._playersymbol = playerflag;
            this.Lifenum = 3;
            ox = location.X;
            oy = location.Y;
        }

        /// <summary>
        /// Move the block to new x,y location
        /// </summary>
        public override void BlockMove()
        {
            this.PositionX = this.Tx;
            this.PositionY = this.Ty;
        }

        /// <summary>
        /// When player key down, activate the direction move
        /// </summary>
        /// <param name="e">key event</param>
        public void KeyDown(KeyEventArgs e)
        {
            if (this.PlayerSymbol == PlayerSymbol.PLAYER1)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.IsMoveLeft = true;
                    this.Tankdirection = TowardDirection.LEFT;
                }
                else if (e.KeyCode == Keys.D)
                {
                    this.IsMoveRight = true;
                    this.Tankdirection = TowardDirection.RIGHT;
                }
                else if (e.KeyCode == Keys.W)
                {
                    this.IsMoveUp = true;
                    this.Tankdirection = TowardDirection.UP;
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.IsMoveDown = true;
                    this.Tankdirection = TowardDirection.DOWN;
                }
            }
            else if (this.PlayerSymbol == PlayerSymbol.PLAYER2)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.IsMoveLeft = true;
                    this.Tankdirection = TowardDirection.LEFT;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.IsMoveRight = true;
                    this.Tankdirection = TowardDirection.RIGHT;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.IsMoveUp = true;
                    this.Tankdirection = TowardDirection.UP;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.IsMoveDown = true;
                    this.Tankdirection = TowardDirection.DOWN;
                }
            }
        }

        /// <summary>
        /// When player key up, de-activate the direction move
        /// </summary>
        /// <param name="e">key event</param>
        public void KeyUp(KeyEventArgs e)
        {
            if (this.PlayerSymbol == PlayerSymbol.PLAYER1)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.IsMoveLeft = false;
                }
                else if (e.KeyCode == Keys.D)
                {
                    this.IsMoveRight = false;
                }
                else if (e.KeyCode == Keys.W)
                {
                    this.IsMoveUp = false;
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.IsMoveDown = false;
                }
                else if (e.KeyCode == Keys.J)
                {
                    this.Fire();
                }
            }
            else if (this.PlayerSymbol == PlayerSymbol.PLAYER2)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.IsMoveLeft = false;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.IsMoveRight = false;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.IsMoveUp = false;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.IsMoveDown = false;
                }
                else if (e.KeyCode == Keys.B)
                {
                    this.Fire();
                }
            }
        }

        /// <summary>
        /// Move toward 4 directions, reset the x,y position
        /// </summary>
        /// <returns>new rect block with x,y position and size</returns>
        public override Rectangle MoveTest()
        {
            if (this.IsMoveLeft)
            {
                this.Tx -= this.XRate;
            }
            if (this.IsMoveRight)
            {
                this.Tx += this.XRate;
            }
            if (this.IsMoveUp)
            {
                this.Ty -= this.YRate;
            }
            if (this.IsMoveDown)
            {
                this.Ty += this.YRate;
            }
            return new Rectangle(new Point(this.Tx, this.Ty), this.BlockSize);
        }

        /// <summary>
        /// When player is eliminated by enemies, decrease the life number by 1, set the states to false and let game eliminate itself, then return an explosion effect at its position, and play sound effect
        /// </summary>
        /// <returns>explosion effect with x,y position and size</returns>
        public override Explosion Eliminate()
        {
            int lx = this.PositionX;
            int ly = this.PositionY;
            this.Lifenum--;
            if (this.Lifenum < 0)
            {
                this.Isalive = false;
            }
            else
            {
                this.PositionX = this.ox;
                this.PositionY = this.oy;
                this.Tankdirection = TowardDirection.UP;
                UndoMove();
            }
            this.sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
            this.sp.Play();
            return new Explosion(this.Img, new Point(lx, ly), this.BlockSize, new string[] { "others\\bomb.bmp" });
        }
    }
}
