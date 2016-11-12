using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    [Serializable]
    public class Map
    {
        private List<Terrain> terrains;

        public List<Terrain> Terrains
        {
            get { return terrains; }
            set { terrains = value; }
        }

        private Graphics g;

        public Map(Graphics g)
        {
            this.Terrains = new List<Terrain>();
            this.g = g;
        }

        public void ReadMap(string mapurl)
        {
            FileStream fs = null;
            StreamReader sr = null;
            string[] linestr;
            string mapstr = null;
            try
            {
                fs = new FileStream(mapurl, FileMode.Open);
                sr = new StreamReader(fs);
                mapstr = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("指定地图不存在或者已损坏！", "读取地图出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            linestr = mapstr.Split(new char[] { ',' });
            for(int j = 0;j <20;j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (linestr[j * 20 + i].Trim().Equals("1"))
                    {
                        this.Terrains.Add(new Terrain(g, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\bigwall.bmp" }, TerrainType.WALL));
                    }
                    else if(linestr[j * 20 + i].Trim().Equals("2"))
                    {
                        this.Terrains.Add(new Terrain(g, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\grassland.bmp" }, TerrainType.GRASSLAND));
                    }
                    else if (linestr[j * 20 + i].Trim().Equals("3"))
                    {
                        this.Terrains.Add(new Terrain(g, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\iron.bmp" }, TerrainType.IRON));
                    }
                    else if(linestr[j * 20 + i].Trim().Equals("4"))
                    {
                        this.Terrains.Add(new Terrain(g, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\water.bmp" }, TerrainType.WATER));
                    }
                    else if (linestr[j * 20 + i].Trim().Equals("5"))
                    {
                        this.Terrains.Add(new Terrain(g, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\home.bmp" }, TerrainType.HOME));
                    }
                }
            }
        }
    }
}
