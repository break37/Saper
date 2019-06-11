using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Saper.View;
using Saper.Model;

namespace Saper.Presenter
{
    public class GameWindowPresenter
    {
        IGameWindow view;
        MainModel model;
        
        public GameWindowPresenter(IGameWindow view, MainModel model)
        {
            this.view = view;
            this.model = model;
            this.view.GameStarted += StartGame;
            this.view.GameTime.Tick += new EventHandler(GetTime);
            this.view.Loaded += LoadBoard;
            this.view.Dig += Dig;
            this.view.FlagToggled += ToggleFlag;
            this.view.GameEnded += EndGame;
            this.view.Reset += ResetBoard;
        }


        private void LoadBoard()
        {
            view.DrawTiles(model.LoadFullBoard());
        }

        private void StartGame(int x, int y)
        {
            view.GameTime.Enabled = true;
            view.GameTime.Start(); 
            model.StartTimer();
            view.Time = model.GetTime().ToString();
        }

        private void GetTime(object sender, EventArgs e)
        {
            view.Time = model.GetTime().ToString();
        }

        private void Dig(int x, int y)
        {
            if (model.GameStatus() == null)
            {
                view.DrawTiles(model.ShowFields(x, y));
            }
            if (model.GameStatus() == true)
            {
                Console.WriteLine("You won!");
            }
            else if (model.GameStatus() == false)
            {
                Console.WriteLine("You loose!");
            }

        }

        private void ToggleFlag(int x, int y)
        {
            if (model.GameStatus() == null)
            {
                view.DrawTiles(model.ToggleFlag(x, y));
            }
            if (model.GameStatus() == true)
            {
                Console.WriteLine("You Won!");
            }
            else if (model.GameStatus() == false)
            {
                Console.WriteLine("You loose!");
            }
        }
        
        private void EndGame()
        {
            view.DrawTiles(model.ShowBombs());
        }

        private void ResetBoard()
        {
            model.ResetGame();
        }
    }
}
