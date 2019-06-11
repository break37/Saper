using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Saper.Model
{
    public class MainModel
    {
        #region Fields

        private const int bombsAmount = 10;
        private List<Coords> BombsCoords;
        private int uncoveredFields = 0;
        private bool isGameLost = false;
        private bool endGameFlag = true;
        private bool timerFlag = true;
        private int startTime;

        #endregion


        #region Properties

        public string[,] GameBoard { get; set; }
        public string[,] PlayerBoard { get; set; }
        public int Size { get; } = 10;
        public int FlagsMarked { get; set; } = 0;

        #endregion

        public MainModel()
        {
            GameBoard = new string[Size, Size];
            PlayerBoard = new string[Size, Size];
            BombsCoords = new List<Coords>();
            ClearBoard();
            PlantBombs();
            InitializeBoard();
        }

        public void StartTimer()
        {
            if (timerFlag)
            {
                startTime = DateTime.Now.Second;
                timerFlag = false;
            }
        }

        public int GetTime()
        {
            return (DateTime.Now.Second - startTime);
        }

        public void PlantBombs()
        {
            int iter = 0;
            while (iter < bombsAmount)
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                int x = rand.Next(0, 9);
                int y = rand.Next(0, 9);

                Coords bombCoords = new Coords(x, y);

                //check if bomb is not already placed in the same place
                if (!GameBoard[x, y].Equals("#"))
                {
                    GameBoard[x, y] = "#";
                    BombsCoords.Add(bombCoords);
                    iter++;
                }
            }
            Console.WriteLine("bombs planted");
        }

        private void InitializeBoard()
        {
            foreach (Coords item in BombsCoords)
            {
                int x = item.X;
                int y = item.Y;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        try
                        {
                            if (GameBoard[x + i, y + j] != "#")
                            {
                                GameBoard[x + i, y + j] = (Convert.ToInt16(GameBoard[x + i, y + j]) + 1).ToString();
                            }
                        }
                        catch (Exception)
                        {
                            //exception means, that index was out of range, which acually isn't a problem
                        }
                    }
                }
            }
        }

        public List<Tile> LoadFullBoard()
        {
            List<Tile> tilesToDraw = new List<Tile>();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    tilesToDraw.Add(new Tile(i, j, @"bitmaps\field .bmp"));
                }
            }

            return tilesToDraw;
        }

        public List<Tile> ToggleFlag(int x, int y)
        {
            List<Tile> tile = new List<Tile>();
            //coordinates comes as a cursor position value, so to transform them into table indices its necessary to divide them by 25
            x = x / 25;
            y = y / 25;

            if (PlayerBoard[y, x] == " ")
            {
                PlayerBoard[y, x] = "F";
                tile.Add(new Tile(x, y, $@"bitmaps\fieldF.bmp"));
                FlagsMarked++;
            }
            else if(PlayerBoard[y, x] == "F")
            {
                PlayerBoard[y, x] = " ";
                tile.Add(new Tile(x, y, $@"bitmaps\field .bmp"));
                FlagsMarked--;
            }

            return tile;
        }

        public List<Tile> ShowFields(int x, int y)
        {
            //coordinates comes as a cursor position value, so to transform them into table indices its necessary to divide them by 25
            x = x / 25;
            y = y / 25;

            if (uncoveredFields + FlagsMarked == 100)
            {
                Console.WriteLine("Wygrałeś");
            }
            return FindNextEmpty(x, y);
        }

        private List<Tile> FindNextEmpty(int x, int y)
        {
            List<Tile> uncovered = new List<Tile>();

            try
            {
                //empty field uncovered
                if (GameBoard[y, x] == "0" && PlayerBoard[y, x] == " ")
                {
                    PlayerBoard[y, x] = GameBoard[y, x];
                    uncovered.Add(new Tile(x, y, $@"bitmaps\field{PlayerBoard[y, x]}.bmp"));
                    uncoveredFields++;

                    if (x < Size - 1)
                        foreach (Tile tile in FindNextEmpty(x + 1, y))
                            uncovered.Add(tile);

                    if (y > 0)
                        foreach (Tile tile in FindNextEmpty(x, y - 1))
                            uncovered.Add(tile);

                    if (x > 0)
                        foreach (Tile tile in FindNextEmpty(x - 1, y))
                            uncovered.Add(tile);

                    if (y < Size - 1)
                        foreach (Tile tile in FindNextEmpty(x, y + 1))
                        uncovered.Add(tile);

                    if (x < Size - 1 && y < Size - 1)
                        foreach (Tile tile in FindNextEmpty(x + 1, y + 1))
                            uncovered.Add(tile);

                    if (x > 0 && y > 0)
                        foreach (Tile tile in FindNextEmpty(x - 1, y - 1))
                            uncovered.Add(tile);

                    if (x > 0 && y < Size - 1)
                        foreach (Tile tile in FindNextEmpty(x - 1, y + 1))
                            uncovered.Add(tile);

                    if (x < Size - 1 && y > 0)
                        foreach (Tile tile in FindNextEmpty(x + 1, y - 1))
                            uncovered.Add(tile);
                }
                //number uncovered
                else if (GameBoard[y, x] != "#" && PlayerBoard[y, x] == " ")
                {
                    PlayerBoard[y, x] = GameBoard[y, x];      
                    uncovered.Add(new Tile(x, y, $@"bitmaps\field{PlayerBoard[y, x]}.bmp"));
                    uncoveredFields++;
                }
                //bomb uncovered
                else if (GameBoard[y, x] == "#" && PlayerBoard[y, x] == " ")
                {
                    isGameLost = true;
                    PlayerBoard[y, x] = GameBoard[y, x];
                    uncovered.Add(new Tile(x, y, $@"bitmaps\field#red.bmp"));
                }
            }
            catch (Exception)
            {
                //IndexOutOfRangeException, no problem
            }

            return uncovered;
        }


        public void ClearBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    GameBoard[i, j] = "0";
                    PlayerBoard[i, j] = " ";
                }
            }
        }

        public List<Tile> ShowBombs()
        {
            List<Tile> bombs = new List<Tile>();

            if (endGameFlag)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (GameBoard[j, i] == "#" && PlayerBoard[j, i] == " ")
                        {
                            bombs.Add(new Tile(i, j, $@"bitmaps\field{GameBoard[j, i]}.bmp"));
                        }
                    }
                }

                endGameFlag = false;
            }
            return bombs;
        }

        public bool? GameStatus()
        {
            //true - won
            //false - lost
            //null - still in progress

            if (uncoveredFields + FlagsMarked == 100 && FlagsMarked == bombsAmount) return true;
            else if (isGameLost) return false;
            else return null;
        } 

        public void ResetGame()
        {
            FlagsMarked = 0;
            uncoveredFields = 0;
            timerFlag = true;
            endGameFlag = true;
            isGameLost = false;
            BombsCoords.Clear();
            ClearBoard();
            PlantBombs();
            InitializeBoard();
        }
    }
}
