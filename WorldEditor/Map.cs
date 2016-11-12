using System;

namespace WorldEditor
{
    /// <summary>
    /// MapFile saves string array with numbers splited by ","
    /// </summary>
    public class MapFile
    {
        private String[] _element_label;

        public String[] ElementLabel
        {
            get { return _element_label; }
            set { _element_label = value; }
        }

        /// <summary>
        /// Initiation string array all by 0;
        /// </summary>
        public MapFile()
        {
            ElementLabel = new string[400];
            for (int i = 0; i < ElementLabel.Length; i++)
            {
                ElementLabel[i] = "0";
            }
        }
    }
}
