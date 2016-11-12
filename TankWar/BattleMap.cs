using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;


namespace TankWar
{
    /// <summary>
    /// BattleMAp is for reading the text file data and transform the data to be map
    /// </summary>
    public class BattleMap
    {
        /// <summary>
        /// There are 5 elements on the battle map: 1: Brick 2: Bush 3: Steel 4: River 5: Command Base
        /// </summary>
        private List<MapElement> _elements;

        public List<MapElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        private Graphics _img;

        public BattleMap(Graphics img)
        {
            this.Elements = new List<MapElement>();
            this._img = img;
        }
        /// <summary>
        /// Read map file which is actually an array of int splited by "," and allocate different image to the element to form the map
        /// </summary>
        /// <param name="path">path of file</param>
        public void ReadBattleMap(string path)
        {
            FileStream fstr1 = null;
            StreamReader strr1 = null;
            string[] strl1;
            string bmst1 = null;
            try
            {
                fstr1 = new FileStream(path, FileMode.Open);
                strr1 = new StreamReader(fstr1);
                bmst1 = strr1.ReadToEnd();
                strr1.Close();
                fstr1.Close();
            }
            catch
            {
                MessageBox.Show("Load Success", "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            strl1 = bmst1.Split(new char[] { ',' });
            for(int j = 0;j <20;j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (strl1[j * 20 + i].Trim().Equals("1"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\bigwall.bmp" }, ElementType.BRICK));
                    }
                    else if(strl1[j * 20 + i].Trim().Equals("2"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\grassland.bmp" }, ElementType.BUSH));
                    }
                    else if (strl1[j * 20 + i].Trim().Equals("3"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\iron.bmp" }, ElementType.STEEL));
                    }
                    else if(strl1[j * 20 + i].Trim().Equals("4"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\water.bmp" }, ElementType.RIVER));
                    }
                    else if (strl1[j * 20 + i].Trim().Equals("5"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\home.bmp" }, ElementType.BASE));
                    }
                }
            }
        }
    }
}
