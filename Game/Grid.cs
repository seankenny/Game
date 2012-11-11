using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Game
{
    public class Grid
    {
        private readonly int _size;
        private readonly Row[] _rows;

        public Grid(int size = 2, bool initializeBorder = false)
        {
            _size = size;
            _rows = new Row[size];

            if (initializeBorder)
            {
                // left
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.Blue, ColourEnum.None), 0, 1);
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.Yellow, ColourEnum.None), 0, 2);
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.Red, ColourEnum.None), 0, 3);
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.Yellow, ColourEnum.None), 0, 4);

                // top
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.None, ColourEnum.Red), 1, 5);
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.None, ColourEnum.Blue), 2, 5);
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.None, ColourEnum.Yellow), 3, 5);
                SetCell(new Cell(ColourEnum.None, ColourEnum.None, ColourEnum.None, ColourEnum.Blue), 4, 5);

                // right
                SetCell(new Cell(ColourEnum.Red, ColourEnum.None, ColourEnum.None, ColourEnum.None), 5, 1);
                SetCell(new Cell(ColourEnum.Yellow, ColourEnum.None, ColourEnum.None, ColourEnum.None), 5, 2);
                SetCell(new Cell(ColourEnum.Blue, ColourEnum.None, ColourEnum.None, ColourEnum.None), 5, 3);
                SetCell(new Cell(ColourEnum.Yellow, ColourEnum.None, ColourEnum.None, ColourEnum.None), 5, 4);

                // bottom
                SetCell(new Cell(ColourEnum.None, ColourEnum.Red, ColourEnum.None, ColourEnum.None), 1, 0);
                SetCell(new Cell(ColourEnum.None, ColourEnum.Yellow, ColourEnum.None, ColourEnum.None), 2, 0);
                SetCell(new Cell(ColourEnum.None, ColourEnum.Red, ColourEnum.None, ColourEnum.None), 3, 0);
                SetCell(new Cell(ColourEnum.None, ColourEnum.Blue, ColourEnum.None, ColourEnum.None), 4, 0);
            }
        }

        public bool SetCell(Cell cell, int xPosition, int yPosition)
        {
            if (_rows[yPosition] == null)
            {
                _rows[yPosition] = new Row(_size);
            }

            // border rows/cells
            var isBorder = xPosition == 0 || xPosition == _size - 1 || 
                           yPosition == 0 || yPosition == _size - 1;

            // get valid colours
            var validColours = isBorder ? null : GetValidColours(xPosition, yPosition);
            
            return _rows[yPosition].SetCell(cell, xPosition, validColours);
        }

        public Cell GetCell(int xPosition, int yPosition)
        {
            var row = _rows.ElementAtOrDefault(yPosition);
            if (row == null)
            {
                return null;
            }

            return row.GetCell(xPosition);
        }

        public List<ColourEnum> GetValidColours(int xPosition, int yPosition)
        {
            // left
            var c = GetCell(xPosition - 1, yPosition);
            var l = c == null ? ColourEnum.None : c.Colours[2]; // right side of left cell

            // top
            c = GetCell(xPosition, yPosition + 1);
            var t = c == null ? ColourEnum.None : c.Colours[3]; // bottom side of top cell

            // right
            c = GetCell(xPosition + 1, yPosition);
            var r = c == null ? ColourEnum.None : c.Colours[0]; // left side of right cell

            // bottom
            c = GetCell(xPosition, yPosition - 1);
            var b = c == null ? ColourEnum.None : c.Colours[1]; // top side of bottom cell


            return new List<ColourEnum> { l, t, r, b };
        }

        public bool IsComplete()
        {
            return GetCell(_size -2, _size -2) != null;
        }

        public bool Fill(List<Cell> inputCells)
        {
            const int xStartPosition = 1;
            const int yStartPosition = 1;

            // get the working list of cells
            var remainingCells = new List<Cell>();
            inputCells.ForEach(remainingCells.Add);

            SetNextCell(xStartPosition, yStartPosition, remainingCells);

            return GetCell(_size - 1, _size - 1) != null;
        }

        private void SetNextCell(int xPosition, int yPosition, List<Cell> remainingCells)
        {
            if (IsComplete())
            {
                // VICTORY!!
                return;
            }

            // get the valid cells for the next position
            var cellsForNextPosition = GetCellsValidForNextCell(xPosition, yPosition, remainingCells);

            // if no cells, return
            if(cellsForNextPosition.Count == 0)
            {
                return;
            }

            // set the next position
            foreach (var cellForNextPosition in cellsForNextPosition)
            {
                if(IsComplete())
                {
                    return;
                }

                // set the cell
                SetCell(cellForNextPosition, xPosition, yPosition);
                
                // pop from available cells
                remainingCells.Remove(cellForNextPosition);

                if(remainingCells.Count == 0)
                {
                    return;
                }

                // recursion!!! 
                var nextXPosition = xPosition + 1;
                var nextYPosition = yPosition;
                if (nextXPosition == _size - 1)
                {
                    nextXPosition = 1;
                    nextYPosition = nextYPosition + 1;
                }

                SetNextCell(nextXPosition, nextYPosition, remainingCells);
                
                // add this cell back into the pot as we didn't use it after all.
                remainingCells.Add(cellForNextPosition);
            }
            // clear the board position
            if (!IsComplete())
            {
                SetCell(null, xPosition, yPosition);
            }
        }

        private List<Cell> GetCellsValidForNextCell(int xPosition, int yPosition, IEnumerable<Cell> remainingCells)
        {
            var validColours = GetValidColours(xPosition, yPosition);

            var validCells = remainingCells.Where(c => c.IsValid(validColours)).ToList();

            return validCells;
        }

        public string Print()
        {
            var sb = new StringBuilder();

            foreach (var row in _rows)
            {
                sb.Append(row.SerializeRow());
            }

            return sb.ToString();
        }
    }
}