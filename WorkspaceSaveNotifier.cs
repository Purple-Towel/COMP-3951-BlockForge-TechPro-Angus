using System;

namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Reports save outcomes to the custom console.
    /// </summary>
    public class WorkspaceSaveNotifier
    {
        public const string SaveSuccessMessage = "successfully saved workspace";

        private readonly IConsoleMessageSink _console;

        public WorkspaceSaveNotifier(IConsoleMessageSink console)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
        }

        public void ReportSaveSuccess()
        {
            _console.Append(new ConsoleMessage(ConsoleMessageSeverity.Message, SaveSuccessMessage));
        }
    }
}
