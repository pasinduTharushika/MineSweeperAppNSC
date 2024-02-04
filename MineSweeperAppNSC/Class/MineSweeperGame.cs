using MineSweeperAppNSC.Interface;
using System;

namespace MineSweeperAppNSC.Class
{
    public class MineSweeperGame : IMineSweeperGame
    {
        public int gridSize;
        public int numMines;
        private string inputGridSize;
        private string inputNumMines;
        private char[,] minefield;
        private char[,] revealedGrid;

        private readonly IMinefield _minefieldprop;
        private readonly IGrid _gridprop;

        public MineSweeperGame(IMinefield minefieldprop, IGrid gridprop)
        {
            // Dependency injection for Minefield and Grid interfaces
            _minefieldprop = minefieldprop;
            _gridprop = gridprop;
        }

        /// <summary>
        /// Initiates and runs the Minesweeper game.
        /// </summary>
        public void PlayGame()
        {
            try
            {
                Console.WriteLine("Welcome to Minesweeper!");

                // Get user input for grid size
                Console.Write("Enter the size (min 3, Max 10) of the grid (e.g., 5 for a 5x5 grid): ");
                inputGridSize = Console.ReadLine();
                gridSize = int.Parse(inputGridSize ?? "0");

                if (2 < gridSize && gridSize < 11)
                {
                    // Get user input for the number of mines
                    Console.Write("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
                    inputNumMines = Console.ReadLine();
                    numMines = int.Parse(inputNumMines ?? "0");
                    double result = (35.0 / 100) * (gridSize * gridSize);

                    if (numMines < result)
                    {
                        if (numMines > 0)
                        {
                            // Initialize minefield and revealed grid
                            minefield = _minefieldprop.InitializeMinefield(gridSize, numMines);
                            revealedGrid = _gridprop.InitializeRevealedGrid(gridSize);
                            bool gameOver = false;
                            int count = 0;

                            do
                            {
                                count++;
                                _gridprop.DisplayGrid(revealedGrid);
                                Console.Write("Select a square to reveal (e.g., A1): ");
                                string inputFirst = Console.ReadLine();
                                string input = inputFirst ?? "0";

                                // Validate and parse user input
                                if (input.Length == 2 && char.IsLetter(input[0]) && char.IsDigit(input[1]))
                                {
                                    int row = input[0] - 'A';
                                    int col = int.Parse(input[1].ToString()) - 1;
                                    if (minefield[row, col] == '*')
                                    {
                                        Console.WriteLine("Game over! You hit a mine!");
                                        gameOver = true;
                                    }
                                    else
                                    {
                                        // Uncover Grid
                                        _gridprop.UncoverSquare(minefield, revealedGrid, row, col);

                                        // Check the winning status 
                                        if (_minefieldprop.CheckForWin(revealedGrid, numMines))
                                        {
                                            Console.WriteLine("Congratulations! You won!");
                                            gameOver = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (count == 2)
                                    {
                                        gameOver = true;
                                    }
                                    Console.WriteLine("Incorrect input. Please enter a valid square (e.g., A1).");
                                }
                            } while (!gameOver);
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("There must be at least 1 mine.");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Maximum number is 35% of total squares.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    if (gridSize < 3)
                    {
                        Console.WriteLine("Minimum size of grid is 3");
                    }
                    else
                    {
                        Console.WriteLine("Maximum size of grid is 10");
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions.
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                PlayGame();
            }
        }
    }
}
