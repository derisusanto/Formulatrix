﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 38);
            Console.BufferHeight = 38;
            Console.BufferWidth = (80);
            while (true)
            {
                Game game = Menu.WriteMenu();
                if (game.Difficulty == 6)
                {
                    game.GodMode();
                }
                Console.WriteLine("\n\n");
                while (game.IsGameOver() != true)
                {
                    Grid.WriteGrid(game.Board.PlayerLayer);
                    game.Board.Shoot();
                }
                if (game.Board.BackToMenu == 0)
                {
                    game.GameOver();
                    Console.WriteLine("\nPress Enter to exit to menu");
                    string final = Console.ReadLine();
                    if (final.ToLower() == "exit")
                    {
                        Menu.exit();
                    }
                }
                Console.Clear();
            }
        }
    }
    class Game
    {
        public Gameboard Board { get; set; }
        public int Difficulty { get; set; }

        public Game()
        {
            Board = new Gameboard();
            Difficulty = 0;
            Console.Clear();
        }
        public Game(int difficulty)
        {
            Board = new Gameboard();
            Difficulty = difficulty;
            SetRocketValue(difficulty);
            Console.Clear();
        }
        public bool IsGameOver()
        {
            if (Board.BackToMenu == 1)
            {
                return true;
            }
            if (Board.Rockets <= 0)
            {
                return true;
            }
            if (AllBoatsSunk(Board))
            {
                return true;
            }
            return false;
        }
        public void GameOver()
        {
            if (AllBoatsSunk(Board))
            {
                Grid.WriteGrid(Board.PlayerLayer);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(String.Format("\n{0}\n{1}\n{2}\n{3}",
                    "  ----------------------------",
                    " | 01011001 01001111 01010101 |",
                    " | 01010111 01001111 01001110 |",
                    "  ----------------------------\n"));
                Console.ForegroundColor = currentColor;
                Grid.WriteGrid(Board.BoatLayer);
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("CONGRATULATIONS!");
                int usedRockets = GetUsedRockets();
                Console.WriteLine("You won with " + usedRockets + " Rockets");
                Console.ForegroundColor = currentColor;

            }
            else if (Board.Rockets <= 0)
            {
                Console.Clear();
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("GAME OVER\n");
                Console.ForegroundColor = currentColor;
                Console.WriteLine("Better luck next time.");
                foreach (Boat boat in Board.BoatLayer.Boats)
                {
                    if (boat.Sunk == false)
                    {
                        boat.ShowBoat(Board.PlayerLayer);
                    }
                }
                Grid.WriteGrid(Board.PlayerLayer);
            }
        }
        public void GodMode()
        {
            Console.Clear();
            Console.WriteLine("GET READY");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("3");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("2");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("1");
            System.Threading.Thread.Sleep(1000);
            Grid.WriteGrid(Board.BoatLayer);
            System.Threading.Thread.Sleep(2500);
            Console.Clear();
        }

        private int GetUsedRockets()
        {
            int currentRockets = Board.Rockets;
            SetRocketValue(Difficulty);
            int diff = Board.Rockets - currentRockets;
            return diff;
        }
        private bool AllBoatsSunk(Gameboard board)
        {
            foreach (Boat boat in board.BoatLayer.Boats)
            {
                if (boat.Sunk != true)
                {
                    return false;
                }
            }
            return true;
        }
        private int SetRocketValue(int difficulty)
        {
            switch (Difficulty)
            {
                case 1:
                    Board.Rockets = 70;
                    break;
                case 2:
                    Board.Rockets = 60;
                    break;
                case 3:
                    Board.Rockets = 50;
                    break;
                case 4:
                    Board.Rockets = 35;
                    break;
                case 5:
                    Board.Rockets = 43;
                    break;
                case 6:
                    Board.Rockets = 20;
                    break;
                default:
                    Board.Rockets = 60;
                    break;
            }
            return Board.Rockets;
        }
    }
    class Menu
    {
        public static Game WriteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            WriteTitle();
            LoopOptions();
            int difficulty = Difficulty();
            Game game = new Game(difficulty);
            return game;
        }

        private static int Difficulty()
        {
            Console.Clear();
            bool proceed = false;
            while (proceed == false)
            {
                Console.WriteLine("Select your difficulty:");
                Console.WriteLine("1 = Easy");
                Console.WriteLine("2 = Medium");
                Console.WriteLine("3 = Hard");
                Console.WriteLine("4 = Insane");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "1":
                        proceed = true;
                        return 1;
                    case "2":
                        proceed = true;
                        return 2;
                    case "3":
                        proceed = true;
                        return 3;
                    case "4":
                        proceed = true;
                        return 4;
                    case "harder":
                        proceed = true;
                        return 5;
                    case "god":
                        proceed = true;
                        return 6;
                    case "exit":
                        exit();
                        break;
                    case "back":
                    case "menu":
                        Console.Clear();
                        WriteTitle();
                        LoopOptions();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Let's try that again shall we..\n");
                        proceed = false;
                        break;
                }
            }
            return 0;
        }

        private static void LoopOptions()
        {
            bool notContinue = true;
            while (notContinue)
            {
                Console.WriteLine("1 - New Game (or press Enter)");
                Console.WriteLine("2 - Exit");
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "":
                        notContinue = false;
                        break;
                    case "1":
                        Console.Clear();
                        notContinue = false;
                        break;
                    case "2":
                        exit();
                        break;
                    case "exit":
                        exit();
                        break;
                    default:
                        Console.WriteLine("Please enter in 1 for a new game, or 2 to exit.");
                        Console.WriteLine("You can also type exit\n");
                        break;
                }
            }
        }

        public static void exit()
        {
            Environment.Exit(0);
        }

        private static void WriteTitle()
        {
            string nameOfTheGame = @"
        _           _   _   _           _     _       
       | |         | | | | | |         | |   (_)      
       | |__   __ _| |_| |_| | ___  ___| |__  _ _ __  
       | '_ \ / _` | __| __| |/ _ \/ __| '_ \| | '_ \ 
       | |_) | (_| | |_| |_| |  __/\__ \ | | | | |_) |
       |_.__/ \__,_|\__|\__|_|\___||___/_| |_|_| .__/ 
                                               | |    
                                               |_|  ";

            string ship = @"                                        ___
                                       __|
                                  ______|_|
                           _   __|_________|  _
            _        =====| | |            | | |==== _
      =====| |        .---------------------------. | |====
<-----------------'   .  .  .  .  .  .  .  .   '------------/
  \                                                        /
   \                                                      /
    \____________________________________________________/
";
            Console.WriteLine(nameOfTheGame);
            Console.WriteLine(ship);
        }
    }

    class Gameboard
    {
        public Grid BoatLayer { get; set; }
        public Grid PlayerLayer { get; set; }
        private ConsoleColor Background { get; set; }
        private ConsoleColor Foreground { get; set; }
        public int Rockets { get; set; }
        public int BackToMenu { get; set; }

        public Gameboard(Grid boatGrid, Grid playerGrid)
        {
            BoatLayer = boatGrid;
            PlayerLayer = playerGrid;
            Rockets = 10;
            BackToMenu = 0;
            Foreground = ConsoleColor.White;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }
        public Gameboard()
        {
            Grid playerGrid = new Grid();
            PlayerLayer = playerGrid;
            Rockets = 40;
            BackToMenu = 0;
            Grid boatGrid = new Grid(10, 10, ' ');
            List<Boat> boatList = Boat.GenerateBoats();
            Boat.PlaceBoats(boatList, boatGrid);
            BoatLayer = boatGrid;
            Foreground = ConsoleColor.Gray;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }
        public void Shoot()
        {
            string coordinates = "";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Rockets left: " + Rockets + "\n");
            Console.ForegroundColor = Foreground;
            Console.WriteLine("Enter in the coordinates for your shot:");
            coordinates = Console.ReadLine();
            if (coordinates.ToLower() == "menu"
                || coordinates.ToLower() == "back")
            {
                BackToMenu = 1;
            }
            while (isShotValid(coordinates) != true
                && BackToMenu == 0
                || isShotUsedBefore(coordinates) == true 
                && BackToMenu == 0)
            {
                Console.Clear();
                Console.WriteLine("\n\n");
                Grid.WriteGrid(PlayerLayer);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Rockets left: " + Rockets + "\n");
                Console.ForegroundColor = Foreground;
                if (isShotUsedBefore(coordinates))
                {
                    Console.WriteLine(String.Format("{0}: {1}",
                        coordinates,
                        "You already shot there... Try again\n"));
                }
                else
                {
                    Console.WriteLine(String.Format("'{0}' {1}",
                        coordinates,
                        "is not a valid location.\n"));
                }
                coordinates = Console.ReadLine();
            }
            if (BackToMenu == 0)
            {
                Console.Clear();
                Rockets--;
                WriteShot(coordinates);
            }
        }
        private void WriteShot(string shot)
        {
            int col = GetColumnNumber(shot[1]);
            int row = GetRowNumber(shot[0]);
            bool hit = isShotHit(shot);
            if (hit)
            {
                this.PlayerLayer.Matrix[row, col] = 'x';
                Boat hitBoat = HitBoat(shot);
            }
            else
            {
                this.PlayerLayer.Matrix[row, col] = ' ';
            }
        }
        private Boat HitBoat(string shot)
        {
            int col = GetColumnNumber(shot[1]);
            int row = GetRowNumber(shot[0]);
            Boat hitBoat;
            foreach (Boat boat in BoatLayer.Boats)
            {
                hitBoat = boat;
                bool rowMatch = false;
                for (int i = 0; i < boat.Length; i++)
                {
                    for (int ii = 0; ii < 2; ii++)
                    {
                        if (ii == 0 && boat.Coordinates[i, ii] == row)
                        {
                            rowMatch = true;
                        }
                        if (ii == 1 &&
                            rowMatch &&
                            boat.Coordinates[i, ii] == col)
                        {
                            hitBoat.Hits++;
                            IsBoatSunk(hitBoat);
                            return hitBoat;
                        }
                    }
                }
            }
            throw new ArgumentException("No hit boat");
        }

        private bool IsBoatSunk(Boat hitBoat)
        {
            if (hitBoat.Hits >= hitBoat.Length)
            {
                hitBoat.Sunk = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("You sunk the " + hitBoat.Name + "!");
                Console.ForegroundColor = Foreground;
                return true;
            }
            return false;
        }
        private bool isShotHit(string shot)
        {
            if (BoatLayer.Matrix[GetRowNumber(shot[0]),
                GetColumnNumber(shot[1])] != BoatLayer.Symbol)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(shot + " HIT!" + "\n\n");
                Console.ForegroundColor = Foreground;
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(shot + " MISSED!" + "\n\n");
            Console.ForegroundColor = Foreground;
            return false;
        }
        private bool isShotUsedBefore(string shot)
        {
            if (isShotValid(shot) != true)
            {
                return false;
            }
            int row = GetRowNumber(shot[0]);
            int col = GetColumnNumber(shot[1]);
            if (PlayerLayer.Matrix[row, col] == ' ' ||
                PlayerLayer.Matrix[row, col] == 'x')
            {
                return true;
            }
            return false;
        }

        private bool isShotValid(string shot)
        {
            if (isInputValid(shot) != true)
            {
                return false;
            }
            int row = GetRowNumber(shot[0]);
            int column = GetColumnNumber(shot[1]);
            if (row > 0 &&
                column > 0 &&
                row < PlayerLayer.Matrix.GetLength(0) &&
                column < PlayerLayer.Matrix.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isInputValid(string shot)
        {
            shot = shot.Replace(" ", "");
            if (shot == "" || shot == " " || shot == null)
            {
                return false;
            }
            if (shot.ToLower() == "exit")
            {
                Menu.exit();
            }
            string pattern = @"^[A-ja-j]{1}[0-9]{1}$";
            Regex reg = new Regex(pattern);
            Match match = reg.Match(shot);
            if (match.Value != shot)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private int GetColumnNumber(char column)
        {
            string strColumn = (column).ToString();
            return int.Parse(strColumn) + 1;
        }
        private static int GetRowNumber(char letter)
        {
            int index = char.ToUpper(letter) - 64;
            return index;
        }
    }

    class Boat
    {
        public int Length { get; private set; }
        public string Name { get; set; }
        public char[,] Position { get; set; }
        public int[,] Coordinates { get; set; }
        public int Orientation { get; set; }
        public int[] Seed { get; set; }
        public char Symbol { get; set; }
        public int Hits { get; set; }
        public bool Sunk { get; set; }
        private static Random rand = new Random();

        public Boat(int length, string name)
        {
            this.Length = length;
            this.Hits = 0;
            this.Sunk = false;
            this.Name = name;
            this.Orientation = rand.Next(0, 2);
            this.Symbol = 'b';
        }

        public Boat(int length, string name, char symbol)
        {
            this.Length = length;
            this.Hits = 0;
            this.Sunk = false;
            this.Name = name;
            this.Orientation = rand.Next(0, 2);
            this.Symbol = symbol;
        }

        public static List<Boat> GenerateBoats()
        {
            List<Boat> boatList = new List<Boat>();
            boatList.Add(new Boat(3, "Submarine", 'S'));
            boatList.Add(new Boat(2, "Patrol Boat", 'P'));
            boatList.Add(new Boat(5, "Carrier", 'C'));
            boatList.Add(new Boat(4, "Battle Ship", 'B'));
            boatList.Add(new Boat(3, "Destroyer", 'D'));
            return boatList;
        }
        public static void PlaceBoats(List<Boat> boats, Grid boatGrid)
        {
            foreach (Boat boat in boats)
            {
                boat.PlaceBoat(boatGrid);
            }
        }
        private static int[] GetBoatSeed(char[,] Grid)
        {
            int height = Grid.GetLength(0);
            int width = Grid.GetLength(1);
            int w = rand.Next(1, width);
            int h = rand.Next(1, height);
            int[] seedCoord = { h, w };
            return seedCoord;
        }

        private static void VerticalBoat(Boat boat, Grid grid)
        {
            boat.Seed = GetBoatSeed(grid.Matrix);

            if (boat.Seed[0] + boat.Length > grid.Matrix.GetLength(0))
            {
                if (!(boat.Seed[0] - boat.Length < 1))
                {
                    VerticalBoatUp(boat, grid);
                }
                else
                {
                    throw new ArgumentException(
                       "Bad Seed: " + boat.Seed[0] + "x" + boat.Seed[1]);
                }
            }
            else
            {
                VerticalBoatDown(boat, grid);
            }
        }
        private static void VerticalBoatUp(Boat boat, Grid grid)
        {
            int[,] coords = new int[boat.Length, 2];
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            for (int i = boat.Seed[0]; i > boat.Seed[0] - boat.Length; i--)
            {
                position[i, boat.Seed[1]] = boat.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = boat.Seed[0] - ii;
                    }
                    else
                    {
                        coords[ii, iii] = boat.Seed[1];
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        private static void VerticalBoatDown(Boat boat, Grid grid)
        {
            int[,] coords = new int[boat.Length, 2];
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            for (int i = boat.Seed[0]; i < boat.Seed[0] + boat.Length; i++)
            {
                position[i, boat.Seed[1]] = boat.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = boat.Seed[0] + ii;
                    }
                    else
                    {
                        coords[ii, iii] = boat.Seed[1];
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        private static void HorizontalBoat(Boat boat, Grid grid)
        {
            boat.Seed = GetBoatSeed(grid.Matrix);
            if (boat.Seed[1] + boat.Length > grid.Matrix.GetLength(1))
            {
                if (!(boat.Seed[1] - boat.Length < 1))
                {
                    HorizontalBoatLeft(boat, grid);
                }
                else
                {
                    throw new ArgumentException(
                        "Bad Seed: " + boat.Seed[0] + "x" + boat.Seed[1]);
                }
            }
            else
            {
                HorizontalBoatRight(boat, grid);
            }
        }

        private static void HorizontalBoatRight(Boat boat, Grid grid)
        {
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            int[,] coords = new int[boat.Length, 2];
            for (int i = boat.Seed[1]; i < boat.Seed[1] + boat.Length; i++)
            {
                for (int ii = 0; ii < coords.GetLength(0); ii++)
                {
                    position[boat.Seed[0], i] = boat.Symbol;
                }
                for (int ii = 0; ii < coords.GetLength(0); ii++)
                {
                    for (int iii = 0; iii < 2; iii++)
                    {
                        if (iii == 0)
                        {
                            coords[ii, iii] = boat.Seed[0];
                        }
                        else
                        {
                            coords[ii, iii] = boat.Seed[1] + ii;
                        }
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        private static void HorizontalBoatLeft(Boat boat, Grid grid)
        {
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            int[,] coords = new int[boat.Length, 2];
            for (int i = boat.Seed[1]; i > boat.Seed[1] - boat.Length; i--)
            {
                position[boat.Seed[0], i] = boat.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = boat.Seed[0];
                    }
                    else
                    {
                        coords[ii, iii] = boat.Seed[1] - ii;
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        public Grid PlaceBoat(Boat boat, Grid grid)
        {
            if (boat.Length > grid.Matrix.GetLength(0) ||
                boat.Length > grid.Matrix.GetLength(1))
            {
                throw new ArgumentException(
                    "The 'Boat' length must be smaller than the 'Grid' dimensions");
            }
            if (boat.Orientation == 1)
            {
                Boat.HorizontalBoat(boat, grid);
            }
            else
            {
                Boat.VerticalBoat(boat, grid);
            }
            while (!IsBoatPositionValid(boat, grid))
            {
                boat.Seed = GetBoatSeed(grid.Matrix);
                if (boat.Orientation == 1)
                {
                    Boat.HorizontalBoat(boat, grid);
                }
                else
                {
                    Boat.VerticalBoat(boat, grid);
                }
                IsBoatPositionValid(boat, grid);
            }
            for (int i = 0; i < boat.Position.GetLength(0); i++)
            {
                for (int ii = 0; ii < boat.Position.GetLength(1); ii++)
                {
                    if (boat.Position[i, ii] == boat.Symbol)
                    {
                        grid.Matrix[i, ii] = boat.Symbol;
                    }
                }
            }
            grid.Boats.Add(boat);
            return grid;
        }

        public void ShowBoat(Grid grid)
        {
            for (int i = 0; i < this.Position.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.Position.GetLength(1); ii++)
                {
                    if (this.Position[i, ii] == Symbol)
                    {
                        if (grid.Matrix[i, ii] != 'x')
                        {
                            grid.Matrix[i, ii] = this.Symbol;
                        }
                    }
                }
            }
        }

        public Grid PlaceBoat(Grid grid)
        {
            if (this.Length > grid.Matrix.GetLength(0) ||
                this.Length > grid.Matrix.GetLength(1))
            {
                throw new ArgumentException(
                    "The 'Boat' length must be smaller than the 'Grid' dimensions");
            }
            if (this.Orientation == 1)
            {
                Boat.HorizontalBoat(this, grid);
            }
            else
            {
                Boat.VerticalBoat(this, grid);
            }
            while (!IsBoatPositionValid(this, grid))
            {
                Seed = GetBoatSeed(grid.Matrix);
                if (Orientation == 1)
                {
                    Boat.HorizontalBoat(this, grid);
                }
                else
                {
                    Boat.VerticalBoat(this, grid);
                }
                IsBoatPositionValid(this, grid);
            }
            for (int i = 0; i < this.Position.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.Position.GetLength(1); ii++)
                {
                    if (this.Position[i, ii] == Symbol)
                    {
                        grid.Matrix[i, ii] = this.Symbol;
                    }
                }
            }
            grid.Boats.Add(this);
            return grid;
        }

        private static bool IsBoatPositionValid(Boat boat, Grid grid)
        {
            for (int i = 0; i < grid.Matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.Matrix.GetLength(1); ii++)
                {
                    if (grid.Matrix[i, ii] != grid.Symbol &&
                        boat.Position[i, ii] == boat.Symbol)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void WriteCoordinates()
        {
            for (int i = 0; i < Coordinates.GetLength(0); i++)
            {
                for (int ii = 0; ii < Coordinates.GetLength(1); ii++)
                {
                    if (ii == 0)
                    {
                        Console.Write("Coords: " + Coordinates[i, ii] + "x");
                    }
                    else
                    {
                        Console.WriteLine(Coordinates[i, ii]);
                    }
                }
            }
        }
    }

    class Grid
    {
        public char Symbol { get; set; }
        public char[,] Matrix { get; set; }
        public List<Boat> Boats { get; set; }

        public Grid()
        {
            char[,] grid = GenerateGrid(10, 10);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = '#';
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }

        public Grid(char symbol)
        {
            char[,] grid = GenerateGrid(10, 10, symbol);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = symbol;
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }
        public Grid(int height, int width)
        {
            char[,] grid = GenerateGrid(height, width);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = '#';
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }

        public Grid(int height, int width, char symbol)
        {
            char[,] grid = GenerateGrid(height, width, symbol);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = symbol;
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }

        private static char[,] GenerateGrid(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("Must be positive values");
            }
            x = x + 1;
            y = y + 1;
            char[,] grid = new System.Char[x, y];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.GetLength(1); ii++)
                {
                    if (i == 0 && ii == 0)
                    {
                        grid[i, ii] = ' ';
                    }
                    else if (i == 0 && ii > 0)
                    {
                        string num = (ii - 1).ToString();
                        grid[i, ii] = Convert.ToChar(num);
                    }
                    else if (ii == 0 && i > 0)
                    {
                        grid[i, ii] = GetColumnName(i);
                    }
                    else
                    {
                        grid[i, ii] = '#';
                    }
                }
            }
            return grid;
        }

        private static char[,] GenerateGrid(int x, int y, char symbol)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("Must be positive values");
            }
            x = x + 1;
            y = y + 1;
            char[,] grid = new System.Char[x, y];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.GetLength(1); ii++)
                {
                    if (i == 0 && ii == 0)
                    {
                        grid[i, ii] = symbol;
                    }
                    else if (i == 0 && ii > 0)
                    {
                        string num = (ii - 1).ToString();
                        grid[i, ii] = Convert.ToChar(num);
                    }
                    else if (ii == 0 && i > 0)
                    {
                        grid[i, ii] = GetColumnName(i);
                    }
                    else
                    {
                        grid[i, ii] = symbol;
                    }
                }
            }
            return grid;
        }


        public static void WriteGrid(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < matrix.GetLength(1); ii++)
                {
                    Console.Write(matrix[i, ii] + "  ");
                }
                Console.WriteLine("\n");
            }
        }
        public static void WriteGrid(Grid grid)
        {
            for (int i = 0; i < grid.Matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.Matrix.GetLength(1); ii++)
                {
                    if (grid.Matrix[i, ii] == 'x')
                    {
                        ConsoleColor curColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(grid.Matrix[i, ii] + "  ");
                        Console.ForegroundColor = curColor;
                    }
                    else if (grid.Matrix[i, ii] != 'x' &&
                        grid.Matrix[i, ii] != ' ' &&
                        grid.Matrix[i, ii] != grid.Symbol &&
                        i != 0 && ii != 0)
                    {
                        ConsoleColor curColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(grid.Matrix[i, ii] + "  ");
                        Console.ForegroundColor = curColor;
                    }
                    else
                    {
                        Console.Write(grid.Matrix[i, ii] + "  ");
                    }
                }
                Console.WriteLine("\n");
            }
        }

        public void WriteGrid()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < Matrix.GetLength(1); ii++)
                {
                    Console.Write(Matrix[i, ii] + "  ");
                }
                Console.WriteLine("\n");
            }
        }

        private static char GetColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            char columnName = ' ';
            int modulo;

            while (dividend > 0 && dividend < 27)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo);
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private static bool IsValidDimension(string dimension)
        {
            bool result = false;
            dimension = dimension.Replace(" ", "");
            string pattern = @"^[0-9]{1,2}x[0-9]{1,2}$";
            Regex reg = new Regex(pattern);
            Match match = reg.Match(dimension);
            if (match.Value == dimension)
            {
                result = true;
            }
            return result;
        }

        private static int maxGridSize = 297;
        private static int minGridSize = 30;
        private static int maxHeight = 27;
        private static int maxWidth = 11;

        private static bool IsGridValidRange(char[,] matrix)
        {
            int h = matrix.GetLength(0);
            int w = matrix.GetLength(1);
            if (h <= 1 || w <= 1 || w > maxWidth || h > maxHeight)
            {
                return false;
            }
            if (h < 5 || w < 5)
            {
                return false;
            }
            if (matrix.Length > maxGridSize || matrix.Length < minGridSize)
            {
                return false;
            }

            return true;
        }
    }
}