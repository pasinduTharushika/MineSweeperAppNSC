using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperAppNSC.Interface
{
    public interface IMinefield
    {
        char[,] InitializeMinefield(int gridSize, int numMines);
        int CountAdjacentMines(char[,] minefield, int row, int col);
        bool CheckForWin(char[,] revealedGrid, int numMines);
    }
}
