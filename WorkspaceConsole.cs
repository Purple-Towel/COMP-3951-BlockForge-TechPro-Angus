using System;
using System.Collections.Generic;

namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// In-memory backing store for the custom console window.
    /// </summary>
    public class WorkspaceConsole : IConsoleMessageSink
    {
        private readonly List<ConsoleMessage> _messages = new();

        public IReadOnlyList<ConsoleMessage> Messages => _messages;

        public void Append(ConsoleMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
            {
                throw new ArgumentException("Console messages must contain text.", nameof(message));
            }

            _messages.Add(message);
        }
    }
}
