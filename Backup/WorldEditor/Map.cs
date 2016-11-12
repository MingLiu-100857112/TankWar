using System;
using System.Collections.Generic;
using System.Text;

namespace WorldEditor
{
    public class Map
    {
        private String[] terrains;

        public String[] Terrains
        {
            get { return terrains; }
            set { terrains = value; }
        }

        public Map()
        {
            Terrains = new string[400];
            for (int i = 0; i < Terrains.Length; i++)
            {
                Terrains[i] = "0";
            }
        }
    }
}
