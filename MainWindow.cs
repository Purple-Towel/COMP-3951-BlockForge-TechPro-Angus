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
        private int _sequenceTracker;
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
            _sequenceTracker = 0;
            comboBoxBlockType.DataSource = Enum.GetValues(typeof(CodeBlockType));
            comboBoxBlockType.SelectedItem = CodeBlockType.Start;

            RefreshBlockList();
        }

        private void ClearBlockInput()
        {
            textBoxBlockName.Text = string.Empty;
            comboBoxBlockType.SelectedItem = CodeBlockType.Start;
        }

        private void RefreshBlockList()
        {
            listBlocks.Items.Clear();

            foreach (CodeBlock block in _currentProject.CodeBlocks.OrderBy(b => b.Sequence))
            {
                listBlocks.Items.Add(block);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CodeBlock newBlock = new CodeBlock
                (
                    0,
                    0,
                    Guid.NewGuid().ToString(),
                    _sequenceTracker,
                    (CodeBlockType)comboBoxBlockType.SelectedItem,
                    textBoxBlockName.Text
                );

            _currentProject.CodeBlocks.Add(newBlock);
            RefreshBlockList();
            ClearBlockInput();
            _sequenceTracker++;
        }

        private void buttonRemoveLast_Click(object sender, EventArgs e)
        {
            if (_currentProject.CodeBlocks.Count == 0)
            {
                MessageBox.Show("Block queue empty.");
                return;
            }

            CodeBlock lastBlock = _currentProject.CodeBlocks.OrderBy(b => b.Sequence).Last();

            _currentProject.CodeBlocks.Remove(lastBlock);
            _sequenceTracker--;
            RefreshBlockList();
        }
    }
}
