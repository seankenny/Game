using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Game.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Cell_SetCellWithInvalidColours_ReturnsFalse()
        {
            var cell = new Cell(ColourEnum.Red, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Blue);

            var result = cell.IsValid(new List<ColourEnum> { ColourEnum.Red, ColourEnum.Red, ColourEnum.Red, ColourEnum.Red });

            Assert.IsFalse(result);
        }

        [Test]
        public void Cell_SetCellWithValidColoursInSequence_ReturnsTrue()
        {
            var cell = new Cell(ColourEnum.Red, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Blue);

            var result = cell.IsValid(new List<ColourEnum> { ColourEnum.Red, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Blue });

            Assert.IsTrue(result);
        }

        [Test]
        public void Cell_SetCellWithValidColoursOutOfSequence_ReturnsTrue()
        {
            var cell = new Cell(ColourEnum.Red, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Blue);

            var result = cell.IsValid(new List<ColourEnum> { ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Blue, ColourEnum.Red });
            var result2 = cell.IsValid(new List<ColourEnum> { ColourEnum.Yellow, ColourEnum.Blue, ColourEnum.Red, ColourEnum.Red });
            var result3 = cell.IsValid(new List<ColourEnum> { ColourEnum.Blue, ColourEnum.Red, ColourEnum.Red, ColourEnum.Yellow });

            Assert.IsTrue(result);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
        }

        [Test]
        public void Grid_GetValidColours_ReturnsArrayOfValidColours()
        {
            /*
             *              RED
             *      BLUE    CELL (1,1)    YELLOW
             *              GREEN
             */
            var grid = new Grid(3);
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.Blue, ColourEnum.None), 0, 1); // l
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.None, ColourEnum.Red), 1, 2); // t
            grid.SetCell(new Cell(ColourEnum.Yellow, ColourEnum.None, ColourEnum.None, ColourEnum.None), 2, 1); // r
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.Green, ColourEnum.None, ColourEnum.None), 1, 0); // b

            var result = grid.GetValidColours(1, 1);

            Assert.IsTrue(result.SequenceEqual(new List<ColourEnum> { ColourEnum.Blue, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Green }));
        }

        [Test]
        public void GridSetCell_ValidCell_ReturnsTrue()
        {
            /*
             *              RED
             *      BLUE    CELL (1,1)    YELLOW
             *              GREEN
             */
            var grid = new Grid(3);
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.Blue, ColourEnum.None), 0, 1); // l
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.None, ColourEnum.Red), 1, 2); // t
            grid.SetCell(new Cell(ColourEnum.Yellow, ColourEnum.None, ColourEnum.None, ColourEnum.None), 2, 1); // r
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.Green, ColourEnum.None, ColourEnum.None), 1, 0); // b

            var result = grid.SetCell(new Cell(ColourEnum.Blue, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Green), 1, 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void GridSetCell_ValidCellRequiringRotation_ReturnsTrue()
        {
            /*
             *              RED
             *      BLUE    CELL (1,1)    YELLOW
             *              GREEN
             */
            var grid = new Grid(3);
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.Blue, ColourEnum.None), 0, 1); // l
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.None, ColourEnum.Red), 1, 2); // t
            grid.SetCell(new Cell(ColourEnum.Yellow, ColourEnum.None, ColourEnum.None, ColourEnum.None), 2, 1); // r
            grid.SetCell(new Cell(ColourEnum.None, ColourEnum.Green, ColourEnum.None, ColourEnum.None), 1, 0); // b

            var result = grid.SetCell(new Cell(ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Green, ColourEnum.Blue), 1, 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void GridRun_ValidCells_ReturnsTrue()
        {
            var inputCells = new List<Cell>();

            // row 0
            inputCells.Add(new Cell(ColourEnum.Blue, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Red));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Red, ColourEnum.Blue, ColourEnum.Yellow));
            inputCells.Add(new Cell(ColourEnum.Blue, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Red));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Blue, ColourEnum.Red, ColourEnum.Blue));
            
            // row 1
            inputCells.Add(new Cell(ColourEnum.Red, ColourEnum.Blue, ColourEnum.Red, ColourEnum.Blue));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Blue, ColourEnum.Yellow, ColourEnum.Red));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Red));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Blue));

            // row 2
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Blue, ColourEnum.Blue, ColourEnum.Yellow));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Blue));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Red, ColourEnum.Yellow, ColourEnum.Red));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Blue, ColourEnum.Blue, ColourEnum.Red));
            
            // row 3
            inputCells.Add(new Cell(ColourEnum.Blue, ColourEnum.Red, ColourEnum.Blue, ColourEnum.Yellow));
            inputCells.Add(new Cell(ColourEnum.Yellow, ColourEnum.Blue, ColourEnum.Blue, ColourEnum.Red));
            inputCells.Add(new Cell(ColourEnum.Blue, ColourEnum.Yellow, ColourEnum.Red, ColourEnum.Red));
            inputCells.Add(new Cell(ColourEnum.Red, ColourEnum.Blue, ColourEnum.Yellow, ColourEnum.Blue));

            var grid = new Grid(6, true);
            grid.Fill(inputCells);

            grid.Print();

            Assert.IsTrue(grid.IsComplete());
        }
    }
}