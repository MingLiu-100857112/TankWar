using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    [Serializable]
    public class Element        //∆¡ƒª…œ‘™Àÿ¿‡
    {
        private Graphics g;

        public Graphics G
        {
            get { return g; }
            set { g = value; }
        }

        /*private Point location;

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }*/

        private Size size;

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        /*private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }*/

        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private List<Image> images;

        public List<Image> Images
        {
            get { return images; }
            set { images = value; }
        }

        private int imagestate;

        public int Imagestate
        {
            get { return imagestate; }
            set { imagestate = value; }
        }

        private bool islife;

        public bool Islife
        {
            get { return islife; }
            set { islife = value; }
        }

        public Element(Graphics g,Point location, Size size, String[] imageurl)
        {
            //this.Location = location;
            this.X = location.X;
            this.Y = location.Y;
            this.Size = size;
            Images = new List<Image>();
            this.G = g;
            for (int i = 0; i < imageurl.Length; i++)
            {
                //this.Images[i] = Image.FromFile(Application.StartupPath + @"\Resource\images\" + imageurl[i]);
                Images.Add(Image.FromFile(Application.StartupPath + "\\Resource\\images\\" + imageurl[i]));
            }
            this.islife = true;
            this.Imagestate = 0;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(new Point(this.X, this.Y), this.Size);
        }

        public virtual void Show()
        {
            this.g.DrawImage(this.Images[imagestate], new Rectangle(new Point(this.X, this.Y), this.Size));
        }

        public virtual Bomb Death()
        {
            this.Islife = false;
            return null;
        }
    }
}
