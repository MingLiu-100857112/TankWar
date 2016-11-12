using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TankWar
{
    //[Serializable]
    public partial class GameForm : Form
    {
        public StartForm sf;
        public Bitmap buffereimage;
        private Graphics gbuffereimage;
        private bool flag = true;
        private Thread gamethread;
        private Thread drawthread;
        private Level level;
        private int levelnum = 1;
        //private Image background;
        private bool isstop = false;
        private int playercount;
        public GameForm(StartForm sf,StartOrLoad sl,int playercount)
        {
            InitializeComponent();
            LauchForm();
            this.playercount = playercount;
            if (sl == StartOrLoad.START)
            {
                level = new Level(this, playercount, gbuffereimage, levelnum);
            }
            /*else if (sl == StartOrLoad.LOAD)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(Application.StartupPath + @"\save\save.dat", FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    Save save = (Save)bf.Deserialize(fs);
                    level = new Level(save, this, gbuffereimage);
                    fs.Close();
                }
                catch
                {
                    MessageBox.Show("存档不存在或者已经损坏！", "载入存档错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }*/
            this.sf = sf;
        }

        private void LauchForm()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            buffereimage = new Bitmap(800, 600);
            gbuffereimage = Graphics.FromImage(buffereimage);
            this.ClientSize = new Size(800, 600);
            //this.Size = new Size(800, 600);
        }

        private void GameRun()
        {
            while (flag)
            {
                //gbuffereimage.DrawImage(, 0, 0, 800, 600);
                //gbuffereimage.Clear(Color.Black);
                if (level.GameDo())
                {
                    int score = level.score;
                    List<int> plifenum = new List<int>();
                    for(int i = 0;i<level.playertanks.Count;i++)
                    {
                        plifenum.Add(level.playertanks[i].Lifenum);
                    }
                    levelnum++;
                    level = new Level(this, playercount, gbuffereimage, levelnum, score, plifenum);
                }
                //level.DrawScreen();
                //this.Invalidate();
                Thread.Sleep(60);
            }
        }

        private void GameDraw()
        {
            while (flag)
            {
                level.DrawScreen();
                this.Invalidate();
                Thread.Sleep(40);
            }
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(buffereimage, 0, 0, 800, 600);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            gamethread = new Thread(new ThreadStart(GameRun));
            drawthread = new Thread(new ThreadStart(GameDraw));
            gamethread.Start();
            drawthread.Start();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameClose();
        }

        public void GameClose()
        {
            if (isstop)
            {
                gamethread.Resume();
                isstop = !isstop;
            }
            gamethread.Abort();
            gamethread.Join();
            drawthread.Abort();
            drawthread.Join();
        }

        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            level.MouseClick(e);
        }

        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            level.MouseMove(e);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            level.KeyDown(e);
            if (e.KeyCode == Keys.K || e.KeyCode == Keys.NumPad2)
            {
                //paintthread.Suspend();
                if (!isstop)
                {
                    gamethread.Suspend();
                    isstop = true;
                }
                else
                {
                    gamethread.Resume();
                    isstop = false;
                }
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            level.KeyUp(e);
        }
    }
}