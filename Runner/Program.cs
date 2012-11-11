using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
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

            Console.WriteLine(grid.Print());
        }
    }
}
