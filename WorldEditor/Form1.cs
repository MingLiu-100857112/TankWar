using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;

namespace WorldEditor
{
    /// <summary>
    /// This is the main interface of map editor, users can paint map by defined elements
    /// </summary>
    public partial class Form1 : Form
    {
        private Graphics _map_element;
        private Bitmap _paint_field;
        private Thread _paint_thread;
        private bool _signal = true;
        private MapFile _map_saved;
        public Form1()
        {
            InitializeComponent();
            LauchForm();
        }

        /// <summary>
        /// Double Buffered Graphics and create the main interface and the side bar for operation
        /// </summary>
        private void LauchForm()
        {
            
            this.ClientSize = new Size(800, 600);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            _paint_field = new Bitmap(600, 600);
            _map_element = Graphics.FromImage(_paint_field);
            _map_saved = new MapFile();
        }

        /// <summary>
        /// Start the paint process
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">form event</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _paint_thread = new Thread(new ThreadStart(PaintMap));
            _paint_thread.Start();
        }

        /// <summary>
        /// Draw the map element by different element labels
        /// </summary>
        private void PaintMap()
        {
            while (_signal)
            {
                _map_element.FillRectangle(Brushes.Black, new Rectangle(0, 0, 600, 600));
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        switch (_map_saved.ElementLabel[i * 20 + j])
                        {
                            case "1":
                                _map_element.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\bigwall.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "2":
                                _map_element.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\grassland.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "3":
                                _map_element.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\iron.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "4":
                                _map_element.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\water.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "5":
                                _map_element.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\home.bmp"), j * 30, i * 30, 30, 30);
                                break;
                        }
                    }
                }
                this.Invalidate();
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Draw the paint area
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">form paint event</param>
        private void Form1_PaintArea(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_paint_field, 0, 0, 600, 600);
        }

        /// <summary>
        /// Close the map editor
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">form closing event</param>
        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            _signal = false;
            _paint_thread.Abort();
            _paint_thread.Join();
        }

        /// <summary>
        /// Different radio buttom represent different number that labels the map element, number is saved and used for drawing
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">mouse click event</param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X <= 600 && e.Y >= 0 && e.Y <= 600)
            {
                int x = e.X / 30;
                int y = e.Y / 30;
                String element_num;
                if (radioButton1.Checked)
                {
                    element_num = "0";
                }
                else if (radioButton2.Checked)
                {
                    element_num = "1";
                }
                else if (radioButton3.Checked)
                {
                    element_num = "2";
                }
                else if (radioButton4.Checked)
                {
                    element_num = "3";
                }
                else if (radioButton5.Checked)
                {
                    element_num = "4";
                }
                else
                {
                    element_num = "5";
                }
                _map_saved.ElementLabel[20 * y + x] = element_num;
            }
        }

        /// <summary>
        /// Respond to mouse in defined area
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">mouse event</param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X <= 600 && e.Y >= 0 && e.Y <= 600)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Save string array to map file
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">form event</param>
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog msg_save = new SaveFileDialog();
            msg_save.Filter = "map file|*.map";
            if (msg_save.ShowDialog() == DialogResult.OK)
            {
                FileStream newfs;
                StreamWriter newsw;
                try
                {
                    newfs = new FileStream(msg_save.FileName, FileMode.Create);
                    newsw = new StreamWriter(newfs);
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            newsw.Write(_map_saved.ElementLabel[i * 20 + j] + ",");
                        }
                    }
                    newsw.Flush();
                    newsw.Close();
                    newfs.Close();
                }
                catch 
                {

                }
            }
        }

        /// <summary>
        /// Click to exit
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">form event</param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click to show the help document about map editor
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">form event</param>
        private void button2_Click(object sender, EventArgs e)
        {
            AboutForm newabtfm = new AboutForm();
            newabtfm.ShowDialog();
        }
    }
}