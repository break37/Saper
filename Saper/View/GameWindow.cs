using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper.View
{
    public partial class GameWindow : Form, IGameWindow
    {
        #region Fields

        Graphics gameBoardCanvas;

        #endregion

        #region Properties

        public string Time
        {
            set { textBoxTime.Text = $"{Convert.ToInt32(value) / 60}:{Convert.ToInt32(value) % 60}"; }
        }

        public Panel GameBoard
        {
            get { return panelGameBoard; }
            set { panelGameBoard = value; }
        }

        #endregion

        #region Actions

        public Timer GameTime
        {
            get { return timerGameTime; }
            set { timerGameTime = value; }
        }

        public event Action Loaded;
        public event Action<int, int> GameStarted;
        public event Action<int,int> Dig;
        public event Action<int,int> FlagToggled;
        public event Action GameEnded;
        public event Action Reset;

        #endregion

        public GameWindow()
        {
            InitializeComponent();
            gameBoardCanvas = panelGameBoard.CreateGraphics(); 
        }

        private void GameWindow_Shown(object sender, EventArgs e)
        {
            Loaded?.Invoke();
        }

        private void PanelGameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            GameStarted?.Invoke(x, y);

            //togle flag
            if (e.Button == MouseButtons.Right)
            {
                FlagToggled?.Invoke(x, y);
            }

            if (e.Button == MouseButtons.Left)
            {
                Dig?.Invoke(x, y);
            }
        }

        public void DrawTiles(List<Tile> tilesToDraw)
        {
            foreach (var tile in tilesToDraw)
            {
                Bitmap img = new Bitmap(tile.Path);
                gameBoardCanvas.DrawImage(img, tile.X, tile.Y);

                if (tile.Path.Contains("#"))
                {
                    GameEnded?.Invoke();
                }
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            Reset?.Invoke();
            Loaded?.Invoke();
        }
    }
}
