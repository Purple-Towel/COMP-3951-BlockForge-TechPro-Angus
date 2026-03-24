using System;
using System.Drawing;

namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Calculates snapped block positions for a simple rectangular grid.
    /// </summary>
    public class GridSnapService
    {
        public int CellWidth { get; }
        public int CellHeight { get; }

        public GridSnapService(int cellWidth, int cellHeight)
        {
            if (cellWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cellWidth), "Cell width must be greater than zero.");
            }

            if (cellHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cellHeight), "Cell height must be greater than zero.");
            }

            CellWidth = cellWidth;
            CellHeight = cellHeight;
        }

        public GridPosition GetGridPosition(Point rawLocation)
        {
            int column = (int)Math.Round(rawLocation.X / (double)CellWidth, MidpointRounding.AwayFromZero);
            int row = (int)Math.Round(rawLocation.Y / (double)CellHeight, MidpointRounding.AwayFromZero);

            return new GridPosition(column, row);
        }

        public Point GetSnappedLocation(GridPosition gridPosition)
        {
            return new Point(gridPosition.Column * CellWidth, gridPosition.Row * CellHeight);
        }

        public SnappedPlacement Snap(Point rawLocation, Size blockSize, Size workspaceSize)
        {
            int maxColumn = Math.Max(0, (workspaceSize.Width - blockSize.Width) / CellWidth);
            int maxRow = Math.Max(0, (workspaceSize.Height - blockSize.Height) / CellHeight);

            GridPosition rawGridPosition = GetGridPosition(rawLocation);
            GridPosition clampedGridPosition = new(
                Clamp(rawGridPosition.Column, 0, maxColumn),
                Clamp(rawGridPosition.Row, 0, maxRow));

            return new SnappedPlacement(GetSnappedLocation(clampedGridPosition), clampedGridPosition);
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }
    }
}
