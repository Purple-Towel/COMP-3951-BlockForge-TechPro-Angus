namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Visual connector shown between attached blocks.
    /// This starts as a simple orange control so it can be restyled later in the WinForms designer.
    /// </summary>
    public partial class BlockConnectorControl : UserControl
    {
        public BlockConnectorControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the visual body panel so future code can reposition or restyle it if needed.
        /// </summary>
        public Panel ConnectorBodyPanel => connectorBodyPanel;
    }
}
