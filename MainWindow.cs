using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// BlockForge MainWindow
/// Author: Angus Grewal
/// Date: Apr 14 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Main window of the BlockForge program.
    /// </summary>
    public partial class MainWindow : Form
    {
        private int _sequenceTracker;
        private Project _currentProject;
        private readonly PayloadTransformer _payloadTransformer;
        private readonly ProjectFileManager _projectFileManager;

        /// <summary>
        /// Constructor that will instantiate the main window.
        /// </summary>
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

        /// <summary>
        /// Helper function that will clear input fields for Blocks.
        /// </summary>
        private void ClearBlockInput()
        {
            textBoxBlockName.Text = string.Empty;
            comboBoxBlockType.SelectedItem = CodeBlockType.Start;
        }

        /// <summary>
        /// Helper function that will populate the list of blocks in order by their sequence number.
        /// </summary>
        private void RefreshBlockList()
        {
            listBlocks.Items.Clear();

            foreach (CodeBlock block in _currentProject.CodeBlocks.OrderBy(b => b.Sequence))
            {
                listBlocks.Items.Add(block);
            }
        }

        /// <summary>
        /// Handler for the add block button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handler for the remove last block button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handler for the save menu option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string projectName = textProjectName.Text.Trim();

            if (string.IsNullOrWhiteSpace(projectName))
            {
                MessageBox.Show("Please enter a project name before save.");
                return;
            }

            _currentProject.ProjectName = projectName;

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "BlockForge Project (*.bfg)|*.bfg|All Files (*.*)|*.*";
            save.FileName = $"{projectName}.bfg";

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _projectFileManager.SaveFile(_currentProject, save.FileName);
                    MessageBox.Show($"Saved {_currentProject.ProjectName} successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Save failed:\n{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handler for the open menu option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "BlockForge Project (*.bfg)|*.bfg|All Files (*.*)|*.*";

            if (open.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    _currentProject = _projectFileManager.LoadFile(open.FileName);
                    textProjectName.Text = _currentProject.ProjectName ?? "Untitled";

                    _sequenceTracker = _currentProject.CodeBlocks.Any() ? _currentProject.CodeBlocks.Max(b => b.Sequence) + 1 : 0;
                    RefreshBlockList();
                    MessageBox.Show($"Project {_currentProject.ProjectName} opened successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Open failed:\n{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handler for the generate menu option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            save.FileName = $"{textProjectName.Text.Trim()}_Output.txt";

            if (save.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    _projectFileManager.OutputCode(_currentProject, save.FileName);
                    MessageBox.Show("Pseudocode generated successfully.");
                } 
                catch (Exception ex)
                {
                    MessageBox.Show($"Code generation failed:\n{ex.Message}");
                }
            }
        }
    }
}
