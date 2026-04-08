using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace COMP_3951_BlockForge_TechPro
{
    public partial class MainWindow : Form
    {
        private Project _currentProject;
        private readonly PayloadTransformer _payloadTransformer;
        private readonly ProjectFileManager _projectFileManager;

        public MainWindow()
        {
            InitializeComponent();
            _payloadTransformer = new PayloadTransformer(3);
            _projectFileManager = new ProjectFileManager(_payloadTransformer);
            _currentProject = new Project("Untitled", new List<CodeBlock>());

            textProjectName.Text = _currentProject.ProjectName;
            numericUpDownSequence.Value = 0;
            comboBoxBlockType.DataSource = Enum.GetValues(typeof(CodeBlockType));
            comboBoxBlockType.SelectedItem = CodeBlockType.Start;

            RefreshBlockList();
        }

        private void RefreshBlockList()
        {

        }
    }
}
