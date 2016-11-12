using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WorldEditor
{
    public partial class Form1 : Form
    {
        private Graphics gbuffere;
        private Bitmap buffereimage;
        private Thread paintthread;
        private bool flag = true;
        private Map map;
        public Form1()
        {
            InitializeComponent();
            LauchForm();
        }

        private void LauchForm()
        {
            this.ClientSize = new Size(800, 600);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            buffereimage = new Bitmap(600, 600);
            gbuffere = Graphics.FromImage(buffereimage);
            map = new Map();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            paintthread = new Thread(new ThreadStart(PaintWorld));
            paintthread.Start();
        }

        private void PaintWorld()
        {
            while (flag)
            {
                gbuffere.FillRectangle(Brushes.Black, new Rectangle(0, 0, 600, 600));
                //String imagename;
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        switch (map.Terrains[i * 20 + j])
                        {
                            case "1":
                                gbuffere.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\bigwall.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "2":
                                gbuffere.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\grassland.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "3":
                                gbuffere.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\iron.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "4":
                                gbuffere.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\water.bmp"), j * 30, i * 30, 30, 30);
                                break;
                            case "5":
                                gbuffere.DrawImage(Image.FromFile(Application.StartupPath + "\\Resource\\images\\Terrains\\home.bmp"), j * 30, i * 30, 30, 30);
                                break;
                        }
                    }
                }
                this.Invalidate();
                Thread.Sleep(50);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(buffereimage, 0, 0, 600, 600);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            flag = false;
            paintthread.Abort();
            paintthread.Join();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X <= 600 && e.Y >= 0 && e.Y <= 600)
            {
                int x = e.X / 30;
                int y = e.Y / 30;
                String value;
                if (radioButton1.Checked)
                {
                    value = "0";
                }
                else if (radioButton2.Checked)
                {
                    value = "1";
                }
                else if (radioButton3.Checked)
                {
                    value = "2";
                }
                else if (radioButton4.Checked)
                {
                    value = "3";
                }
                else if (radioButton5.Checked)
                {
                    value = "4";
                }
                else
                {
                    value = "5";
                }

                map.Terrains[20 * y + x] = value;
            }
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "µØÍ¼ÎÄ¼þ|*.map";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs;
                StreamWriter sw;
                try
                {
                    fs = new FileStream(sfd.FileName, FileMode.Create);
                    sw = new StreamWriter(fs);
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            sw.Write(map.Terrains[i * 20 + j] + ",");
                        }
                        //sw.Write("\n");
                    }
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                catch 
                { }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }
    }
}