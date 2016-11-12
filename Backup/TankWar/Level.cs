using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Media;
using AudioLibrary;

namespace TankWar
{
    public enum GameState   //游戏的运行状态
    {
        RUN,OVER,PASS,PASSANDEND
    }
    //[Serializable]
    public class Level      //关卡类
    {
        GameForm gf;
        private int gamestep = 0;
        public Image background;
        public GameState gamestate = GameState.RUN;
        public int score = 0;
        public int levelnum;
        //public int lifenum;
        public int eptank_count;
        private RectangleF backtitle = new RectangleF(600, 500, 200, 20);
        //private RectangleF save = new RectangleF(600, 530, 200, 20);
        private RectangleF exit = new RectangleF(600, 560, 200, 20);
        private RectangleF cap = new RectangleF(600, 440, 200, 20);
        private RectangleF help = new RectangleF(600, 400, 200, 20);
        private int playercount;
        private Graphics g;
        /******************测试********************/
        //private PlayerTank pt;
        //private List<Bullet> bullets;
        private Map map;
        //private SoundPlayer sp;
        AudioByDirectX abd;

        /******************游戏元素集合****************/

        List<Tank> tanks;
        public List<PlayerTank> playertanks;
        List<Bullet> tbullets;
        List<Bullet> pbullets;
        List<Bomb> bombs;
        List<Prize> prizes;

        public Level(GameForm gf, int playercount, Graphics g, int levelnum)
        {
            this.gf = gf;
            this.playercount = playercount;
            this.levelnum = levelnum;
            this.g = g;
            this.eptank_count = levelnum * 3 + 10;
            //this.eptank_count = levelnum * 3;
            //bullets = new List<Bullet>();
            //pt = new PlayerTank(g, new Point(500, 400), new Size(40, 40), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 10, 10, Direction.UP, bullets, PlayerFlag.PLAYER1);
            map = new Map(this.g);
            map.ReadMap(Application.StartupPath + "\\Map\\" + levelnum + ".map");

            tanks = new List<Tank>();
            playertanks = new List<PlayerTank>();
            tbullets = new List<Bullet>();
            pbullets = new List<Bullet>();
            bombs = new List<Bomb>();
            prizes = new List<Prize>();
            /*sp = new SoundPlayer();
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\prop.wav";
            sp.Play();*/
            //AudioByAPI.OpenAndPlay(Application.StartupPath + "\\Resource\\sounds\\op.mp3", true);
            abd = new AudioByDirectX(Application.StartupPath + "\\Resource\\sounds\\op.mp3");
            abd.Play();
            if (playercount == 1)
            {
                playertanks.Add(new PlayerTank(this.g, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, Direction.UP, pbullets, PlayerFlag.PLAYER1));
            }
            else
            {
                playertanks.Add(new PlayerTank(this.g, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, Direction.UP, pbullets, PlayerFlag.PLAYER1));
                playertanks.Add(new PlayerTank(this.g, new Point(333, 570), new Size(24, 24), new string[] { "tanks\\tank21.bmp", "tanks\\tank22.bmp", "tanks\\tank23.bmp", "tanks\\tank24.bmp" }, 6, 6, Direction.UP, pbullets, PlayerFlag.PLAYER2));
            }
        }

        public Level(GameForm gf, int playercount, Graphics g, int levelnum, int score, List<int> plifenum)
        {
            this.gf = gf;
            this.playercount = playercount;
            this.levelnum = levelnum;
            this.g = g;
            this.eptank_count = levelnum * 3 + 10;
            //this.eptank_count = levelnum * 3;
            //bullets = new List<Bullet>();
            //pt = new PlayerTank(g, new Point(500, 400), new Size(40, 40), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 10, 10, Direction.UP, bullets, PlayerFlag.PLAYER1);
            map = new Map(this.g);
            map.ReadMap(Application.StartupPath + "\\Map\\" + levelnum + ".map");

            tanks = new List<Tank>();
            playertanks = new List<PlayerTank>();
            tbullets = new List<Bullet>();
            pbullets = new List<Bullet>();
            bombs = new List<Bomb>();
            prizes = new List<Prize>();
            /*sp = new SoundPlayer();
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\prop.wav";
            sp.Play();*/
            //AudioByAPI.OpenAndPlay(Application.StartupPath + "\\Resource\\sounds\\op.mp3", true);
            abd = new AudioByDirectX(Application.StartupPath + "\\Resource\\sounds\\op.mp3");
            abd.Play();
            if (playercount == 1)
            {
                playertanks.Add(new PlayerTank(this.g, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, Direction.UP, pbullets, PlayerFlag.PLAYER1));
            }
            else
            {
                playertanks.Add(new PlayerTank(this.g, new Point(213, 570), new Size(24, 24), new string[] { "tanks\\tank11.bmp", "tanks\\tank12.bmp", "tanks\\tank13.bmp", "tanks\\tank14.bmp" }, 6, 6, Direction.UP, pbullets, PlayerFlag.PLAYER1));
                playertanks.Add(new PlayerTank(this.g, new Point(333, 570), new Size(24, 24), new string[] { "tanks\\tank21.bmp", "tanks\\tank22.bmp", "tanks\\tank23.bmp", "tanks\\tank24.bmp" }, 6, 6, Direction.UP, pbullets, PlayerFlag.PLAYER2));
            }
            this.score = score;
            for (int i = 0; i < plifenum.Count; i++)
            {
                playertanks[i].Lifenum = plifenum[i];
            }
        }

        /*public Level(GameForm gf, int score, int levelnum, int lifenum, int eptank_count)
        {
            this.gf = gf;
        }*/

        public void DrawScreen()
        {
            if (gamestate == GameState.RUN)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 600, 600));
                g.FillRectangle(Brushes.White, new Rectangle(600, 0, 200, 600));
                g.DrawString("得分:" + score.ToString(), new Font("黑体", 15), Brushes.Red, new RectangleF(600, 20, 200, 20));
                g.DrawString("第 " + levelnum.ToString()+" 关", new Font("黑体", 15), Brushes.Red, new RectangleF(600, 50, 200, 20));
                //g.DrawString("剩余生命:" + lifenum.ToString(), new Font("黑体", 15), Brushes.Red, new RectangleF(600, 80, 200, 20));
                for (int i = 0; i < playertanks.Count; i++)
                {
                    g.DrawString("P" + (i + 1) + "剩余生命:" + playertanks[i].Lifenum.ToString(), new Font("黑体", 15), Brushes.Red, new RectangleF(600, 80 + i * 30, 200, 20));
                }
                g.DrawString("剩余敌人:" + eptank_count.ToString(), new Font("黑体", 15), Brushes.Red, new RectangleF(600, 140, 200, 20));
                //GameRun();
                DrawGame();
            }
            else if(gamestate == GameState.OVER)
            {
                //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\tongguan.wav";
                //sp.Play();
                background = Image.FromFile(Application.StartupPath + @"\Resource\images\systems\endScreen.bmp");
                g.DrawImage(background,0,0,800,600);
            }
            else if(gamestate == GameState.PASSANDEND)
            {
                //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\tongguan.wav";
                //sp.Play();
                background = Image.FromFile(Application.StartupPath + @"\Resource\images\systems\passScreen.bmp");
                g.DrawImage(background,0,0,800,600);
            }
            else if (gamestate == GameState.PASS)
            {
                //return true;
            }
            g.DrawString("游戏帮助", new Font("黑体", 15), Brushes.Red, help);
            g.DrawString("截屏", new Font("黑体", 15), Brushes.Red, cap);
            g.DrawString("返回标题", new Font("黑体", 15), Brushes.Red, backtitle);
            //g.DrawString("保存游戏", new Font("黑体", 15), Brushes.Red, save);
            g.DrawString("退出游戏", new Font("黑体", 15), Brushes.Red, exit);
            //return false;
        }

        public bool GameDo()
        {
            if (gamestate == GameState.PASS)
            {
                return true;
            }
            else if (gamestate == GameState.RUN)
            {
                GameRun();
            }
            else
            {
                
            }
            return false;
        }

        private void GameRun()        //游戏运行处理
        {
            /*pt.Move();
            pt.Show();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Move();
                bullets[i].Show();
            }*/
            if (gamestep < 100)
            {
                gamestep++;
                if (gamestep % 20 == 0)
                {
                    TankAIFire();
                }
            }
            else
            {
                gamestep = 0;
                if (eptank_count > 0)
                {
                    CreateTank();
                    eptank_count--;
                }
            }

            /*if (gamestep % 3 == 0)
            {
                CleanBombs();
            }*/

            CleanBombs();
            //DrawGame();
            Move();
            HitCheck();
            CleanDeathTank();
            CheckGame();
        }

        private void CleanBombs()
        {
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                //bombs.RemoveAt(i);
                bombs[i].LiveRun();
                if (!bombs[i].Islife)
                {
                    bombs.RemoveAt(i);
                }
            }
        }

        private void CreateTank()
        {
            Random rd = new Random();
            int r = rd.Next(1, 9);
            if (r % 2 == 0)
            {
                tanks.Add(new Tank(g, new Point(1, 1), new Size(24, 24), new string[] { "tanks\\eptank" + r + "1.bmp", "tanks\\eptank" + r + "2.bmp", "tanks\\eptank" + r + "3.bmp", "tanks\\eptank" + r + "4.bmp" }, 6, 6, Direction.DOWN, tbullets));
            }
            else
            {
                tanks.Add(new Tank(g, new Point(575, 1), new Size(24, 24), new string[] { "tanks\\eptank" + r + "1.bmp", "tanks\\eptank" + r + "2.bmp", "tanks\\eptank" + r + "3.bmp", "tanks\\eptank" + r + "4.bmp" }, 6, 6, Direction.DOWN, tbullets));
            }
        }

        private void DrawGame()             //屏幕绘制处理
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                tanks[i].Show();
            }
            for (int i = 0; i < playertanks.Count; i++)
            {
                playertanks[i].Show();
            }
            for (int i = 0; i < map.Terrains.Count; i++)
            {
                map.Terrains[i].Show();
            }
            for (int i = 0; i < tbullets.Count; i++)
            {
                tbullets[i].Show();
            }
            for (int i = 0; i < pbullets.Count; i++)
            {
                pbullets[i].Show();
            }
            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].Show();
            }
            for (int i = 0; i < prizes.Count; i++)
            {
                prizes[i].Show();
            }
        }

        private void HitCheck()             //碰撞处理
        {
            /*for (int i = 0; i < tbullets.Count; i++)    //电脑子弹
            {
                for (int j = 0; j < map.Terrains.Count; j++)
                {
                    if (tbullets[i].GetRectangle().IntersectsWith(map.Terrains[j].GetRectangle()))
                    {
                        if (map.Terrains[j].Terraintype == TerrainType.IRON)
                        {
                            tbullets.RemoveAt(i);
                        }
                        else if (map.Terrains[j].Terraintype == TerrainType.WALL || map.Terrains[j].Terraintype == TerrainType.HOME)
                        {
                            map.Terrains.RemoveAt(j);
                            tbullets.RemoveAt(i);
                        }
                    }
                }
            }

            for (int i = 0; i < pbullets.Count; i++)    //玩家子弹
            {
                //MessageBox.Show(pbullets.Count.ToString());
                for (int j = 0; j < map.Terrains.Count; j++)
                {
                    if (pbullets[i].GetRectangle().IntersectsWith(map.Terrains[j].GetRectangle()))
                    {
                        if (map.Terrains[j].Terraintype == TerrainType.IRON)
                        {
                            pbullets.RemoveAt(i);
                        }
                        else if (map.Terrains[j].Terraintype == TerrainType.WALL || map.Terrains[j].Terraintype == TerrainType.HOME)
                        {
                            map.Terrains.RemoveAt(j);
                            pbullets.RemoveAt(i);
                        }
                    }
                }
            }*/

            lable1:
            for (int i = map.Terrains.Count - 1; i >= 0; i--)
            {
                for (int j = pbullets.Count - 1; j >= 0; j--)
                {
                    if (map.Terrains[i].GetRectangle().IntersectsWith(pbullets[j].GetRectangle()))
                    {
                        if (map.Terrains[i].Terraintype == TerrainType.IRON)
                        {
                            //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\tie.wav";
                            //sp.Play();
                            SoundPlayer sp1 = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\tie.wav");
                            sp1.Play();
                            pbullets.RemoveAt(j);
                            goto lable1;
                        }
                        else if (map.Terrains[i].Terraintype == TerrainType.HOME)
                        {
                            //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
                            //sp.Play();
                            SoundPlayer sp1 = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\bob.wav");
                            sp1.Play();
                            map.Terrains.RemoveAt(i);
                            gamestate = GameState.OVER;
                            pbullets.RemoveAt(j);
                            goto lable1;
                        }
                        else if (map.Terrains[i].Terraintype == TerrainType.WALL)
                        {
                            //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\wall.wav";
                            //sp.Play();
                            SoundPlayer sp1 = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\wall.wav");
                            sp1.Play();
                            map.Terrains.RemoveAt(i);
                            pbullets.RemoveAt(j);
                            goto lable1;
                        }
                    }
                }

                for (int j = tbullets.Count - 1; j >= 0; j--)
                {
                    if (map.Terrains[i].GetRectangle().IntersectsWith(tbullets[j].GetRectangle()))
                    {
                        if (map.Terrains[i].Terraintype == TerrainType.IRON)
                        {
                            //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\tie.wav";
                            //sp.Play();
                            SoundPlayer sp1 = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\tie.wav");
                            sp1.Play();
                            tbullets.RemoveAt(j);
                            goto lable1;
                        }
                        else if (map.Terrains[i].Terraintype == TerrainType.HOME)
                        {
                            //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\bob.wav";
                            //sp.Play();
                            SoundPlayer sp1 = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\bob.wav");
                            sp1.Play();
                            map.Terrains.RemoveAt(i);
                            gamestate = GameState.OVER;
                            tbullets.RemoveAt(j);
                            goto lable1;
                        }
                        else if (map.Terrains[i].Terraintype == TerrainType.WALL)
                        {
                            //sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\wall.wav";
                            //sp.Play();
                            SoundPlayer sp1 = new SoundPlayer(Application.StartupPath + "\\Resource\\sounds\\wall.wav");
                            sp1.Play();
                            map.Terrains.RemoveAt(i);
                            tbullets.RemoveAt(j);
                            goto lable1;
                        }
                    }
                }
            }

            /*for (int i = pbullets.Count - 1; i >= 0; i--)
            {
                for (int j = tanks.Count - 1; j >= 0; j--)
                {
                    if (tanks[j].GetRectangle().IntersectsWith(pbullets[i].GetRectangle()))
                    {
                        pbullets.RemoveAt(i);
                        bombs.Add(tanks[j].Death());
                    }
                }
            }
            for (int i = tbullets.Count - 1; i >= 0; i--)
            {
                for (int j = playertanks.Count - 1; j >= 0; j--)
                {
                    if (playertanks[j].GetRectangle().IntersectsWith(tbullets[i].GetRectangle()))
                    {
                        tbullets.RemoveAt(i);
                        bombs.Add(playertanks[j].Death());
                    }
                }
            }*/

            for (int i = tanks.Count - 1; i >= 0; i--)
            {
                for (int j = pbullets.Count - 1; j >= 0; j--)
                {
                    if (tanks[i].GetRectangle().IntersectsWith(pbullets[j].GetRectangle()))
                    {
                        pbullets.RemoveAt(j);
                        Random rd = new Random();
                        int r = rd.Next(0,100);
                        //tanks[i].GetRectangle()
                        Rectangle rect = tanks[i].GetRectangle();
                        if (r >= 90 && r < 97)
                        {
                            prizes.Add(new Prize(this.g, rect.Location, rect.Size, new string[] { "others\\propBomb.bmp" }, PrizeType.BOMB));
                        }
                        else if (r >= 97)
                        {
                            prizes.Add(new Prize(this.g, rect.Location, rect.Size, new string[] { "others\\propTank.bmp" }, PrizeType.TANK));
                        }
                        
                        bombs.Add(tanks[i].Death());
                    }
                }
            }

            for (int i = playertanks.Count - 1; i >= 0; i--)
            {
                for (int j = tbullets.Count - 1; j >= 0; j--)
                {
                    if (playertanks[i].GetRectangle().IntersectsWith(tbullets[j].GetRectangle()))
                    {
                        tbullets.RemoveAt(j);
                        bombs.Add(playertanks[i].Death());
                    }
                }

                for (int j = prizes.Count - 1; j >= 0; j--)
                {
                    if (playertanks[i].GetRectangle().IntersectsWith(prizes[j].GetRectangle()))
                    {
                        if (prizes[j].Prizetype == PrizeType.BOMB)
                        {
                            //score += 10 * tanks.Count;
                            for (int k = 0; k < tanks.Count; k++)
                            {
                                bombs.Add(tanks[k].Death());
                            }
                        }
                        else if (prizes[j].Prizetype == PrizeType.TANK)
                        {
                            playertanks[i].Lifenum++;
                        }
                        prizes.RemoveAt(j);
                    }
                }
            }
        }

        private void CleanDeathTank()
        {
            for (int i = tanks.Count - 1; i >= 0; i--)
            {
                if (!tanks[i].Islife)
                {
                    score += 10;
                    tanks.RemoveAt(i);
                }
            }
            for (int i = playertanks.Count - 1; i >= 0; i--)
            {
                if (!playertanks[i].Islife)
                {
                    playertanks.RemoveAt(i);
                }
            }
        }

        private void CheckGame()
        {
            if (playertanks.Count == 0)
            {
                gamestate = GameState.OVER;
            }
            if (tanks.Count == 0 && eptank_count == 0)
            {
                gamestate = GameState.PASS;
                if (levelnum == 30)
                {
                    gamestate = GameState.PASSANDEND;
                }
            }
        }

        private void Move()
        {
            for (int i = 0; i < playertanks.Count; i++)         //玩家坦克移动
            {
                Rectangle playerrect = playertanks[i].MoveTest();
                bool moveable = true;
                for (int j = 0; j < map.Terrains.Count; j++)
                {
                    if (playerrect.IntersectsWith(map.Terrains[j].GetRectangle()))
                    {
                        if (map.Terrains[j].Terraintype != TerrainType.GRASSLAND)
                        {
                            //playertanks[i].UndoMove();
                            moveable = moveable && false;
                        }

                        else
                        {
                            //playertanks[i].Move();
                            moveable = moveable && true;
                        }
                    }

                }

                for (int j = 0; j < tanks.Count; j++)
                {
                    if (playerrect.IntersectsWith(tanks[j].GetRectangle()))
                    {
                        moveable = moveable & false;
                    }
                }

                if (playertanks[i].Tx > 0 && playertanks[i].Tx < 576 && playertanks[i].Ty > 0 && playertanks[i].Ty < 576)
                {
                    //playertanks[i].Move();
                    moveable = moveable && true;
                }
                else
                {
                    //playertanks[i].UndoMove();
                    moveable = moveable && false;
                }
                if (moveable)
                {
                    playertanks[i].Move();
                }
                else
                {
                    playertanks[i].UndoMove();
                }
            }

            TankAIMove();

            for (int i = pbullets.Count - 1; i >= 0; i--)
            {
                pbullets[i].Move();
                if (pbullets[i].X <= 0 || pbullets[i].X >= 600 || pbullets[i].Y <= 0 || pbullets[i].Y >= 600)
                {
                    pbullets.RemoveAt(i);
                }
            }
            for (int i = tbullets.Count - 1; i >= 0; i--)
            {
                tbullets[i].Move();
                if (tbullets[i].X <= 0 || tbullets[i].X >= 600 || tbullets[i].Y <= 0 || tbullets[i].Y >= 600)
                {
                    tbullets.RemoveAt(i);
                }
            }

            for (int i = prizes.Count - 1; i >= 0; i--)
            {
                prizes[i].LiveRun();
                if (!prizes[i].Islife)
                {
                    prizes.RemoveAt(i);
                }
            }
        }

        private void TankAIMove()
        {
            for (int i = 0; i < tanks.Count; i++)         //电脑坦克移动
            {
                Rectangle tankrect = tanks[i].MoveTest();
                bool moveable = true;
                for (int j = 0; j < map.Terrains.Count; j++)
                {
                    if (tankrect.IntersectsWith(map.Terrains[j].GetRectangle()))
                    {
                        if (map.Terrains[j].Terraintype != TerrainType.GRASSLAND)
                        {
                            //tanks[i].UndoMove();
                            //tanks[i].AIRoundDirection();
                            //goto Label1;
                            //continue;
                            //TankAIMove();
                            moveable = moveable && false;
                        }
                        else
                        {
                            moveable = moveable && true;
                        }
                    }

                }

                for (int j = 0; j < playertanks.Count; j++)
                {
                    if (tankrect.IntersectsWith(playertanks[j].GetRectangle()))
                    {
                        moveable = moveable & false;
                    }
                }

                if (tanks[i].Tx > 0 && tanks[i].Tx < 576 && tanks[i].Ty > 0 && tanks[i].Ty < 576)
                {
                    //tanks[i].Move();
                    moveable = moveable && true;
                }
                else
                {
                    //tanks[i].UndoMove();
                    //tanks[i].AIRoundDirection();
                    //TankAIMove();
                    //goto Label1;
                    //continue;
                    moveable = moveable && false;
                }
                if (moveable)
                {
                    tanks[i].Move();
                }
                else
                {
                    tanks[i].UndoMove();
                    tanks[i].AIRoundDirection();
                }
            }
        }

        private void TankAIFire()
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                tanks[i].Fire();
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.X >= backtitle.X && e.X <= (backtitle.X + backtitle.Width) && e.Y >= backtitle.Y && e.Y <= (backtitle.Y + backtitle.Height))
            {
                gf.Cursor = Cursors.Hand;
            }
            /*else if (e.X >= save.X && e.X <= (save.X + save.Width) && e.Y >= save.Y && e.Y <= (save.Y + save.Height))
            {
                gf.Cursor = Cursors.Hand;
            }*/
            else if (e.X >= exit.X && e.X <= (exit.X + exit.Width) && e.Y >= exit.Y && e.Y <= (exit.Y + exit.Height))
            {
                gf.Cursor = Cursors.Hand;
            }
            else if (e.X >= help.X && e.X <= (help.X + help.Width) && e.Y >= help.Y && e.Y <= (help.Y + help.Height))
            {
                gf.Cursor = Cursors.Hand;
            }
            else if (e.X >= cap.X && e.X <= (cap.X + cap.Width) && e.Y >= cap.Y && e.Y <= (cap.Y + cap.Height))
            {
                gf.Cursor = Cursors.Hand;
            }
            else
            {
                gf.Cursor = Cursors.Default;
            }
        }

        public void MouseClick(MouseEventArgs e)
        {
            if (e.X >= backtitle.X && e.X <= (backtitle.X + backtitle.Width) && e.Y >= backtitle.Y && e.Y <= (backtitle.Y + backtitle.Height))
            {
                gf.sf.Visible = true;
                gf.GameClose();
                gf.Dispose();
            }
            /*else if (e.X >= save.X && e.X <= (save.X + save.Width) && e.Y >= save.Y && e.Y <= (save.Y + save.Height))
            {
                FileStream fs = new FileStream(Application.StartupPath + @"\save\save.dat", FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                Save savedat = new Save(this.levelnum, this.eptank_count);
                savedat.SetElement(tanks, playertanks, tbullets, pbullets, bombs, map);
                bf.Serialize(fs, savedat);
                fs.Close();
                MessageBox.Show("游戏保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
            else if (e.X >= exit.X && e.X <= (exit.X + exit.Width) && e.Y >= exit.Y && e.Y <= (exit.Y + exit.Height))
            {
                gf.Close();
                //Application.Exit();
            }
            else if (e.X >= help.X && e.X <= (help.X + help.Width) && e.Y >= help.Y && e.Y <= (help.Y + help.Height))
            {
                Help.ShowHelp(gf, Application.StartupPath + "\\Help.chm");
            }
            else if (e.X >= cap.X && e.X <= (cap.X + cap.Width) && e.Y >= cap.Y && e.Y <= (cap.Y + cap.Height))
            {
                int i = 1;
                while (File.Exists(Application.StartupPath + "\\cap\\" + "GameCap" + i + ".jpg"))
                {
                    i++;
                }
                gf.buffereimage.Save(Application.StartupPath + "\\cap\\" + "GameCap" + i + ".jpg");
                MessageBox.Show("截屏成功!图片保存为:" + Application.StartupPath + "\\cap\\" + "GameCap" + i + ".jpg。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            for (int i = 0; i < playertanks.Count; i++)
            {
                playertanks[i].KeyUp(e);
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            for (int i = 0; i < playertanks.Count; i++)
            {
                playertanks[i].KeyDown(e);
            }
        }
    }
}
