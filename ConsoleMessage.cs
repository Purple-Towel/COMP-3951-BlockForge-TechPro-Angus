namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Represents one line in the custom console window.
    /// </summary>
    public readonly record struct ConsoleMessage(ConsoleMessageSeverity Severity, string Text);
}
