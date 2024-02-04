using MineSweeperAppNSC.Interface;
using System;

namespace MineSweeperAppNSC.Class
{
    public class Grid : IGrid
    {
        private readonly IMinefield _minefieldprop;

        public Grid(IMinefield minefieldprop)
        {
            // Dependency injection for Minefield interface
            _minefieldprop = minefieldprop;
        }

        /// <summary>
        /// Initializes the revealed grid with hidden squares.
        /// </summary>
        /// <param name="gridSize">Size of the grid.</param>
        /// <returns>The initialized revealed grid.</returns>
        public char[,] InitializeRevealedGrid(int gridSize)
        {
            try
            {
                char[,] revealedGrid = new char[gridSize, gridSize];

                // Initialize the revealed grid with hidden squares
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        revealedGrid[i, j] = '-';
                    }
                }

                return revealedGrid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Displays the current state of the grid.
        /// </summary>
        /// <param name="grid">The grid to display.</param>
        public void DisplayGrid(char[,] grid)
        {
            try
            {
                int size = grid.GetLength(0);

                // Display column headers
                Console.Write("  ");
                for (int i = 1; i <= size; i++)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                // Display minefield
                for (int i = 0; i < size; i++)
                {
                    Console.Write((char)('A' + i) + " ");
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write($"{grid[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Uncovers the selected square and recursively uncovers adjacent squares if no mines nearby.
        /// </summary>
        /// <param name="minefield">The minefield grid.</param>
        /// <param name="revealedGrid">The grid of revealed squares.</param>
        /// <param name="row">Row index of the selected square.</param>
        /// <param name="col">Column index of the selected square.</param>
        public void UncoverSquare(char[,] minefield, char[,] revealedGrid, int row, int col)
        {
            try
            {
                if (revealedGrid[row, col] == '-')
                {
                    int adjacentMines = _minefieldprop.CountAdjacentMines(minefield, row, col);
                    revealedGrid[row, col] = (char)(adjacentMines + '0');

                    // Recursive call to uncover adjacent squares if no mines nearby
                    if (adjacentMines == 0)
                    {
                        for (int i = row - 1; i <= row + 1; i++)
                        {
                            for (int j = col - 1; j <= col + 1; j++)
                            {
                                if (i >= 0 && i < minefield.GetLength(0) && j >= 0 && j < minefield.GetLength(1))
                                {
                                    UncoverSquare(minefield, revealedGrid, i, j);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
