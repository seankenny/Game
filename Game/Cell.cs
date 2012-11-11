using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Cell
    {
        private ColourEnum[] _colours = new ColourEnum[4];
        public ColourEnum[] Colours { get { return _colours; } }
        
        public Cell(ColourEnum side1, ColourEnum side2, ColourEnum side3, ColourEnum side4)
        {
            _colours[0] = side1; // left
            _colours[1] = side2; // top
            _colours[2] = side3; // right
            _colours[3] = side4; // bottom
        }

        public bool IsValid(List<ColourEnum> validColours)
        {
            var valid = false;

            // border cells means no valid colours to worry about
            if (validColours == null)
            {
                return true;
            }

            for (int cellSide = 0; cellSide < 4; cellSide++)
            {
                if ((validColours[0] == ColourEnum.None || _colours[0] == validColours[0]) &&
                    (validColours[1] == ColourEnum.None || _colours[1] == validColours[1]) &&
                    (validColours[2] == ColourEnum.None || _colours[2] == validColours[2]) &&
                    (validColours[3] == ColourEnum.None || _colours[3] == validColours[3]))
                {
                    valid = true;
                    break;
                }
                RotateCell();
            }

            return valid;
        }

        private void RotateCell()
        {
            var temp = _colours.Skip(1).Take(3).ToList();
            temp.Add(_colours.First());
            _colours = temp.ToArray();
        }
    }
}