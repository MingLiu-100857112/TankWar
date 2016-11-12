namespace TankWar
{
    partial class GameInterface
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameInterface));
            this.SuspendLayout();
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 252);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tank Defense Battle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameInterfaceClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameInterfaceClose);
            this.Load += new System.EventHandler(this.GameInterfaceLoad);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameInterfaceDraw);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameInterfaceKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameInterfaceKeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameInterfaceMouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameInterfaceMouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}