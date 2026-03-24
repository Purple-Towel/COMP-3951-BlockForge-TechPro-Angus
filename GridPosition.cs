namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Represents a block's occupied row and column on the workspace snap grid.
    /// </summary>
    public readonly record struct GridPosition(int Column, int Row);
}
