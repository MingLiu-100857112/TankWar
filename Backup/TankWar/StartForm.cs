using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Media;
using AudioLibrary;

namespace TankWar
{
    public enum StartOrLoad     //сно╥
    {
        START,LOAD
    }
    //[Serializable]
    public partial class StartForm : Form
    {
        //private SoundPlayer sp;

        private int playercount = 1;

        public StartForm()
        {
            InitializeComponent();
            this.BackgroundImage = System.Drawing.Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\Resource\images\systems\tank.jpg");
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            /*sp = new SoundPlayer();
            sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\tongguan.wav";
            sp.Play();*/
            AudioByAPI.OpenAndPlay(Application.StartupPath + "\\Resource\\sounds\\sop.mp3", true);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            GameForm gf = new GameForm(this, StartOrLoad.START, playercount);
            //MessageBox.Show(playercount.ToString());
            this.Visible = false;
            AudioByAPI.Stop();
            gf.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            GameForm gf = new GameForm(this, StartOrLoad.LOAD, playercount);
            this.Visible = false;
            //sp.Stop();
            AudioByAPI.Stop();
            gf.Show();
        }

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

        private void StartForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                /*sp.SoundLocation = Application.StartupPath + "\\Resource\\sounds\\tongguan.wav";
                sp.Play();*/
                AudioByAPI.OpenAndPlay(Application.StartupPath + "\\Resource\\sounds\\sop.mp3", true);
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + "\\Help.chm");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = Application.StartupPath + "\\WorldEditor.exe";
            ps.Start();
        }
    }
}