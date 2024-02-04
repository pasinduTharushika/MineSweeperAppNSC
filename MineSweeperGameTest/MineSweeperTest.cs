using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperAppNSC.Class;
using MineSweeperAppNSC.Interface;
using Moq;
using System;
using System.IO;
using System.Text;

namespace MineSweeperGameTest
{
    [TestClass]
    public class MineSweeperTest
    {
        // Test case to verify the initialization of the revealed grid with a size of 5.
        [TestMethod]
        public void InitializeRevealedGrid_WithSize5_ReturnsCorrectGrid()
        {
            // Arrange
            int gridSize = 5;
            char[,] expectedGrid = new char[,]
            {
                { '-', '-', '-', '-', '-' },
                { '-', '-', '-', '-', '-' },
                { '-', '-', '-', '-', '-' },
                { '-', '-', '-', '-', '-' },
                { '-', '-', '-', '-', '-' }
            };

            // Act
            var insminefld = new Minefield();
            var insgrd = new Grid(insminefld);

            char[,] actualGrid = insgrd.InitializeRevealedGrid(gridSize);

            // Assert
            CollectionAssert.AreEqual(expectedGrid, actualGrid);
        }

        // Test case to verify the initialization of the revealed grid with a size of 3.
        [TestMethod]
        public void InitializeRevealedGrid_WithSize3_ReturnsCorrectGrid()
        {
            // Arrange
            int gridSize = 3;
            char[,] expectedGrid = new char[,]
            {
                { '-', '-', '-' },
                { '-', '-', '-' },
                { '-', '-', '-' }
            };

            // Act
            var insminefld = new Minefield();
            var insgrd = new Grid(insminefld);

            char[,] actualGrid = insgrd.InitializeRevealedGrid(gridSize);

            // Assert
            CollectionAssert.AreEqual(expectedGrid, actualGrid);
        }

        // Test case to verify the correct display of the grid.
        [TestMethod]
        public void DisplayGrid_ShouldDisplayCorrectly()
        {
            // Arrange
            char[,] grid = new char[,]
            {
                { '-', '-', '-' },
                { '-', '-', '-' },
                { '-', '-', '-' }
            };

            string expectedOutput = "  1 2 3 \r\nA - - - \r\nB - - - \r\nC - - - \r\n\r\n";

            // Create a StringWriter to redirect console output
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                var insminefld = new Minefield();
                var insgrd = new Grid(insminefld);
                insgrd.DisplayGrid(grid);

                // Assert
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }

        // Test case to verify the correct uncovering of a square in the grid.
        [TestMethod]
        public void UncoverSquare_ShouldUncoverCorrectly()
        {
            // Arrange
            char[,] minefield = new char[,]
            {
                { '0', '1', '0' },
                { '1', 'X', '1' },
                { '0', '1', '0' }
            };

            char[,] revealedGrid = new char[,]
            {
                { '-', '-', '-' },
                { '-', '-', '-' },
                { '-', '-', '-' }
            };

            int row = 1;
            int col = 1;
            var insminefld = new Minefield();
            var insgrd = new Grid(insminefld);

            // Act
            insgrd.UncoverSquare(minefield, revealedGrid, row, col);

            // Assert
            char[,] expectedRevealedGrid = new char[,]
            {
                { '0', '0', '0' },
                { '0', '0', '0' },
                { '0', '0', '0' }
            };

            CollectionAssert.AreEqual(expectedRevealedGrid, revealedGrid);
        }

        // Test case to verify the correct initialization of the minefield with a specified size and number of mines.
        [TestMethod]
        public void InitializeMinefield_ShouldCreateCorrectMinefield()
        {
            // Arrange
            int gridSize = 3;
            int numMines = 2;

            var insminefld = new Minefield();

            // Act
            char[,] minefield = insminefld.InitializeMinefield(gridSize, numMines);

            // Assert
            int countMines = 0;
            int countEmptySquares = 0;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (minefield[i, j] == '*')
                    {
                        countMines++;
                    }
                    else if (minefield[i, j] == ' ')
                    {
                        countEmptySquares++;
                    }
                }
            }

            Assert.AreEqual(numMines, countMines);
            Assert.AreEqual(gridSize * gridSize - numMines, countEmptySquares);
        }

        // Test case to verify the correct counting of adjacent mines for a given square in the minefield.
        [TestMethod]
        public void CountAdjacentMines_ShouldReturnCorrectCount()
        {
            // Arrange
            char[,] minefield = new char[,]
            {
                { '0', '1', '0' },
                { '1', '*', '1' },
                { '0', '1', '0' }
            };

            int row = 1;
            int col = 1;

            var insminefld = new Minefield();

            // Act
            int count = insminefld.CountAdjacentMines(minefield, row, col);

            // Assert
            Assert.AreEqual(1, count);
        }

        // Test case to verify the correct detection of a win condition based on the revealed grid and the number of mines.
        [TestMethod]
        public void CheckForWin_ShouldReturnTrueForWin()
        {
            // Arrange
            int numMines = 2;

            char[,] revealedGrid = new char[,]
            {
                { '1', '-', '1' },
                { '1', '*', '1' },
                { '1', '1', '1' }
            };

            var insminefld = new Minefield();

            // Act
            bool isWin = insminefld.CheckForWin(revealedGrid, numMines);

            // Assert
            Assert.IsTrue(isWin);
        }

        // Test case to verify the correct detection of a non-win condition based on the revealed grid and the number of mines.
        [TestMethod]
        public void CheckForWin_ShouldReturnFalseForNotWin()
        {
            // Arrange
            int numMines = 1;

            char[,] revealedGrid = new char[,]
            {
                { '1', '-', '1' },
                { '1', '*', '1' },
                { '1', '1', '1' }
            };

            var insminefld = new Minefield();

            // Act
            bool isWin = insminefld.CheckForWin(revealedGrid, numMines);

            // Assert
            Assert.IsFalse(isWin);
        }

        // Test case to verify the correct execution of the Minesweeper game with valid user input.
        [TestMethod]
        public void PlayGame_WithValidInput_ShouldInitializeGameAndHandleUserInput()
        {
            // Arrange
            var gridDisplayerMock = new Mock<IGrid>();
            var minefieldHandlerMock = new Mock<IMinefield>();

            minefieldHandlerMock.Setup(x => x.InitializeMinefield(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new char[3, 3]); // Mocked minefield

            gridDisplayerMock.Setup(x => x.InitializeRevealedGrid(It.IsAny<int>()))
                .Returns(new char[3, 3]); // Mocked revealed grid

            var minesweeperGame = new MineSweeperAppNSC.Class.MineSweeperGame(
                minefieldHandlerMock.Object, gridDisplayerMock.Object
            );

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader("5\n3\nA1\n"))
                {
                    Console.SetIn(sr);

                    // Act
                    minesweeperGame.PlayGame();

                    // Assert
                    Assert.IsTrue(sw.ToString().Contains("Welcome to Minesweeper!"));
                    minefieldHandlerMock.Verify(x => x.InitializeMinefield(5, 3), Times.Once);
                    gridDisplayerMock.Verify(x => x.InitializeRevealedGrid(5), Times.Once);
                }
            }
        }

        // Test case to verify the correct display of an error message when the user provides invalid input.
        [TestMethod]
        public void PlayGame_WithInvalidInput_ShouldDisplayErrorMessage()
        {
            // Arrange
            var gridDisplayerMock = new Mock<IGrid>();
            var minefieldHandlerMock = new Mock<IMinefield>();

            var minesweeperGame = new MineSweeperAppNSC.Class.MineSweeperGame(
                minefieldHandlerMock.Object, gridDisplayerMock.Object);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader("2\n5\n"))
                {
                    Console.SetIn(sr);

                    // Act
                    minesweeperGame.PlayGame();

                    // Assert
                    Assert.IsTrue(sw.ToString().Contains("Minimum size of grid is 3"));
                }
            }
        }
    }
}
