using System.Drawing;

namespace GoBang
{
    public class ChessMan
    {
        private Point point;
        private bool isBlack;
        /// <summary>
        /// 棋子的位置
        /// </summary>
        public Point Position
        {
            get { return point; }
            set { point = value; }
        }
        /// <summary>
        /// 棋子是否是黑色
        /// </summary>
        public bool IsBlack
        {
            get { return isBlack; }
            set { isBlack = value; }
        }

        public int X
        {
            get { return point.X; }
        }

        public int Y
        {
            get { return point.Y; }
        }

        public ChessMan(Point point, bool isBlack)
        {
            this.point = point;
            this.isBlack = isBlack;
        }

        public ChessMan(int x, int y, bool isBlack)
        {
            this.point = new Point(x, y);
            this.isBlack = isBlack;
        }
    }
}
