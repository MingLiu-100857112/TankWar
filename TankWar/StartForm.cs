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