using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GoBang
{
    public partial class frmGame : Form
    {
        public frmGame()
        {
            InitializeComponent();

            this.tsmiNew.Tag = false;
            this.tsmiPair.Tag = true;
        }

        private ChessMan theNewChess, theLastChess;
        private Point lastFocusPoint, thisFocuePoint;
        private Gobang gobang;
        private bool IsPair = false;

        void gobang_PutChessManEvent(ChessMan chessMan)
        {
            if (chessMan == null)
                return;
            theLastChess = theNewChess;
            theNewChess = chessMan;
            Graphics gp = lblGame.CreateGraphics();
            gp.DrawImage(chessMan.IsBlack ? Resource1.black : Resource1.white, chessMan.X * 30 + 2, chessMan.Y * 30 + 2, 26, 26);
            if (theLastChess != null)
            {
                gp.DrawImage(chessMan.IsBlack ? Resource1.white : Resource1.black, theLastChess.X * 30 + 2, theLastChess.Y * 30 + 2, 26, 26);
            }
            gp.Dispose();
        }

        void gobang_ChangeFocusEvent(ChessMan chessMan)
        {
            if (chessMan != null)
            {
                theNewChess = chessMan;
                ShowTheNewChess(theNewChess);
            }
        }

        //显示最新下的棋子
        private void ShowTheNewChess(ChessMan chessMan)
        {
            if (chessMan == null)
                return;
            Pen pen = new Pen(chessMan.IsBlack ? Color.White : Color.Black);
            Graphics gp = lblGame.CreateGraphics();
            int x = chessMan.X * 30;
            int y = chessMan.Y * 30;
            gp.DrawLine(pen, x + 8, y + 15, x + 13, y + 15);
            gp.DrawLine(pen, x + 17, y + 15, x + 22, y + 15);
            gp.DrawLine(pen, x + 15, y + 8, x + 15, y + 13);
            gp.DrawLine(pen, x + 15, y + 17, x + 15, y + 22);
            gp.Dispose();
        }

        //游戏区域绘制
        private void lblGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            Pen pen = new Pen(Color.Black);
            gp.Clear(Color.BurlyWood);
            for (int i = 15; i < 450; i += 30)
            {
                gp.DrawLine(pen, i, 15, i, 435);
                gp.DrawLine(pen, 15, i, 435, i);
            }
            foreach (ChessMan cm in gobang.ChessMen)
            {
                if (cm != null)
                {
                    gp.DrawImage(cm.IsBlack ? Resource1.black : Resource1.white, cm.X * 30 + 2, cm.Y * 30 + 2, 26, 26);
                }
            }
            ShowTheNewChess(theNewChess);
        }

        //鼠标移动
        private void lblGame_MouseMove(object sender, MouseEventArgs e)
        {
            Point theFocusPoint = new Point(e.X / 30, e.Y / 30);
            if (e.X < 0 || e.Y < 0 || e.X > 449 || e.Y > 449 || theFocusPoint == lastFocusPoint)
                return;
            DrawRect(Pens.BurlyWood, lastFocusPoint);
            thisFocuePoint = theFocusPoint;
            DrawRect(Pens.Red, thisFocuePoint);
            lastFocusPoint = thisFocuePoint;
        }

        //绘制当前位置
        private void DrawRect(Pen rectPen, Point p)
        {
            Graphics gp = lblGame.CreateGraphics();
            int x = p.X * 30;
            int y = p.Y * 30;
            gp.DrawLine(rectPen, x, y, x + 10, y);
            gp.DrawLine(rectPen, x + 20, y, x + 30, y);
            gp.DrawLine(rectPen, x, y + 30, x + 10, y + 30);
            gp.DrawLine(rectPen, x + 20, y + 30, x + 30, y + 30);
            gp.DrawLine(rectPen, x, y, x, y + 10);
            gp.DrawLine(rectPen, x, y + 20, x, y + 30);
            gp.DrawLine(rectPen, x + 30, y, x + 30, y + 10);
            gp.DrawLine(rectPen, x + 30, y + 20, x + 30, y + 30);
            gp.Dispose();
        }

        //单击下子
        private void lblGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (!gobang.IsEmpty(thisFocuePoint.X, thisFocuePoint.Y))
                return;
            gobang.PutChessMan(new ChessMan(thisFocuePoint, gobang.IsBlack));

            if (!gobang.HasWinner && !gobang.IsOver)
            {
                if (!IsPair)
                {
                    thisFocuePoint = gobang.FindBestPoint();
                    gobang.PutChessMan(new ChessMan(thisFocuePoint, gobang.IsBlack));
                }
                tsmiUnDo.Enabled = true;
            }
            if (gobang.HasWinner)
            {
                if (IsPair)
                {
                    ShowResult((gobang.IsBlack ? "黑" : "白") + "方胜！\n是否开始新游戏？");
                }
                else
                {
                    ShowResult("你" + (gobang.IsBlack ? "赢" : "输") + "了！\n是否开始新游戏？");
                }
            }
            else if (gobang.Number == 225)
            {
                ShowResult("和局！\n是否开始新游戏？");
            }
        }

        private void ShowResult(string win)
        {
            tsmiUnDo.Enabled = false;
            DialogResult re = MessageBox.Show(win, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (re == DialogResult.Yes)
            {
                NewGame();
            }
        }

        //窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
            gobang = new Gobang();
            gobang.ChangeFocusEvent += new ChangeFocusHandle(gobang_ChangeFocusEvent);
            gobang.PutChessManEvent += new PutChessManHandle(gobang_PutChessManEvent);
            tsmiUnDo.Enabled = false;
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                var tsmi = sender as ToolStripMenuItem;
                this.IsPair = (bool)tsmi.Tag;
                NewGame();
            }
        }

        private void NewGame()
        {
            gobang.Inix(this.IsPair);
            theNewChess = null;
            theLastChess = null;
            lblGame.Invalidate();
            tsmiUnDo.Enabled = false;
        }

        private void tsmiUnDo_Click(object sender, EventArgs e)
        {
            if (gobang.Undo())
            {
                lblGame.Invalidate();
                theNewChess = null;
                tsmiUnDo.Enabled = false;
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            ShellAbout(this.Handle, "五子棋", "精品小游戏五子棋", this.Icon.Handle);
        }

        /// <summary>
        /// 显示系统关于界面
        /// Show Windows About Dialog
        /// </summary>
        /// <param name="hWnd">主程序句柄</param>
        /// <param name="szApp">标题</param>
        /// <param name="szOtherStuff">说明文字</param>
        /// <param name="hIcon">图标句柄</param>
        /// <example>WindowsAPI.ShellAbout(this.Handle, "caption", "text", this.Icon.Handle.ToInt32());</example>
        [DllImport("shell32.dll")]
        public extern static int ShellAbout(IntPtr hWnd, string szApp, string szOtherStuff, IntPtr hIcon);
    }
}