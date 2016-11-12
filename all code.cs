using System;
using System.Windows.Forms;

namespace TankWar
{
    /// <summary>
    /// The form is about author's information
    /// </summary>
    public partial class AboutForm : Form
    {
        /// <summary>
        /// Start to load the form
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click to exit the form
        /// </summary>
        /// <param name="sender">trigger</param>
        /// <param name="e">form event</param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;


namespace TankWar
{
    /// <summary>
    /// BattleMAp is for reading the text file data and transform the data to be map
    /// </summary>
    public class BattleMap
    {
        /// <summary>
        /// There are 5 elements on the battle map: 1: Brick 2: Bush 3: Steel 4: River 5: Command Base
        /// </summary>
        private List<MapElement> _elements;

        public List<MapElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        private Graphics _img;

        public BattleMap(Graphics img)
        {
            this.Elements = new List<MapElement>();
            this._img = img;
        }
        /// <summary>
        /// Read map file which is actually an array of int splited by "," and allocate different image to the element to form the map
        /// </summary>
        /// <param name="path">path of file</param>
        public void ReadBattleMap(string path)
        {
            FileStream fstr1 = null;
            StreamReader strr1 = null;
            string[] strl1;
            string bmst1 = null;
            try
            {
                fstr1 = new FileStream(path, FileMode.Open);
                strr1 = new StreamReader(fstr1);
                bmst1 = strr1.ReadToEnd();
                strr1.Close();
                fstr1.Close();
            }
            catch
            {
                MessageBox.Show("Load Success", "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            strl1 = bmst1.Split(new char[] { ',' });
            for(int j = 0;j <20;j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (strl1[j * 20 + i].Trim().Equals("1"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\bigwall.bmp" }, ElementType.BRICK));
                    }
                    else if(strl1[j * 20 + i].Trim().Equals("2"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\grassland.bmp" }, ElementType.BUSH));
                    }
                    else if (strl1[j * 20 + i].Trim().Equals("3"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\iron.bmp" }, ElementType.STEEL));
                    }
                    else if(strl1[j * 20 + i].Trim().Equals("4"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\water.bmp" }, ElementType.RIVER));
                    }
                    else if (strl1[j * 20 + i].Trim().Equals("5"))
                    {
                        this.Elements.Add(new MapElement(_img, new Point(i * 30, j * 30), new Size(30, 30), new string[] { "Terrains\\home.bmp" }, ElementType.BASE));
                    }
                }
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TankWar
{
    /// <summary>
    /// Basic parent class for all objects of the game, it allows the object to draw at defined position with defined bitmap
    /// </summary>
    public abstract class Block
    {
        /// <summary>
        /// Object's image
        /// </summary>
        private Graphics _img;

        public Graphics Img
        {
            get { return _img; }
            set { _img = value; }
        }

        /// <summary>
        /// Object's size
        /// </summary>
        private Size _blocksize;

        public Size BlockSize
        {
            get { return _blocksize; }
            set { _blocksize = value; }
        }

        /// <summary>
        /// Object's position x
        /// </summary>
        private int _positionx;

        public int PositionX
        {
            get { return _positionx; }
            set { _positionx = value; }
        }

        /// <summary>
        /// Object's position y
        /// </summary>
        private int _positiony;

        public int PositionY
        {
            get { return _positiony; }
            set { _positiony = value; }
        }

        /// <summary>
        /// Block's image array that links to bitmap resource
        /// </summary>
        private List<Image> _images;

        public List<Image> Images
        {
            get { return _images; }
            set { _images = value; }
        }

        /// <summary>
        /// Object's image label number
        /// </summary>
        private int _imagestate;

        public int Imagestate
        {
            get { return _imagestate; }
            set { _imagestate = value; }
        }

        /// <summary>
        /// Object's state for game to judge whether keep it or not
        /// </summary>
        private bool _isalive;

        public bool Isalive
        {
            get { return _isalive; }
            set { _isalive = value; }
        }

        /// <summary>
        /// Creat an object at defined position with defined image and set the state of object is true (keep it)
        /// </summary>
        /// <param name="img">bitmap resource</param>
        /// <param name="position">x,y value</param>
        /// <param name="blocksize">size value</param>
        /// <param name="path">resources file path</param>
        public Block(Graphics img, Point position, Size blocksize, String[] path)
        {
            this.PositionX = position.X;
            this.PositionY = position.Y;
            this.BlockSize = blocksize;
            Images = new List<Image>();
            this.Img = img;
            for (int i = 0; i < path.Length; i++)
            {
                Images.Add(Image.FromFile(Application.StartupPath + "\\Resource\\images\\" + path[i]));
            }
            this._isalive = true;
            this.Imagestate = 0;
        }

        /// <summary>
        /// Pre-set a rec area for the object's image
        /// </summary>
        /// <returns>return a new rect block with size and position</returns>
        public Rectangle GetRectangleBlock()
        {
            return new Rectangle(new Point(this.PositionX, this.PositionY), this.BlockSize);
        }

        /// <summary>
        /// Draw the object at defined position use defined bitmap image
        /// </summary>
        public virtual void DrawBlock()
        {
            this._img.DrawImage(this.Images[_imagestate], new Rectangle(new Point(this.PositionX, this.PositionY), this.BlockSize));
        }

        /// <summary>
        /// Set the object's state to false to let game delete it
        /// </summary>
        /// <returns>return null if already deleted, otherwise false</returns>
        public virtual Explosion Eliminate()
        {
            this.Isalive = false;
            return null;
        }
    }
}

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

using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// Explosion effect when some tank (whether player or enemies) is shot and eliminated
    /// </summary>
    public class Explosion : Block
    {
        /// <summary>
        /// Stay time for explosion effect
        /// </summary>
        private int _alivetime;

        public int AliveTime
        {
            get { return _alivetime; }
            set { _alivetime = value; }
        }

        /// <summary>
        /// Create explosion with defined image, at defined x,y position, with defined size, and defined bitmap path
        /// </summary>
        /// <param name="img">defined bitmap image for object</param>
        /// <param name="position">defined x,y position</param>
        /// <param name="blocksize">defined object size</param>
        /// <param name="path">defined file path</param>
        public Explosion(Graphics img, Point position, Size blocksize, String[] path)
            : base(img, position, blocksize, path)
        {
            this.AliveTime = 3;
        }

        /// <summary>
        /// When stay time down to 0, set the object's state to false, let game delete it
        /// </summary>
        public void StayAlive()
        {
            this.AliveTime--;
            if (this.AliveTime <= 0)
            {
                this.Isalive = false;
            }
        }
    }
}

using System;
using System.Windows.Forms;

namespace TankWar
{
    static class GameEntry
    {
        /// <summary>
        /// Entry of Main Program
        /// </summary>
        /// STAThreadAttribute indicates that the COM threading model for the application is single-threaded apartment. This attribute must be present on the entry point of any application that uses Windows Forms; if it is omitted, the Windows components might not work correctly. If the attribute is not present, the application uses the multithreaded apartment model, which is not supported for Windows Forms.
        [STAThread]
        static void Main()
        {
            /// <summary>
            /// Graphic double buffer process
            /// </summary>
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
        }
    }
}

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

using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// MoveBlock is parent of tanks and it defined direction and move action
    /// </summary>
    public abstract class MoveBlock : Block
    {
        /// <summary>
        /// Move direction
        /// </summary>
        private TowardDirection _direction;

        public TowardDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// Object's move rate on x
        /// </summary>
        private int _xrate;

        public int XRate
        {
            get { return _xrate; }
            set { _xrate = value; }
        }

        /// <summary>
        /// Object's move rate on y
        /// </summary>
        private int _yrate;

        public int YRate
        {
            get { return _yrate; }
            set { _yrate = value; }
        }

        /// <summary>
        /// Create move object with defined x,y position, size, and the move rate
        /// </summary>
        /// <param name="img">bitmap for object</param>
        /// <param name="position">x,y position before</param>
        /// <param name="blocksize">size of block</param>
        /// <param name="path">load file path</param>
        /// <param name="xrate">x move rate</param>
        /// <param name="yrate">y move rate</param>
        public MoveBlock(Graphics img, Point position, Size blocksize, String[] path, int xrate, int yrate)
            : base(img,position, blocksize, path)
        {
            this.XRate = xrate;
            this.YRate = yrate;
        }

        /// <summary>
        /// Move object by x,y rate
        /// </summary>
        public virtual void BlockMove()
        {
            this.PositionX += this.XRate;
            this.PositionY += this.YRate;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using AudioLibrary;
using System.Drawing;
using System.Windows.Forms;

namespace TankWar
{
    /// <summary>
    /// There are four game status, ONGOING: the game is running; GAMEOVER: player's live down to 0; PASSONE: player eleminate all enemies of a scene; PASSALL: player finished all scenes' enemies
    /// </summary>
    public enum GameStatus
    {
        ONGOING, GAMEOVER, PASSONE, PASSALL
    }

    /// <summary>
    /// Scene include the elements of the button in the game, the player and enemies' status, and the actions of the tanks
    /// </summary>
    public class Scene
    {
        GameInterface gameinterf;
        private int _gametime = 0;
        public Image bgmimg;
        public GameStatus gamestatus = GameStatus.ONGOING;
        public int gamescore = 0;
        public int scenenum;
        public int enemytanknum;
        private RectangleF _backtomain = new RectangleF(600, 500, 200, 20);
        private RectangleF _exitgame = new RectangleF(600, 560, 200, 20);
        private RectangleF _capturescreen = new RectangleF(600, 440, 200, 20);
        private RectangleF _helpdocument = new RectangleF(600, 400, 200, 20);
        private int _playernumber;
        private Graphics _img;
        private BattleMap _battlemap;
        AudioByDirectX audirx;
        List<Tanks> tanklist;
        public List<TankPlayer> _tankplayers;
        List<TankShot> tankshots;
        List<TankShot> playershots;
        List<Explosion> explosions;
        List<Bonus> bonus;

        /// <summary>
        /// Overload Scene: when user play from the beginning and scene will intitialize all, include everything in the game, the interface, tanks, status, and the actions between tanks
        /// </summary>
        /// <param name="gmintefce">game interface for user's operation</param>
        /// <param name="playernum">defined the player's number</param>
        /// <param name="img">defined bitmap image</param>
        /// <param name="scenenum">defined the score</param>
        public Scene(GameInterface gmintefce, int playernum, Graphics img, int scenenum)
        {
            this.gameinterf = gmintefce;
            this._playernumber = playernum;
            this.scenenum = scenenum;
            this._img = img;
            this.enemytanknum = scenenum * 3 + 10;
            _battlemap = new BattleMap(this._img);
            _battlemap.ReadBattleMap(Application.StartupPath + "\\Map\\" + scenenum + ".map");
            tanklist = new List<Tanks>();
            _tankplayers = new List<TankPlayer>();
            tankshots = new List<TankShot>();
            playershots = new List<TankShot>();
            explosions = new List<Explosion>();
            bonus = new List<Bonus>();
            if (playernum == 1)
            {
                _tankplayers.Add(new TankPlayer(this._img, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, TowardDirection.UP, playershots, PlayerSymbol.PLAYER1));
            }
            else
            {
                _tankplayers.Add(new TankPlayer(this._img, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, TowardDirection.UP, playershots, PlayerSymbol.PLAYER1));
                _tankplayers.Add(new TankPlayer(this._img, new Point(333, 570), new Size(24, 24), new string[] { "tanks\\tank21.bmp", "tanks\\tank22.bmp", "tanks\\tank23.bmp", "tanks\\tank24.bmp" }, 6, 6, TowardDirection.UP, playershots, PlayerSymbol.PLAYER2));
            }
        }

        /// <summary>
        /// Overload Scene: when user continue to play after passing the scene 1. and scene will load the scene number to choose the map, load the game score stored, load the player number stored, and include everything in the game, the interface, tanks, status, and the actions between tanks
        /// </summary>
        /// <param name="gaminterf"></param>
        /// <param name="playernum"></param>
        /// <param name="img"></param>
        /// <param name="scenenum"></param>
        /// <param name="gamescore"></param>
        /// <param name="playerlifenum"></param>
        public Scene(GameInterface gaminterf, int playernum, Graphics img, int scenenum, int gamescore, List<int> playerlifenum)
        {
            this.gameinterf = gaminterf;
            this._playernumber = playernum;
            this.scenenum = scenenum;
            this._img = img;
            this.enemytanknum = scenenum * 3 + 10;
            _battlemap = new BattleMap(this._img);
            _battlemap.ReadBattleMap(Application.StartupPath + "\\Map\\" + scenenum + ".map");
            tanklist = new List<Tanks>();
            _tankplayers = new List<TankPlayer>();
            tankshots = new List<TankShot>();
            playershots = new List<TankShot>();
            explosions = new List<Explosion>();
            bonus = new List<Bonus>();
            audirx = new AudioByDirectX(Application.StartupPath + "\\Resource\\sounds\\op.mp3");
            audirx.Play();
            if (playernum == 1)
            {
                _tankplayers.Add(new TankPlayer(this._img, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, TowardDirection.UP, playershots, PlayerSymbol.PLAYER1));
            }
            else
            {
                _tankplayers.Add(new TankPlayer(this._img, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, TowardDirection.UP, playershots, PlayerSymbol.PLAYER1));
                _tankplayers.Add(new TankPlayer(this._img, new Point(333, 570), new Size(24, 24), new string[] { "tanks\\tank21.bmp", "tanks\\tank22.bmp", "tanks\\tank23.bmp", "tanks\\tank24.bmp" }, 6, 6, TowardDirection.UP, playershots, PlayerSymbol.PLAYER2));
            }
            this.gamescore = gamescore;
            for (int i = 0; i < playerlifenum.Count; i++)
            {
                _tankplayers[i].Lifenum = playerlifenum[i];
            }
        }

        /// <summary>
        /// When game is ongoing, draw the main screen including all buttons for operation on the right side
        /// </summary>
        public void DrawMainScreen()
        {
            if (gamestatus == GameStatus.ONGOING)
            {
                _img.FillRectangle(Brushes.Black, new Rectangle(0, 0, 600, 600));
                _img.FillRectangle(Brushes.White, new Rectangle(600, 0, 200, 600));
                _img.DrawString("Score:" + gamescore.ToString(), new Font("Times New Roman", 15), Brushes.Red, new RectangleF(600, 20, 200, 20));
                _img.DrawString("Level " + scenenum.ToString()+" Now!", new Font("Times New Roman", 15), Brushes.Red, new RectangleF(600, 50, 200, 20));
                for (int i = 0; i < _tankplayers.Count; i++)
                {
                    _img.DrawString("P" + (i + 1) + "Life:" + _tankplayers[i].Lifenum.ToString(), new Font("Times New Roman", 15), Brushes.Red, new RectangleF(600, 80 + i * 30, 200, 20));
                }
                _img.DrawString("Enemy Left:" + enemytanknum.ToString(), new Font("Times New Roman", 15), Brushes.Red, new RectangleF(600, 140, 200, 20));
                DrawBlock();
            }
            else if(gamestatus == GameStatus.GAMEOVER)
            {
                bgmimg = Image.FromFile(Application.StartupPath + @"\Resource\images\systems\endScreen.bmp");
                _img.DrawImage(bgmimg,0,0,800,600);
            }
            else if(gamestatus == GameStatus.PASSALL)
            {
                bgmimg = Image.FromFile(Application.StartupPath + @"\Resource\images\systems\passScreen.bmp");
                _img.DrawImage(bgmimg,0,0,800,600);
            }
            else if (gamestatus == GameStatus.PASSONE)
            {
            }
            _img.DrawString("Help", new Font("Times New Roman", 15), Brushes.Red, _helpdocument);
            _img.DrawString("Cap", new Font("Times New Roman", 15), Brushes.Red, _capturescreen);
            _img.DrawString("Back To Main", new Font("Times New Roman", 15), Brushes.Red, _backtomain);
            _img.DrawString("Exit", new Font("Times New Roman", 15), Brushes.Red, _exitgame);
        }

        /// <summary>
        /// Control the game state, when pass the scene then return true, when state is ongoing then continue running the game
        /// </summary>
        /// <returns></returns>
        public bool SecnePass()
        {
            if (gamestatus == GameStatus.PASSONE)
            {
                return true;
            }
            else if (gamestatus == GameStatus.ONGOING)
            {
                GameRun();
            }
            else
            {
            }
            return false;
        }

        /// <summary>
        /// Set the enemy creation/number changing and AI fire rate
        /// </summary>
        private void GameRun()
        {
            if (_gametime < 100)
            {
                _gametime++;
                if (_gametime % 20 == 0)
                {
                    EnemyTankAIFire();
                }
            }
            else
            {
                _gametime = 0;
                if (enemytanknum > 0)
                {
                    CreateEnemyTank();
                    enemytanknum--;
                }
            }
            CleanExplosions();
            BlockMove();
            CheckHit();
            EraseDeathTank();
            CheckGameStatus();
        }

        /// <summary>
        /// Remove the explosion effect after set time
        /// </summary>
        private void CleanExplosions()
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].StayAlive();
                if (!explosions[i].Isalive)
                {
                    explosions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Random create differnt type of enemy tanks
        /// </summary>
        private void CreateEnemyTank()
        {
            //r is lable number of tank bmp
            Random rdnum = new Random();
            int labelnum = rdnum.Next(1, 9);
            if (labelnum % 2 == 0)
            {
                tanklist.Add(new Tanks(_img, new Point(1, 1), new Size(24, 24), new string[] { "tanks\\eptank" + labelnum + "1.bmp", "tanks\\eptank" + labelnum + "2.bmp", "tanks\\eptank" + labelnum + "3.bmp", "tanks\\eptank" + labelnum + "4.bmp" }, 6, 6, TowardDirection.DOWN, tankshots));
            }
            else
            {
                tanklist.Add(new Tanks(_img, new Point(575, 1), new Size(24, 24), new string[] { "tanks\\eptank" + labelnum + "1.bmp", "tanks\\eptank" + labelnum + "2.bmp", "tanks\\eptank" + labelnum + "3.bmp", "tanks\\eptank" + labelnum + "4.bmp" }, 6, 6, TowardDirection.DOWN, tankshots));
            }
        }

        /// <summary>
        /// Draw all objects on the battle map
        /// </summary>
        private void DrawBlock()
        {
            for (int i = 0; i < tanklist.Count; i++)
            {
                tanklist[i].DrawBlock();
            }
            for (int i = 0; i < _tankplayers.Count; i++)
            {
                _tankplayers[i].DrawBlock();
            }
            for (int i = 0; i < _battlemap.Elements.Count; i++)
            {
                _battlemap.Elements[i].DrawBlock();
            }
            for (int i = 0; i < tankshots.Count; i++)
            {
                tankshots[i].DrawBlock();
            }
            for (int i = 0; i < playershots.Count; i++)
            {
                playershots[i].DrawBlock();
            }
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].DrawBlock();
            }
            for (int i = 0; i < bonus.Count; i++)
            {
                bonus[i].DrawBlock();
            }
        }

        /// <summary>
        /// Check collision between tank shots and tanks
        /// </summary>
        private void CheckHit() 
        {
            try
            {
                for (int i = _battlemap.Elements.Count - 1; i >= 0; i--)
                {
                    for (int j = playershots.Count - 1; j >= 0; j--)
                    {
                        if (_battlemap.Elements[i].GetRectangleBlock().IntersectsWith(playershots[j].GetRectangleBlock()))
                        {
                            if (_battlemap.Elements[i].ElementType == ElementType.STEEL)
                            {
                                SoundPlayer splyr = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\tie.wav");
                                splyr.Play();
                                playershots.RemoveAt(j);
                                break;
                            }
                            else if (_battlemap.Elements[i].ElementType == ElementType.BASE)
                            {
                                SoundPlayer splyr = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\bob.wav");
                                splyr.Play();
                                _battlemap.Elements.RemoveAt(i);
                                gamestatus = GameStatus.GAMEOVER;
                                playershots.RemoveAt(j);
                                break;
                            }
                            else if (_battlemap.Elements[i].ElementType == ElementType.BRICK)
                            {
                                SoundPlayer splyr = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\wall.wav");
                                splyr.Play();
                                _battlemap.Elements.RemoveAt(i);
                                playershots.RemoveAt(j);
                                break;
                            }
                        }
                    }

                    for (int j = tankshots.Count - 1; j >= 0; j--)
                    {
                        if (_battlemap.Elements[i].GetRectangleBlock().IntersectsWith(tankshots[j].GetRectangleBlock()))
                        {
                            if (_battlemap.Elements[i].ElementType == ElementType.STEEL)
                            {
                                SoundPlayer splyr = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\tie.wav");
                                splyr.Play();
                                tankshots.RemoveAt(j);
                                break;
                            }
                            else if (_battlemap.Elements[i].ElementType == ElementType.BASE)
                            {
                                SoundPlayer splyr = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\bob.wav");
                                splyr.Play();
                                _battlemap.Elements.RemoveAt(i);
                                gamestatus = GameStatus.GAMEOVER;
                                tankshots.RemoveAt(j);
                                break;
                            }
                            else if (_battlemap.Elements[i].ElementType == ElementType.BRICK)
                            {
                                SoundPlayer splyr = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\wall.wav");
                                splyr.Play();
                                _battlemap.Elements.RemoveAt(i);
                                tankshots.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }
            finally { }

            for (int i = tanklist.Count - 1; i >= 0; i--)
            {
                for (int j = playershots.Count - 1; j >= 0; j--)
                {
                    if (tanklist[i].GetRectangleBlock().IntersectsWith(playershots[j].GetRectangleBlock()))
                    {
                        playershots.RemoveAt(j);
                        Random rdnm = new Random();
                        int r = rdnm.Next(0,20);
                        Rectangle nrct1 = tanklist[i].GetRectangleBlock();
                        if (r >= 10 && r < 17)
                        {
                            bonus.Add(new Bonus(this._img, nrct1.Location, nrct1.Size, new string[] { "others\\propBomb.bmp" }, BonusType.KILLALL));
                        }
                        else if (r >= 17)
                        {
                            bonus.Add(new Bonus(this._img, nrct1.Location, nrct1.Size, new string[] { "others\\propTank.bmp" }, BonusType.ADDLIFE));
                        }
                        
                        explosions.Add(tanklist[i].Eliminate());
                    }
                }
            }

            for (int i = _tankplayers.Count - 1; i >= 0; i--)
            {
                for (int j = tankshots.Count - 1; j >= 0; j--)
                {
                    if (_tankplayers[i].GetRectangleBlock().IntersectsWith(tankshots[j].GetRectangleBlock()))
                    {
                        tankshots.RemoveAt(j);
                        explosions.Add(_tankplayers[i].Eliminate());
                    }
                }

                for (int j = bonus.Count - 1; j >= 0; j--)
                {
                    if (_tankplayers[i].GetRectangleBlock().IntersectsWith(bonus[j].GetRectangleBlock()))
                    {
                        if (bonus[j].BonusType == BonusType.KILLALL)
                        {
                            for (int k = 0; k < tanklist.Count; k++)
                            {
                                explosions.Add(tanklist[k].Eliminate());
                            }
                        }
                        else if (bonus[j].BonusType == BonusType.ADDLIFE)
                        {
                            _tankplayers[i].Lifenum++;
                        }
                        bonus.RemoveAt(j);
                    }
                }
            }
        }

        /// <summary>
        /// Eliminate the tanks that has been shot
        /// </summary>
        private void EraseDeathTank()
        {
            for (int i = tanklist.Count - 1; i >= 0; i--)
            {
                if (!tanklist[i].Isalive)
                {
                    gamescore += 10;
                    tanklist.RemoveAt(i);
                }
            }
            for (int i = _tankplayers.Count - 1; i >= 0; i--)
            {
                if (!_tankplayers[i].Isalive)
                {
                    _tankplayers.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Switch the game state according to the player's count, enemies' count, and scene's number
        /// </summary>
        private void CheckGameStatus()
        {
            if (_tankplayers.Count == 0)
            {
                gamestatus = GameStatus.GAMEOVER;
            }
            if (tanklist.Count == 0 && enemytanknum == 0)
            {
                gamestatus = GameStatus.PASSONE;
                if (scenenum == 30)
                {
                    gamestatus = GameStatus.PASSALL;
                }
            }
        }

        /// <summary>
        /// Defined the status of bonus and move pattern of player, enemy and tank shots
        /// </summary>
        private void BlockMove()
        {
            for (int i = 0; i < _tankplayers.Count; i++)
            {
                Rectangle playerblock = _tankplayers[i].MoveTest();
                bool canmove = true;
                for (int j = 0; j < _battlemap.Elements.Count; j++)
                {
                    if (playerblock.IntersectsWith(_battlemap.Elements[j].GetRectangleBlock()))
                    {
                        if (_battlemap.Elements[j].ElementType != ElementType.BUSH)
                        {
                            canmove = canmove && false;
                        }

                        else
                        {
                            canmove = canmove && true;
                        }
                    }
                }

                for (int j = 0; j < tanklist.Count; j++)
                {
                    if (playerblock.IntersectsWith(tanklist[j].GetRectangleBlock()))
                    {
                        canmove = canmove & false;
                    }
                }

                if (_tankplayers[i].Tx > 0 && _tankplayers[i].Tx < 576 && _tankplayers[i].Ty > 0 && _tankplayers[i].Ty < 576)
                {
                    canmove = canmove && true;
                }
                else
                {
                    canmove = canmove && false;
                }
                if (canmove)
                {
                    _tankplayers[i].BlockMove();
                }
                else
                {
                    _tankplayers[i].UndoMove();
                }
            }

            EnemyTankAIMove();

            for (int i = playershots.Count - 1; i >= 0; i--)
            {
                playershots[i].BlockMove();
                if (playershots[i].PositionX <= 0 || playershots[i].PositionX >= 600 || playershots[i].PositionY <= 0 || playershots[i].PositionY >= 600)
                {
                    playershots.RemoveAt(i);
                }
            }
            for (int i = tankshots.Count - 1; i >= 0; i--)
            {
                tankshots[i].BlockMove();
                if (tankshots[i].PositionX <= 0 || tankshots[i].PositionX >= 600 || tankshots[i].PositionY <= 0 || tankshots[i].PositionY >= 600)
                {
                    tankshots.RemoveAt(i);
                }
            }

            for (int i = bonus.Count - 1; i >= 0; i--)
            {
                bonus[i].StayAlive();
                if (!bonus[i].Isalive)
                {
                    bonus.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Define the enemy move pattern in detail
        /// </summary>
        private void EnemyTankAIMove()
        {
            for (int i = 0; i < tanklist.Count; i++)
            {
                Rectangle enemytankblock = tanklist[i].MoveTest();
                bool canmove = true;
                for (int j = 0; j < _battlemap.Elements.Count; j++)
                {
                    if (enemytankblock.IntersectsWith(_battlemap.Elements[j].GetRectangleBlock()))
                    {
                        if (_battlemap.Elements[j].ElementType != ElementType.BUSH)
                        {
                            canmove = canmove && false;
                        }
                        else
                        {
                            canmove = canmove && true;
                        }
                    }

                }

                for (int j = 0; j < _tankplayers.Count; j++)
                {
                    if (enemytankblock.IntersectsWith(_tankplayers[j].GetRectangleBlock()))
                    {
                        canmove = canmove & false;
                    }
                }

                if (tanklist[i].Tx > 0 && tanklist[i].Tx < 576 && tanklist[i].Ty > 0 && tanklist[i].Ty < 576)
                {
                    canmove = canmove && true;
                }
                else
                {
                    canmove = canmove && false;
                }
                if (canmove)
                {
                    tanklist[i].BlockMove();
                }
                else
                {
                    tanklist[i].UndoMove();
                    tanklist[i].AIRoundDirection();
                }
            }
        }

        /// <summary>
        /// Define the enemy AI fire 
        /// </summary>
        private void EnemyTankAIFire()
        {
            for (int i = 0; i < tanklist.Count; i++)
            {
                tanklist[i].Fire();
            }
        }

        /// <summary>
        /// Check the mouse locate in the different buttons' fields
        /// </summary>
        /// <param name="e"></param>
        public void MoveMouse(MouseEventArgs e)
        {
            if (e.X >= _backtomain.X && e.X <= (_backtomain.X + _backtomain.Width) && e.Y >= _backtomain.Y && e.Y <= (_backtomain.Y + _backtomain.Height))
            {
                gameinterf.Cursor = Cursors.Hand;
            }
            else if (e.X >= _exitgame.X && e.X <= (_exitgame.X + _exitgame.Width) && e.Y >= _exitgame.Y && e.Y <= (_exitgame.Y + _exitgame.Height))
            {
                gameinterf.Cursor = Cursors.Hand;
            }
            else if (e.X >= _helpdocument.X && e.X <= (_helpdocument.X + _helpdocument.Width) && e.Y >= _helpdocument.Y && e.Y <= (_helpdocument.Y + _helpdocument.Height))
            {
                gameinterf.Cursor = Cursors.Hand;
            }
            else if (e.X >= _capturescreen.X && e.X <= (_capturescreen.X + _capturescreen.Width) && e.Y >= _capturescreen.Y && e.Y <= (_capturescreen.Y + _capturescreen.Height))
            {
                gameinterf.Cursor = Cursors.Hand;
            }
            else
            {
                gameinterf.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Defined the mouse click actions/results on different buttons
        /// </summary>
        /// <param name="e"></param>
        public void ClickMouse(MouseEventArgs e)
        {
            if (e.X >= _backtomain.X && e.X <= (_backtomain.X + _backtomain.Width) && e.Y >= _backtomain.Y && e.Y <= (_backtomain.Y + _backtomain.Height))
            {
                gameinterf.statf.Visible = true;
                gameinterf.CloseGame();
                gameinterf.Dispose();
            }
            else if (e.X >= _exitgame.X && e.X <= (_exitgame.X + _exitgame.Width) && e.Y >= _exitgame.Y && e.Y <= (_exitgame.Y + _exitgame.Height))
            {
                gameinterf.Close();
            }
            else if (e.X >= _helpdocument.X && e.X <= (_helpdocument.X + _helpdocument.Width) && e.Y >= _helpdocument.Y && e.Y <= (_helpdocument.Y + _helpdocument.Height))
            {
                Help.ShowHelp(gameinterf, Application.StartupPath + "\\Help.chm");
            }
            else if (e.X >= _capturescreen.X && e.X <= (_capturescreen.X + _capturescreen.Width) && e.Y >= _capturescreen.Y && e.Y <= (_capturescreen.Y + _capturescreen.Height))
            {
                int i = 1;
                while (File.Exists(Application.StartupPath + "\\cap\\" + "GameCap" + i + ".jpg"))
                {
                    i++;
                }
                gameinterf.bufferimg.Save(Application.StartupPath + "\\cap\\" + "GameCap" + i + ".jpg");
                MessageBox.Show("Save captured image to " + Application.StartupPath + "\\cap\\" + "GameCap" + i + ".jpg", "Save Successs!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Check the key up (true) states of players
        /// </summary>
        /// <param name="e"></param>
        public void PlayerKeyUp(KeyEventArgs e)
        {
            for (int i = 0; i < _tankplayers.Count; i++)
            {
                _tankplayers[i].KeyUp(e);
            }
        }

        /// <summary>
        /// Check the key down (false) states of players
        /// </summary>
        /// <param name="e"></param>
        public void PlayerKeyDown(KeyEventArgs e)
        {
            for (int i = 0; i < _tankplayers.Count; i++)
            {
                _tankplayers[i].KeyDown(e);
            }
        }
    }
}

using System;
using System.Windows.Forms;
using System.Diagnostics;
using AudioLibrary;

namespace TankWar
{
    /// <summary>
    /// Form permit choosing start or load (still working for the load function)
    /// </summary>
    public enum StartOrLoad
    {
        START,LOAD
    }

    /// <summary>
    /// Main start interface of the game
    /// </summary>
    public partial class StartForm : Form
    {
        private int playercount = 1;

        public StartForm()
        {
            InitializeComponent();
            this.BackgroundImage = System.Drawing.Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\Resource\images\systems\tank.jpg");
        }

        /// <summary>
        /// Load bgm for the start form
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void StartForm_Load(object sender, EventArgs e)
        {
            AudioByAPI.OpenAndPlay(Application.StartupPath + "\\Resource\\sounds\\sop.mp3", true);
        }

        /// <summary>
        /// Click start and the game start
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void label1_Click(object sender, EventArgs e)
        {
            GameInterface gf = new GameInterface(this, StartOrLoad.START, playercount);
            this.Visible = false;
            AudioByAPI.Stop();
            gf.Show();
        }

        /// <summary>
        /// Click exit and you exit
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// You can choose 1 player and two players
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                playercount = 1;
            }
            else if (radioButton2.Checked)
            {
                playercount = 2;
            }
        }

        /// <summary>
        /// When form is visible then play the bgm
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void StartForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                AudioByAPI.OpenAndPlay(Application.StartupPath + "\\Resource\\sounds\\sop.mp3", true);
            }
        }

        /// <summary>
        /// When click the help and help document shows up
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void label3_Click_1(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + "\\Help.chm");
        }

        /// <summary>
        /// When click the about, and about author's information showsup
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void label4_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        /// <summary>
        /// When click the map editor, then start the map editor
        /// </summary>
        /// <param name="sender">form element</param>
        /// <param name="e">event</param>
        private void label5_Click(object sender, EventArgs e)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = Application.StartupPath + "\\WorldEditor.exe";
            ps.Start();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TankWar
{
    /// <summary>
    /// There are three status of tanks, INVINCIBLE: can not be eliminated (when obtain bonus, still working on it); DEATH: been shot and eliminated; ORDINARY: normal state, stay alive
    /// </summary>
    public enum TankStatus
    {
        INVINCIBLE, DEATH, ORDINARY
    }

    /// <summary>
    /// Four directions for the tanks' movement, up, down , left, right
    /// </summary>
    public enum TowardDirection
    {
        UP, DOWN, LEFT, RIGHT,
    }

    /// <summary>
    /// Players' labels to define player 1 or 2
    /// </summary>
    public enum PlayerSymbol
    {
        PLAYER1, PLAYER2
    }

    /// <summary>
    /// Create player tank which is child of Tanks (enemy), defined move direction, Graphics to draw, x,y location, block size image url, x moving speed, y moving speed, tank bullets, player's symbol (player 1 or 2)
    /// </summary>
    public class TankPlayer : Tanks
    {
        private PlayerSymbol _playersymbol;

        public PlayerSymbol PlayerSymbol
        {
            get { return _playersymbol; }
        }

        private bool _ismoveup;

        public bool IsMoveUp
        {
            get { return _ismoveup; }
            set { _ismoveup = value; }
        }

        private bool _ismovedown;

        public bool IsMoveDown
        {
            get { return _ismovedown; }
            set { _ismovedown = value; }
        }

        private bool _ismoveleft;

        public bool IsMoveLeft
        {
            get { return _ismoveleft; }
            set { _ismoveleft = value; }
        }

        private bool _ismoveright;

        public bool IsMoveRight
        {
            get { return _ismoveright; }
            set { _ismoveright = value; }
        }

        private int lifenum;

        public int Lifenum
        {
            get { return lifenum; }
            set { lifenum = value; }
        }

        private int ox;
        private int oy;

        /// <summary>
        /// Constructor defined move direction, Graphics to draw, x,y location, block size image url, x moving speed, y moving speed, tank bullets, player's symbol (player 1 or 2)
        /// </summary>
        /// <param name="g">graphic object for drawing</param>
        /// <param name="location">x,y location</param>
        /// <param name="size">block size</param>
        /// <param name="imageurl">image path</param>
        /// <param name="xspeed">x moving rate</param>
        /// <param name="yspeed">y moving rate</param>
        /// <param name="tankdirection">direction of player tank</param>
        /// <param name="bullets">bullet of player tank</param>
        /// <param name="playerflag">symbol of player tank (playwe 1 or 2)</param>
        public TankPlayer(Graphics g, Point location, Size size, String[] imageurl, int xspeed, int yspeed, TowardDirection tankdirection, List<TankShot> bullets, PlayerSymbol playerflag)
            : base(g, location, size, imageurl, xspeed, yspeed, tankdirection, bullets)
        {
            this._playersymbol = playerflag;
            this.Lifenum = 3;
            ox = location.X;
            oy = location.Y;
        }

        /// <summary>
        /// Move the block to new x,y location
        /// </summary>
        public override void BlockMove()
        {
            this.PositionX = this.Tx;
            this.PositionY = this.Ty;
        }

        /// <summary>
        /// When player key down, activate the direction move
        /// </summary>
        /// <param name="e">key event</param>
        public void KeyDown(KeyEventArgs e)
        {
            if (this.PlayerSymbol == PlayerSymbol.PLAYER1)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.IsMoveLeft = true;
                    this.Tankdirection = TowardDirection.LEFT;
                }
                else if (e.KeyCode == Keys.D)
                {
                    this.IsMoveRight = true;
                    this.Tankdirection = TowardDirection.RIGHT;
                }
                else if (e.KeyCode == Keys.W)
                {
                    this.IsMoveUp = true;
                    this.Tankdirection = TowardDirection.UP;
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.IsMoveDown = true;
                    this.Tankdirection = TowardDirection.DOWN;
                }
            }
            else if (this.PlayerSymbol == PlayerSymbol.PLAYER2)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.IsMoveLeft = true;
                    this.Tankdirection = TowardDirection.LEFT;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.IsMoveRight = true;
                    this.Tankdirection = TowardDirection.RIGHT;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.IsMoveUp = true;
                    this.Tankdirection = TowardDirection.UP;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.IsMoveDown = true;
                    this.Tankdirection = TowardDirection.DOWN;
                }
            }
        }

        /// <summary>
        /// When player key up, de-activate the direction move
        /// </summary>
        /// <param name="e">key event</param>
        public void KeyUp(KeyEventArgs e)
        {
            if (this.PlayerSymbol == PlayerSymbol.PLAYER1)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.IsMoveLeft = false;
                }
                else if (e.KeyCode == Keys.D)
                {
                    this.IsMoveRight = false;
                }
                else if (e.KeyCode == Keys.W)
                {
                    this.IsMoveUp = false;
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.IsMoveDown = false;
                }
                else if (e.KeyCode == Keys.J)
                {
                    this.Fire();
                }
            }
            else if (this.PlayerSymbol == PlayerSymbol.PLAYER2)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.IsMoveLeft = false;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.IsMoveRight = false;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.IsMoveUp = false;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.IsMoveDown = false;
                }
                else if (e.KeyCode == Keys.B)
                {
                    this.Fire();
                }
            }
        }

        /// <summary>
        /// Move toward 4 directions, reset the x,y position
        /// </summary>
        /// <returns>new rect block with x,y position and size</returns>
        public override Rectangle MoveTest()
        {
            if (this.IsMoveLeft)
            {
                this.Tx -= this.XRate;
            }
            if (this.IsMoveRight)
            {
                this.Tx += this.XRate;
            }
            if (this.IsMoveUp)
            {
                this.Ty -= this.YRate;
            }
            if (this.IsMoveDown)
            {
                this.Ty += this.YRate;
            }
            return new Rectangle(new Point(this.Tx, this.Ty), this.BlockSize);
        }

        /// <summary>
        /// When player is eliminated by enemies, decrease the life number by 1, set the states to false and let game eliminate itself, then return an explosion effect at its position, and play sound effect
        /// </summary>
        /// <returns>explosion effect with x,y position and size</returns>
        public override Explosion Eliminate()
        {
            int lx = this.PositionX;
            int ly = this.PositionY;
            this.Lifenum--;
            if (this.Lifenum < 0)
            {
                this.Isalive = false;
            }
            else
            {
                this.PositionX = this.ox;
                this.PositionY = this.oy;
                this.Tankdirection = TowardDirection.UP;
                UndoMove();
            }
            this.sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
            this.sp.Play();
            return new Explosion(this.Img, new Point(lx, ly), this.BlockSize, new string[] { "others\\bomb.bmp" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace TankWar
{
    /// <summary>
    /// Tanks is for enemies, moreover it is the player tank's parent.
    /// </summary>
    public class Tanks : MoveBlock
    {
        private int step = 0;
        public SoundPlayer sp;
        private TankStatus tankstate;
        public TankStatus Tankstate
        {
            get { return tankstate; }
            set { tankstate = value; }
        }

        private TowardDirection _tankdirection;
        public TowardDirection Tankdirection
        {
            get { return _tankdirection; }
            set { _tankdirection = value; }
        }

        public List<TankShot> bullets;

        /// <summary>
        /// Create enemy tank with bullets, bitmap image, location, size, imageurl, speed and direction
        /// </summary>
        /// <param name="g">graphic to draw bitmap</param>
        /// <param name="location">x,y position</param>
        /// <param name="size">block size</param>
        /// <param name="imageurl">path for the bitmap</param>
        /// <param name="xspeed">x move rate</param>
        /// <param name="yspeed">y move rate</param>
        /// <param name="tankdirection">direction type</param>
        /// <param name="bullets">tank shot</param>
        public Tanks(Graphics g, Point location, Size size, String[] imageurl, int xspeed, int yspeed, TowardDirection tankdirection, List<TankShot> bullets)
            : base(g,location, size, imageurl, xspeed, yspeed)
        {
            this.Tankdirection = tankdirection;
            this.bullets = bullets;
            this.Tankstate = TankStatus.ORDINARY;
            sp = new SoundPlayer();
            UndoMove();
        }

        private int tx;

        public int Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        private int ty;

        public int Ty
        {
            get { return ty; }
            set { ty = value; }
        }

        /// <summary>
        /// When enemy fires, the shot will move by different directions, and they will be drawn on the screen, with sound effect
        /// </summary>
        public void Fire()
        {
            int bxspeed = 0;
            int byspeed = 0;
            if (this.Tankdirection == TowardDirection.UP)
            {
                bxspeed = 0;
                byspeed = -20;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                bxspeed = 0;
                byspeed = 20;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                bxspeed = -20;
                byspeed = 0;
            }
            else if (this.Tankdirection == TowardDirection.RIGHT)
            {
                bxspeed = 20;
                byspeed = 0;
            }
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\DropCell.wav";
            sp.Play();
            this.bullets.Add(new TankShot(this.Img, new Point(this.PositionX + (this.BlockSize.Width - 10) / 2, this.PositionY + (this.BlockSize.Height - 10) / 2), new Size(5, 5), new string[] { "others\\bullet.bmp" }, bxspeed, byspeed));
        }

        /// <summary>
        /// When enemy's state is not alive then eliminated it, return a explosion effect, and play sound effect
        /// </summary>
        /// <returns>explosion effect object</returns>
        public override Explosion Eliminate()
        {
            this.Isalive = false;
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
            sp.Play();
            return new Explosion(this.Img, new Point(this.PositionX, this.PositionY), this.BlockSize, new string[] { "others\\bomb.bmp" });
        }

        /// <summary>
        /// Draw enemy tank by different directions
        /// </summary>
        public override void DrawBlock()
        {
            if (this.Tankdirection == TowardDirection.UP)
            {
                this.Imagestate = 0;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                this.Imagestate = 2;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                this.Imagestate = 3;
            }
            else if (this.Tankdirection == TowardDirection.RIGHT)
            {
                this.Imagestate = 1;
            }
            base.DrawBlock();
        }

        /// <summary>
        /// Enemy Tank's AI move pattern
        /// </summary>
        public override void BlockMove()
        {
            this.PositionX = this.Tx;
            this.PositionY = this.Ty;
            step++;
            Random rd = new Random();
            int r = rd.Next(0, 2);
            if (step == 15)
            {
                if (r == 0)
                {
                    AIRoundDirection1();
                }
                else
                {
                    AIRoundDirection();
                }
                step = 0;
            }
        }

        /// <summary>
        /// Update the position of block by different directions
        /// </summary>
        /// <returns>new Block with size and position</returns>
        public virtual Rectangle MoveTest()
        {
            switch (this.Tankdirection)
            {
                case TowardDirection.UP:
                    this.Ty -= this.YRate;
                    break;
                case TowardDirection.DOWN:
                    this.Ty += this.YRate;
                    break;
                case TowardDirection.LEFT:
                    this.Tx -= this.XRate;
                    break;
                case TowardDirection.RIGHT:
                    this.Tx += this.XRate;
                    break;
            }
            return new Rectangle(new Point(this.Tx, this.Ty), this.BlockSize);
        }

        /// <summary>
        /// Transfer the block postion to itself
        /// </summary>
        public void UndoMove()
        {
            this.Tx = this.PositionX;
            this.Ty = this.PositionY;
        }

        /// <summary>
        /// AI direction control scheme 1
        /// </summary>
        public void AIRoundDirection()
        {
            if (this.Tankdirection == TowardDirection.RIGHT)
            {
                this.Tankdirection = TowardDirection.DOWN;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                this.Tankdirection = TowardDirection.LEFT;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                this.Tankdirection = TowardDirection.UP;
            }
            else
            {
                this.Tankdirection = TowardDirection.RIGHT;
            }
        }

        /// <summary>
        /// AI direction control scheme 2
        /// </summary>
        public void AIRoundDirection1()
        {
            if (this.Tankdirection == TowardDirection.RIGHT)
            {
                this.Tankdirection = TowardDirection.UP;
            }
            else if (this.Tankdirection == TowardDirection.DOWN)
            {
                this.Tankdirection = TowardDirection.RIGHT;
            }
            else if (this.Tankdirection == TowardDirection.LEFT)
            {
                this.Tankdirection = TowardDirection.DOWN;
            }
            else
            {
                this.Tankdirection = TowardDirection.LEFT;
            }
        }
    }
}

using System;
using System.Drawing;

namespace TankWar
{
    /// <summary>
    /// Create tank shot/bullets with bitmap, x,y location, size, image path, and x,y moving rate
    /// </summary>
    public class TankShot : MoveBlock
    {
        /// <summary>
        /// Create tank shot/bullets with bitmap, x,y location, size, image path, and x,y moving rate
        /// </summary>
        public TankShot(Graphics g,Point location, Size size, String[] imageurl, int xspeed, int yspeed)
            : base(g,location, size, imageurl, xspeed, yspeed)
        {
 
        }

        public override void BlockMove()
        {
            base.BlockMove();
        }
    }
}

using System;
using System.Windows.Forms;

namespace WorldEditor
{
    static class Program
    {
        /// <summary>
        /// Entry of Main Program
        /// </summary>
        /// STAThreadAttribute indicates that the COM threading model for the application is single-threaded apartment. This attribute must be present on the entry point of any application that uses Windows Forms; if it is omitted, the Windows components might not work correctly. If the attribute is not present, the application uses the multithreaded apartment model, which is not supported for Windows Forms.
        [STAThread]
        static void Main()
        {
            /// <summary>
            /// Graphic double buffer process
            /// </summary>
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

using System;
using System.Windows.Forms;

namespace WorldEditor
{
    /// <summary>
    /// About the map editor which helps the user
    /// </summary>
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

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
