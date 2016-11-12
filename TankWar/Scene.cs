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
