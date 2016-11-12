using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// Bonus type KILLALL: make all enemies explode; ADDLIFE: add a life for the player
    /// </summary>
    public enum BonusType
    {
        KILLALL,ADDLIFE
    }

    /// <summary>
    /// Bouns is created by random when player eliminate enemies and disappear after certain time if player not touch it
    /// </summary>
    public class Bonus : Block
    {
        /// <summary>
        /// Stay time set for disappear for bonus items
        /// </summary>
        private int _staytime;

        public int StayTime
        {
            get { return _staytime; }
            set { _staytime = value; }
        }

        /// <summary>
        /// Bonus type KILLALL: make all enemies explode; ADDLIFE: add a life for the player
        /// </summary>
        private BonusType _bonustype;

        public BonusType BonusType
        {
            get { return _bonustype; }
            set { _bonustype = value; }
        }

        /// <summary>
        /// Create bonus object with defined bitmap, at defined x,y position, with defined size and defined type
        /// </summary>
        /// <param name="img">bitmap for bonus</param>
        /// <param name="position">x,y position for bonus object</param>
        /// <param name="blocksize">size of bonus</param>
        /// <param name="path">image path for loading</param>
        /// <param name="bonustype">bonus type</param>
        public Bonus(Graphics img, Point position, Size blocksize, String[] path, BonusType bonustype)
            : base(img,position, blocksize, path)
        {
            this.BonusType = bonustype;
            this._staytime = 300;
        }

        /// <summary>
        /// When the set stay time down to 0, set the state to false, so game will delete it
        /// </summary>
        public void StayAlive()
        {
            this.StayTime--;
            if (this.StayTime <= 0)
            {
                this.Isalive = false;
            }
        }
    }
}
