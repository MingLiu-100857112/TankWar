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