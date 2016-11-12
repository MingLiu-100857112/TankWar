using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// There are 5 elements types on the battle map: 1: Brick 2: Bush 3: Steel 4: River 5: Command Base
    /// The BRICK could be eliminated by tanks, while STEEL, BUSH, and RIVER not
    /// </summary>
    public enum ElementType
    {
        BUSH, BRICK, STEEL, RIVER, BASE
    }

    /// <summary>
    /// Map element
    /// </summary>
    public class MapElement : Block
    {
        private ElementType _elementtype;

        public ElementType ElementType
        {
            get { return _elementtype; }
            set { _elementtype = value; }
        }

        /// <summary>
        /// Create a map element
        /// </summary>
        /// <param name="img">defined bitmap image</param>
        /// <param name="position">defined x,y position</param>
        /// <param name="blocksize">defined size</param>
        /// <param name="path">defined file path</param>
        /// <param name="elemtp">defined element type</param>
        public MapElement(Graphics img, Point position, Size blocksize, String[] path, ElementType elemtp)
            : base(img, position, blocksize, path)
        {
            this.ElementType = elemtp;
        }
    }
}
