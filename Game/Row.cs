using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class Row
    {
        private readonly Cell[] _cells;

        public Row(int size = 2)
        {
            _cells = new Cell[size];
        }

        public bool SetCell(Cell cell, int xPosition, List<ColourEnum> validColours)
        {
            // see if cell 'fits' and add if so.
            if(cell == null || cell.IsValid(validColours))
            {
                _cells[xPosition] = cell;
                return true;
            }
            return false;
        }

        public Cell GetCell(int xPosition)
        {
            return _cells.ElementAtOrDefault(xPosition);
        }

        public string SerializeRow()
        {
            // 3 rows
            var resp = new StringBuilder();
            resp.AppendLine(string.Format("-{0}--{1}--{2}--{3}--{4}--{5}-",
                GetColour(_cells[0], 3),
                GetColour(_cells[1], 3),
                GetColour(_cells[2], 3),
                GetColour(_cells[3], 3),
                GetColour(_cells[4], 3),
                GetColour(_cells[5], 3)
                ));

            resp.AppendLine(string.Format("{0}-{1}-{2}-{3}-{4}-{5}-",
                GetColour(_cells[0], 0),
                GetColour(_cells[0], 2),
                GetColour(_cells[1], 2),
                GetColour(_cells[2], 2),
                GetColour(_cells[3], 2),
                GetColour(_cells[4], 2)
                ));

            resp.AppendLine(string.Format("-{0}--{1}--{2}--{3}--{4}--{5}-",
                GetColour(_cells[0], 1),
                GetColour(_cells[1], 1),
                GetColour(_cells[2], 1),
                GetColour(_cells[3], 1),
                GetColour(_cells[4], 1),
                GetColour(_cells[5], 1)
                ));

            return resp.ToString();
        }

        private object GetColour(Cell cell, int colourIndex)
        {
            if (cell == null)
            {
                return "-";
            }

            if (cell.Colours[colourIndex] == ColourEnum.None)
            {
                return "-";
            }

            return cell.Colours[colourIndex].ToString().Substring(0, 1);
        }
    }
}