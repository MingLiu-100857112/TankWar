using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TankWar
{
    /// <summary>
    /// Basic parent class for all objects of the game, it allows the object to draw at defined position with defined bitmap
    /// </summary>
    public abstract class Block
    {
        /// <summary>
        /// Object's image
        /// </summary>
        private Graphics _img;

        public Graphics Img
        {
            get { return _img; }
            set { _img = value; }
        }

        /// <summary>
        /// Object's size
        /// </summary>
        private Size _blocksize;

        public Size BlockSize
        {
            get { return _blocksize; }
            set { _blocksize = value; }
        }

        /// <summary>
        /// Object's position x
        /// </summary>
        private int _positionx;

        public int PositionX
        {
            get { return _positionx; }
            set { _positionx = value; }
        }

        /// <summary>
        /// Object's position y
        /// </summary>
        private int _positiony;

        public int PositionY
        {
            get { return _positiony; }
            set { _positiony = value; }
        }

        /// <summary>
        /// Block's image array that links to bitmap resource
        /// </summary>
        private List<Image> _images;

        public List<Image> Images
        {
            get { return _images; }
            set { _images = value; }
        }

        /// <summary>
        /// Object's image label number
        /// </summary>
        private int _imagestate;

        public int Imagestate
        {
            get { return _imagestate; }
            set { _imagestate = value; }
        }

        /// <summary>
        /// Object's state for game to judge whether keep it or not
        /// </summary>
        private bool _isalive;

        public bool Isalive
        {
            get { return _isalive; }
            set { _isalive = value; }
        }

        /// <summary>
        /// Creat an object at defined position with defined image and set the state of object is true (keep it)
        /// </summary>
        /// <param name="img">bitmap resource</param>
        /// <param name="position">x,y value</param>
        /// <param name="blocksize">size value</param>
        /// <param name="path">resources file path</param>
        public Block(Graphics img, Point position, Size blocksize, String[] path)
        {
            this.PositionX = position.X;
            this.PositionY = position.Y;
            this.BlockSize = blocksize;
            Images = new List<Image>();
            this.Img = img;
            for (int i = 0; i < path.Length; i++)
            {
                Images.Add(Image.FromFile(Application.StartupPath + "\\Resource\\images\\" + path[i]));
            }
            this._isalive = true;
            this.Imagestate = 0;
        }

        /// <summary>
        /// Pre-set a rec area for the object's image
        /// </summary>
        /// <returns>return a new rect block with size and position</returns>
        public Rectangle GetRectangleBlock()
        {
            return new Rectangle(new Point(this.PositionX, this.PositionY), this.BlockSize);
        }

        /// <summary>
        /// Draw the object at defined position use defined bitmap image
        /// </summary>
        public virtual void DrawBlock()
        {
            this._img.DrawImage(this.Images[_imagestate], new Rectangle(new Point(this.PositionX, this.PositionY), this.BlockSize));
        }

        /// <summary>
        /// Set the object's state to false to let game delete it
        /// </summary>
        /// <returns>return null if already deleted, otherwise false</returns>
        public virtual Explosion Eliminate()
        {
            this.Isalive = false;
            return null;
        }
    }
}
