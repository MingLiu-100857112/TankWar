using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    public enum PrizeType
    {
        BOMB,TANK
    }
    [Serializable]
    public class Prize : Element     //Ω±¿¯¿‡
    {
        private int lifestep;

        public int Lifestep
        {
            get { return lifestep; }
            set { lifestep = value; }
        }

        private PrizeType prizetype;

        public PrizeType Prizetype
        {
            get { return prizetype; }
            set { prizetype = value; }
        }

        public Prize(Graphics g,Point location, Size size, String[] imageurl, PrizeType prizetype)
            : base(g,location, size, imageurl)
        {
            this.Prizetype = prizetype;
            this.lifestep = 300;
        }

        public void LiveRun()
        {
            this.Lifestep--;
            if (this.Lifestep <= 0)
            {
                this.Islife = false;
            }
        }
    }
}
