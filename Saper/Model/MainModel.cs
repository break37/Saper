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
        private bool endGameFlag = true;
        private bool timerFlag = true;
        private int startTime;

        #endregion


        #region Properties

        public string[,] GameBoard { get; set; }
        public string[,] PlayerBoard { get; set; }
        public int Size { get; } = 10;
        private int flagsMarked { get; set; } = 0;

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
                            GameBoard[x + i, y + j] = (Convert.ToInt16(GameBoard[x + i, y + j]) + 1).ToString();
                        }
                        catch (Exception)
                        {
                            //exception means, that conversion was unsuccessful, because element was a bomb ("#")
                            //or index was out of range, which again makes no problem
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
                flagsMarked++;
            }
            else if(PlayerBoard[y, x] == "F")
            {
                PlayerBoard[y, x] = " ";
                tile.Add(new Tile(x, y, $@"bitmaps\field .bmp"));
                flagsMarked--;
            }

            return tile;
        }

        public List<Tile> ShowFields(int x, int y)
        {
            //coordinates comes as a cursor position value, so to transform them into table indices its necessary to divide them by 25
            x = x / 25;
            y = y / 25;

            return FindNextEmpty(x, y);
        }

        private List<Tile> FindNextEmpty(int x, int y)
        {
            List<Tile> uncovered = new List<Tile>();
            try
            {
                if (GameBoard[y, x] == "0" && PlayerBoard[y, x] == " ")
                {
                    PlayerBoard[y, x] = GameBoard[y, x];
                    uncovered.Add(new Tile(x, y, $@"bitmaps\field{PlayerBoard[y, x]}.bmp"));
                    //if (x < Size - 1) FindNextEmpty(x++, y);
                    //if (y > 0) FindNextEmpty(x, y--);
                    //if (x > 0) FindNextEmpty(x--, y);
                    //if (y < Size - 1) FindNextEmpty(x, y++);
                    //if (x < Size - 1 && y < Size - 1) FindNextEmpty(x++, y++);
                    //if (x > 0 && y > 0) FindNextEmpty(x--, y--);
                    //if (x > 0 && y < Size - 1) FindNextEmpty(x--, y++);
                    //if (x < Size - 1 && y > 0) FindNextEmpty(x++, y--);

                    foreach (Tile tile in FindNextEmpty(x++, y))
                        uncovered.Add(tile);

                    foreach (Tile tile in FindNextEmpty(x, y--))
                        uncovered.Add(tile);

                    foreach (Tile tile in FindNextEmpty(x--, y))
                        uncovered.Add(tile);

                    foreach (Tile tile in FindNextEmpty(x, y++))
                        uncovered.Add(tile);

                    foreach (Tile tile in FindNextEmpty(x++, y++))
                        uncovered.Add(tile);

                    foreach (Tile tile in FindNextEmpty(x--, y--))
                        uncovered.Add(tile);

                    foreach (Tile tile in FindNextEmpty(x--, y++))
                        uncovered.Add(tile);

                    foreach (Tile tile in FindNextEmpty(x++, y--))
                        uncovered.Add(tile);
                }
                else if (PlayerBoard[y, x] != "F" && PlayerBoard[y, x] != "#")
                {
                    PlayerBoard[y, x] = GameBoard[y, x];
                    if (PlayerBoard[y, x] == "#") uncovered.Add(new Tile(x, y, $@"bitmaps\field#red.bmp"));
                    else uncovered.Add(new Tile(x, y, $@"bitmaps\field{PlayerBoard[y, x]}.bmp"));
                }
            }
            catch (Exception exc)
            {
                //IndexOutOfRangeException, no problem
                Console.WriteLine($"Exception [{x}, {y}] - {exc.Message}");
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

        public void ResetGame()
        {
            flagsMarked = 0;
            timerFlag = true;
            endGameFlag = true;
            ClearBoard();
            PlantBombs();
            InitializeBoard();
        }
    }
}
