using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    public enum TerrainType
    {
        GRASSLAND,WALL,IRON,WATER,HOME
    }
    [Serializable]
    public class Terrain : Element       //µØÐÎÔªËØ
    {
        private TerrainType terraintype;

        public TerrainType Terraintype
        {
            get { return terraintype; }
            set { terraintype = value; }
        }

        public Terrain(Graphics g, Point location, Size size, String[] imageurl, TerrainType terraintype)
            : base(g, location, size, imageurl)
        {
            this.Terraintype = terraintype;
        }
    }
}
