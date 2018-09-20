using System;
using System.Drawing;

namespace GoBang
{
    delegate void ChangeFocusHandle(ChessMan chessMan);
    delegate void PutChessManHandle(ChessMan chessMan);

    class Gobang
    {
        private ChessMan focusChess, lastBlack, lastWhite;
        private bool isBlack, isOver, hasWinner;
        private ChessMan[,] chessMen;
        private int[,] Power;
        private int number;
        private bool isPair;

        public event ChangeFocusHandle ChangeFocusEvent;
        public event PutChessManHandle PutChessManEvent;

        public bool IsBlack
        {
            get { return isBlack; }
        }

        public bool IsOver
        {
            get { return isOver; }
        }

        public bool HasWinner
        {
            get { return hasWinner; }
        }

        public int Number
        {
            get { return number; }
        }

        public ChessMan[,] ChessMen
        {
            get { return chessMen; }
        }

        public Gobang()
        {
            this.Inix(false);
        }

        /// <summary>
        /// 游戏初始化
        /// </summary>
        public void Inix(bool isPair)
        {
            isBlack = true;
            isOver = false;
            hasWinner = false;
            focusChess = null;
            lastBlack = null;
            lastWhite = null;
            chessMen = new ChessMan[15, 15];
            Power = new int[15, 15];
            number = 0;
            this.isPair = isPair;
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public bool Undo()
        {
            if (number > 1)
            {
                if (focusChess.X != -1)
                {
                    if (isPair)
                    {
                        if (isBlack)
                        {
                            chessMen[lastWhite.X, lastWhite.Y] = null;
                            CountPower(lastWhite);
                            lastWhite = null;
                        }
                        else
                        {
                            chessMen[lastBlack.X, lastBlack.Y] = null;
                            CountPower(lastBlack);
                        }
                        number--;
                        isBlack = !isBlack;
                        return true;
                    }
                    else
                    {
                        chessMen[lastBlack.X, lastBlack.Y] = null;
                        CountPower(lastBlack);
                        lastBlack = null;
                        chessMen[lastWhite.X, lastWhite.Y] = null;
                        CountPower(lastWhite);
                        lastWhite = null;
                        number -= 2;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断指定位置是否是指定的棋子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsInBoard(int x, int y, bool b)
        {
            return chessMen[x, y] != null && chessMen[x, y].IsBlack == b;
        }

        /// <summary>
        /// 判断指定位置是否为空
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsEmpty(int x, int y)
        {
            return chessMen[x, y] == null;
        }

        /// <summary>
        /// b是否已经胜利
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsWin(ChessMan chessMan)
        {
            int x = chessMan.X, y = chessMan.Y;
            bool b = chessMan.IsBlack;
            int count = 0;
            for (int i = 0; i < 5 && x - i >= 0; i++)
                if (IsInBoard(x - i, y, b)) count++; else break;
            for (int i = 1; i < 5 && x + i < 15; i++)
                if (IsInBoard(x + i, y, b)) count++; else break;
            if (count > 4) return true;

            else count = 0;
            for (int i = 0; i < 5 && y - i >= 0; i++)
                if (IsInBoard(x, y - i, b)) count++; else break;
            for (int i = 1; i < 5 && y + i < 15; i++)
                if (IsInBoard(x, y + i, b)) count++; else break;
            if (count > 4) return true;

            else count = 0;
            for (int i = 0; i < 5 && x - i >= 0 && y - i >= 0; i++)
                if (IsInBoard(x - i, y - i, b)) count++; else break;
            for (int i = 1; i < 5 && x + i < 15 && y + i < 15; i++)
                if (IsInBoard(x + i, y + i, b)) count++; else break;
            if (count > 4) return true;

            else count = 0;
            for (int i = 0; i < 5 && x - i >= 0 && y + i < 15; i++)
                if (IsInBoard(x - i, y + i, b)) count++; else break;
            for (int i = 1; i < 5 && y - i >= 0 && x + i < 15; i++)
                if (IsInBoard(x + i, y - i, b)) count++; else break;
            if (count > 4) return true;
            return false;
        }

        /// <summary>
        /// 下子
        /// </summary>
        /// <param name="chessMan"></param>
        public void PutChessMan(ChessMan chessMan)
        {
            if (hasWinner || isOver)
            {
                return;
            }
            focusChess = chessMan;
            this.chessMen[chessMan.X, chessMan.Y] = chessMan;
            if (chessMan.IsBlack)
            {
                lastBlack = chessMan;
            }
            else
            {
                lastWhite = chessMan;
            }
            if (PutChessManEvent != null)
            {
                PutChessManEvent(chessMan);
            }
            if (ChangeFocusEvent != null)
            {
                ChangeFocusEvent(focusChess);
            }
            if (IsWin(chessMan))
            {
                hasWinner = true;
                return;
            }
            else if (number++ == 225)
            {
                isOver = true;
                return;
            }
            CountPower(chessMan);
            isBlack = !isBlack;
        }

        /// <summary>
        /// 计算机寻找最佳落子坐标
        /// </summary>
        /// <returns></returns>
        public Point FindBestPoint()
        {
            int max = -1, maxi = 0, maxj = 0;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (chessMen[i, j] == null && Power[i, j] > max)
                    {
                        max = Power[i, j];
                        maxi = i;
                        maxj = j;
                    }
                }
            }
            if (Power[maxi, maxj] == 0 && chessMen[7, 7] == null)
            {
                maxi = 7;
                maxj = 7;
            }
            return new Point(maxi, maxj);
        }

        /// <summary>
        /// 计算周围5各单位内的各点的权值
        /// </summary>
        private void CountPower(ChessMan chessMan)
        {
            if (isPair)
                return;

            int minx = Math.Max(chessMan.X - 5, 0);
            int maxx = Math.Min(chessMan.X + 5, 14);
            int miny = Math.Max(chessMan.Y - 5, 0);
            int maxy = Math.Min(chessMan.Y + 5, 14);
            for (int i = minx; i <= maxx; i++)
            {
                for (int j = miny; j <= maxy; j++)
                {
                    if (chessMen[i, j] == null)
                    {
                        Power[i, j] = GetPower(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// 计算指定位置的权值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetPower(int x, int y)
        {
            int pow = 0, count = 0, empty1 = 0, empty2 = 0;
            for (int i = 1; i < 5 && x - i >= 0; i++)
                if (IsInBoard(x - i, y, true)) count++; else break;
            for (int i = 1; i < 5 && x + i < 15; i++)
                if (IsInBoard(x + i, y, true)) count++; else break;
            for (int i = 1; i < 5 && x - i >= 0; i++)
                if (IsEmpty(x - i, y)) empty1++; else if (IsInBoard(x - i, y, false)) break;
            for (int i = 1; i < 5 && x + i < 15; i++)
                if (IsEmpty(x + i, y)) empty2++; else if (IsInBoard(x + i, y, false)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2) / 2;

            count = empty1 = empty2 = 0;
            for (int i = 1; i < 5 && y - i >= 0; i++)
                if (IsInBoard(x, y - i, true)) count++; else break;
            for (int i = 1; i < 5 && y + i < 15; i++)
                if (IsInBoard(x, y + i, true)) count++; else break;
            for (int i = 1; i < 5 && y - i >= 0; i++)
                if (IsEmpty(x, y - i)) empty1++; else if (IsInBoard(x, y - i, false)) break;
            for (int i = 1; i < 5 && y + i < 15; i++)
                if (IsEmpty(x, y + i)) empty2++; else if (IsInBoard(x, y + i, false)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2) / 2;

            count = empty1 = empty2 = 0;
            for (int i = 1; i < 5 && x - i >= 0 && y - i >= 0; i++)
                if (IsInBoard(x - i, y - i, true)) count++; else break;
            for (int i = 1; i < 5 && x + i < 15 && y + i < 15; i++)
                if (IsInBoard(x + i, y + i, true)) count++; else break;
            for (int i = 1; i < 5 && x - i >= 0 && y - i >= 0; i++)
                if (IsEmpty(x - i, y - i)) empty1++; else if (IsInBoard(x - i, y - i, false)) break;
            for (int i = 1; i < 5 && x + i < 15 && y + i < 15; i++)
                if (IsEmpty(x + i, y + i)) empty2++; else if (IsInBoard(x + i, y + i, false)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2) / 2;

            count = empty1 = empty2 = 0;
            for (int i = 1; i < 5 && x - i >= 0 && y + i < 15; i++)
                if (IsInBoard(x - i, y + i, true)) count++; else break;
            for (int i = 1; i < 5 && y - i >= 0 && x + i < 15; i++)
                if (IsInBoard(x + i, y - i, true)) count++; else break;
            for (int i = 1; i < 5 && x - i >= 0 && y + i < 15; i++)
                if (IsEmpty(x - i, y + i)) empty1++; else if (IsInBoard(x - i, y + i, false)) break;
            for (int i = 1; i < 5 && y - i >= 0 && x + i < 15; i++)
                if (IsEmpty(x + i, y - i)) empty2++; else if (IsInBoard(x + i, y - i, false)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2) / 2;
            ////////////////////////////////////////////////////////
            count = empty1 = empty2 = 0;
            for (int i = 1; i < 5 && x - i >= 0; i++)
                if (IsInBoard(x - i, y, false)) count++; else break;
            for (int i = 1; i < 5 && x + i < 15; i++)
                if (IsInBoard(x + i, y, false)) count++; else break;
            for (int i = 1; i < 5 && x - i >= 0; i++)
                if (IsEmpty(x - i, y)) empty1++; else if (IsInBoard(x - i, y, true)) break;
            for (int i = 1; i < 5 && x + i < 15; i++)
                if (IsEmpty(x + i, y)) empty2++; else if (IsInBoard(x + i, y, true)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2);

            count = empty1 = empty2 = 0;
            for (int i = 1; i < 5 && y - i >= 0; i++)
                if (IsInBoard(x, y - i, false)) count++; else break;
            for (int i = 1; i < 5 && y + i < 15; i++)
                if (IsInBoard(x, y + i, false)) count++; else break;
            for (int i = 1; i < 5 && y - i >= 0; i++)
                if (IsEmpty(x, y - i)) empty1++; else if (IsInBoard(x, y - i, true)) break;
            for (int i = 1; i < 5 && y + i < 15; i++)
                if (IsEmpty(x, y + i)) empty2++; else if (IsInBoard(x, y + i, true)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2);

            count = empty1 = empty2 = 0;
            for (int i = 1; i < 5 && x - i >= 0 && y - i >= 0; i++)
                if (IsInBoard(x - i, y - i, false)) count++; else break;
            for (int i = 1; i < 5 && x + i < 15 && y + i < 15; i++)
                if (IsInBoard(x + i, y + i, false)) count++; else break;
            for (int i = 1; i < 5 && x - i >= 0 && y - i >= 0; i++)
                if (IsEmpty(x - i, y - i)) empty1++; else if (IsInBoard(x - i, y - i, true)) break;
            for (int i = 1; i < 5 && x + i < 15 && y + i < 15; i++)
                if (IsEmpty(x + i, y + i)) empty2++; else if (IsInBoard(x + i, y + i, true)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2);

            count = empty1 = empty2 = 0;
            for (int i = 1; i < 5 && x - i >= 0 && y + i < 15; i++)
                if (IsInBoard(x - i, y + i, false)) count++; else break;
            for (int i = 1; i < 5 && y - i >= 0 && x + i < 15; i++)
                if (IsInBoard(x + i, y - i, false)) count++; else break;
            for (int i = 1; i < 5 && x - i >= 0 && y + i < 15; i++)
                if (IsEmpty(x - i, y + i)) empty1++; else if (IsInBoard(x - i, y + i, true)) break;
            for (int i = 1; i < 5 && y - i >= 0 && x + i < 15; i++)
                if (IsEmpty(x + i, y - i)) empty2++; else if (IsInBoard(x + i, y - i, true)) break;
            if (empty1 > 0 && empty2 > 0 || count > 1) pow += Squ(count, empty1, empty2);

            return pow;
        }

        /// <summary>
        /// 累加权值
        /// </summary>
        /// <param name="n"></param>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        private int Squ(int n, int e1, int e2)
        {
            if (n + e1 + e2 > 3)
            {
                if (n > 3) return 1000000;
                else if (n == 3 && e1 > 0 && e2 > 0) return 100000;
                else if (n == 3) return 10000;
                else if (n == 2 && e1 > 0 && e2 > 0) return 1000;
                else if (n == 2) return 100;
                else if (n == 1 && e1 > 0 && e2 > 0) return 10;
                else if (n == 1) return 1;
                else return 0;
            }
            return 0;
        }
    }
}
