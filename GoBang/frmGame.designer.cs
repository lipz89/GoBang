namespace GoBang
{
    partial class frmGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGame));
            this.lblGame = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnDo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPair = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGame
            // 
            this.lblGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGame.Location = new System.Drawing.Point(12, 27);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(453, 453);
            this.lblGame.TabIndex = 0;
            this.lblGame.Text = "label1";
            this.lblGame.Paint += new System.Windows.Forms.PaintEventHandler(this.lblGame_Paint);
            this.lblGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblGame_MouseClick);
            this.lblGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblGame_MouseMove);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGame,
            this.tsmiHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(477, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiGame
            // 
            this.tsmiGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiPair,
            this.tsmiUnDo,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiGame.Name = "tsmiGame";
            this.tsmiGame.Size = new System.Drawing.Size(61, 21);
            this.tsmiGame.Text = "游戏(&G)";
            // 
            // tsmiNew
            // 
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmiNew.Size = new System.Drawing.Size(194, 22);
            this.tsmiNew.Tag = "";
            this.tsmiNew.Text = "人机对战(&N)";
            this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
            // 
            // tsmiUnDo
            // 
            this.tsmiUnDo.Name = "tsmiUnDo";
            this.tsmiUnDo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmiUnDo.Size = new System.Drawing.Size(194, 22);
            this.tsmiUnDo.Text = "撤销(&Z)";
            this.tsmiUnDo.Click += new System.EventHandler(this.tsmiUnDo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiExit.Size = new System.Drawing.Size(194, 22);
            this.tsmiExit.Text = "退出(&X)";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(61, 21);
            this.tsmiHelp.Text = "帮助(&H)";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(116, 22);
            this.tsmiAbout.Text = "关于(&A)";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // tsmiPair
            // 
            this.tsmiPair.Name = "tsmiPair";
            this.tsmiPair.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F2)));
            this.tsmiPair.Size = new System.Drawing.Size(194, 22);
            this.tsmiPair.Tag = "";
            this.tsmiPair.Text = "双人对战(&P)";
            this.tsmiPair.Click += new System.EventHandler(this.tsmiNew_Click);
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 492);
            this.Controls.Add(this.lblGame);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "五子棋";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGame;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnDo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiPair;
    }
}

