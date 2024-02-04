using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperAppNSC.Interface
{
    public interface IGrid
    {
        char[,] InitializeRevealedGrid(int gridSize);
        void DisplayGrid(char[,] grid);
        void UncoverSquare(char[,] minefield, char[,] revealedGrid, int row, int col);
    }
}
