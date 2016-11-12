using System;
using System.Collections.Generic;
using System.Text;

namespace TankWar
{
    [Serializable]
    public class Save       //¥Êµµ¿‡
    {
        public List<Tank> tanks;
        public List<PlayerTank> playertanks;
        public List<Bullet> tbullets;
        public List<Bullet> pbullets;
        public List<Bomb> bombs;
        public Map map;

        private int levelnum;

        public int Levelnum
        {
            get { return levelnum; }
            set { levelnum = value; }
        }

        private int eptank_count;

        public int Eptank_count
        {
            get { return eptank_count; }
            set { eptank_count = value; }
        }

        public Save(int levelnum, int eptank_count)
        {
            this.Levelnum = levelnum;
            this.Eptank_count = eptank_count;
        }

        public void SetElement(List<Tank> tanks, List<PlayerTank> playertanks, List<Bullet> tbullets, List<Bullet> pbullets, List<Bomb> bombs, Map map)
        {
            this.tanks = tanks;
            this.playertanks = playertanks;
            this.tbullets = tbullets;
            this.pbullets = pbullets;
            this.bombs = bombs;
            this.map = map;
        }
    }
}
