using System.Drawing;

namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Contains the snapped pixel location and the grid cell occupied by a block.
    /// </summary>
    public readonly record struct SnappedPlacement(Point Location, GridPosition GridPosition);
}
