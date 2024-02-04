using MineSweeperAppNSC.Interface;
using System;

namespace MineSweeperAppNSC.Class
{
    public class Minefield : IMinefield
    {
        /// <summary>
        /// Initializes the minefield with empty squares and randomly places mines.
        /// </summary>
        /// <param name="gridSize">Size of the minefield grid.</param>
        /// <param name="numMines">Number of mines to be placed on the grid.</param>
        /// <returns>The initialized minefield.</returns>
        public char[,] InitializeMinefield(int gridSize, int numMines)
        {
            try
            {
                char[,] minefield = new char[gridSize, gridSize];

                // Initialize the minefield with empty squares
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        minefield[i, j] = ' ';
                    }
                }

                // Place mines randomly
                Random random = new Random();
                for (int i = 0; i < numMines; i++)
                {
                    int row, col;
                    do
                    {
                        row = random.Next(gridSize);
                        col = random.Next(gridSize);
                    } while (minefield[row, col] == '*');

                    minefield[row, col] = '*';
                }

                return minefield;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Counts the number of adjacent mines to the specified square.
        /// </summary>
        /// <param name="minefield">The minefield grid.</param>
        /// <param name="row">Row index of the square.</param>
        /// <param name="col">Column index of the square.</param>
        /// <returns>The number of adjacent mines.</returns>
        public int CountAdjacentMines(char[,] minefield, int row, int col)
        {
            try
            {
                int count = 0;
                int gridSize = minefield.GetLength(0);

                for (int i = row - 1; i <= row + 1; i++)
                {
                    for (int j = col - 1; j <= col + 1; j++)
                    {
                        if (i >= 0 && i < gridSize && j >= 0 && j < gridSize && minefield[i, j] == '*')
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Checks if the player has won the game by uncovering all safe squares.
        /// </summary>
        /// <param name="revealedGrid">The grid of revealed squares.</param>
        /// <param name="numMines">Number of mines on the minefield.</param>
        /// <returns>True if the player has won, false otherwise.</returns>
        public bool CheckForWin(char[,] revealedGrid, int numMines)
        {
            try
            {
                int gridSize = revealedGrid.GetLength(0);
                int numUncovered = 0;

                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        if (revealedGrid[i, j] != '-' && revealedGrid[i, j] != '*')
                        {
                            numUncovered++;
                        }
                    }
                }

                return numUncovered == (gridSize * gridSize - numMines);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
