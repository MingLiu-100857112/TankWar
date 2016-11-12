using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace TankWar
{
    /// <summary>
    /// Main interfaces of the game
    /// </summary>
    public partial class GameInterface : Form
    {
        public StartForm statf;
        public Bitmap bufferimg;
        private Graphics _grapbufimg;
        private bool _signal = true;
        private Thread _threadgame;
        private Thread _threaddraw;
        private Scene _scene;
        private int _sceneNum = 1;
        private bool _isover = false;
        private int _playernum;

        /// <summary>
        /// Control the start form and load releated elements (player number and background image)
        /// </summary>
        /// <param name="statfm">start form</param>
        /// <param name="choose">game option</param>
        /// <param name="playernum">1 player or two players</param>
        public GameInterface(StartForm statfm, StartOrLoad choose, int playernum)
        {
            InitializeComponent();
            LauchInterface();
            this._playernum = playernum;
            if (choose == StartOrLoad.START)
            {
                _scene = new Scene(this, playernum, _grapbufimg, _sceneNum);
            }
            this.statf = statfm;
        }

        /// <summary>
        /// The following code example enables double-buffering on a Form and updates the styles to reflect the changes.
        /// </summary>
        private void LauchInterface()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            bufferimg = new Bitmap(800, 600);
            _grapbufimg = Graphics.FromImage(bufferimg);
            this.ClientSize = new Size(800, 600);
        }

        /// <summary>
        /// Start the game by the use's option and load the score (player number)
        /// </summary>
        private void RunGame()
        {
            while (_signal)
            {
                if (_scene.SecnePass())
                {
                    int scoreshown = _scene.gamescore;
                    List<int> plifenum = new List<int>();
                    for(int i = 0;i<_scene._tankplayers.Count;i++)
                    {
                        plifenum.Add(_scene._tankplayers[i].Lifenum);
                    }
                    _sceneNum++;
                    _scene = new Scene(this, _playernum, _grapbufimg, _sceneNum, scoreshown, plifenum);
                }
                Thread.Sleep(60);
            }
        }

        /// <summary>
        /// Draw the main screen of the game
        /// </summary>
        private void DrawGame()
        {
            while (_signal)
            {
                _scene.DrawMainScreen();
                this.Invalidate();
                Thread.Sleep(40);
            }
        }

        /// <summary>
        /// Close the main screen
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Draw the frame of the main screen
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceDraw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bufferimg, 0, 0, 800, 600);
        }

        /// <summary>
        /// Thread loading
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceLoad(object sender, EventArgs e)
        {
            _threadgame = new Thread(new ThreadStart(RunGame));
            _threaddraw = new Thread(new ThreadStart(DrawGame));
            _threadgame.Start();
            _threaddraw.Start();
        }

        /// <summary>
        /// Link the form event with closing the game
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceClosing(object sender, FormClosingEventArgs e)
        {
            CloseGame();
        }

        /// <summary>
        /// Close the game
        /// </summary>
        public void CloseGame()
        {
            if (_isover)
            {
                _threadgame.Resume();
                _isover = !_isover;
            }
            _threadgame.Abort();
            _threadgame.Join();
            _threaddraw.Abort();
            _threaddraw.Join();
        }

        /// <summary>
        /// Click the form element
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceMouseClick(object sender, MouseEventArgs e)
        {
            _scene.ClickMouse(e);
        }

        /// <summary>
        /// Judge the mouse position is at the form element's field
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceMouseMove(object sender, MouseEventArgs e)
        {
            _scene.MoveMouse(e);
        }

        /// <summary>
        /// Player coud pause the game by press K and N
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceKeyDown(object sender, KeyEventArgs e)
        {
            _scene.PlayerKeyDown(e);
            if (e.KeyCode == Keys.K || e.KeyCode == Keys.N)
            {
                if (!_isover)
                {
                    _threadgame.Suspend();
                    _isover = true;
                }
                else
                {
                    _threadgame.Resume();
                    _isover = false;
                }
            }
        }

        /// <summary>
        /// Detect the player key action
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void GameInterfaceKeyUp(object sender, KeyEventArgs e)
        {
            _scene.PlayerKeyUp(e);
        }
    }
}