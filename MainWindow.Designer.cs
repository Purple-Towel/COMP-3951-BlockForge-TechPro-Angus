namespace COMP_3951_BlockForge_TechPro
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            generateCodeToolStripMenuItem = new ToolStripMenuItem();
            textProjectName = new TextBox();
            labelProjectName = new Label();
            listBlocks = new ListBox();
            comboBoxBlockType = new ComboBox();
            labelBlockType = new Label();
            labelList = new Label();
            textBoxBlockData = new TextBox();
            labelBlockData = new Label();
            buttonAdd = new Button();
            buttonRemoveSelected = new Button();
            buttonMoveUp = new Button();
            buttonMoveDown = new Button();
            buttonEditSelected = new Button();
            groupBoxSelected = new GroupBox();
            groupBoxAddBlock = new GroupBox();
            buttonValidate = new Button();
            menuStrip.SuspendLayout();
            groupBoxSelected.SuspendLayout();
            groupBoxAddBlock.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(698, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newStripMenuItem, saveToolStripMenuItem, openToolStripMenuItem, generateCodeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newStripMenuItem
            // 
            newStripMenuItem.Name = "newStripMenuItem";
            newStripMenuItem.Size = new Size(152, 22);
            newStripMenuItem.Text = "New";
            newStripMenuItem.Click += newStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(152, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(152, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // generateCodeToolStripMenuItem
            // 
            generateCodeToolStripMenuItem.Name = "generateCodeToolStripMenuItem";
            generateCodeToolStripMenuItem.Size = new Size(152, 22);
            generateCodeToolStripMenuItem.Text = "Generate Code";
            generateCodeToolStripMenuItem.Click += generateCodeToolStripMenuItem_Click;
            // 
            // textProjectName
            // 
            textProjectName.Location = new Point(18, 44);
            textProjectName.Name = "textProjectName";
            textProjectName.Size = new Size(463, 23);
            textProjectName.TabIndex = 1;
            // 
            // labelProjectName
            // 
            labelProjectName.AutoSize = true;
            labelProjectName.Location = new Point(18, 26);
            labelProjectName.Name = "labelProjectName";
            labelProjectName.Size = new Size(82, 15);
            labelProjectName.TabIndex = 2;
            labelProjectName.Text = "Project Name:";
            // 
            // listBlocks
            // 
            listBlocks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBlocks.FormattingEnabled = true;
            listBlocks.Location = new Point(12, 172);
            listBlocks.Name = "listBlocks";
            listBlocks.Size = new Size(674, 304);
            listBlocks.TabIndex = 3;
            listBlocks.SelectedIndexChanged += listBlocks_SelectedIndexChanged;
            // 
            // comboBoxBlockType
            // 
            comboBoxBlockType.FormattingEnabled = true;
            comboBoxBlockType.Location = new Point(6, 37);
            comboBoxBlockType.Name = "comboBoxBlockType";
            comboBoxBlockType.Size = new Size(121, 23);
            comboBoxBlockType.TabIndex = 4;
            // 
            // labelBlockType
            // 
            labelBlockType.AutoSize = true;
            labelBlockType.Location = new Point(6, 19);
            labelBlockType.Name = "labelBlockType";
            labelBlockType.Size = new Size(66, 15);
            labelBlockType.TabIndex = 5;
            labelBlockType.Text = "Block Type:";
            // 
            // labelList
            // 
            labelList.AutoSize = true;
            labelList.Location = new Point(12, 154);
            labelList.Name = "labelList";
            labelList.Size = new Size(143, 15);
            labelList.TabIndex = 6;
            labelList.Text = "Blocks (Execution Order): ";
            // 
            // textBoxBlockData
            // 
            textBoxBlockData.Location = new Point(133, 37);
            textBoxBlockData.Name = "textBoxBlockData";
            textBoxBlockData.Size = new Size(100, 23);
            textBoxBlockData.TabIndex = 7;
            // 
            // labelBlockData
            // 
            labelBlockData.AutoSize = true;
            labelBlockData.Location = new Point(133, 19);
            labelBlockData.Name = "labelBlockData";
            labelBlockData.Size = new Size(66, 15);
            labelBlockData.TabIndex = 8;
            labelBlockData.Text = "Block Data:";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(239, 37);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 11;
            buttonAdd.Text = "Add Block";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonRemoveSelected
            // 
            buttonRemoveSelected.Location = new Point(6, 51);
            buttonRemoveSelected.Name = "buttonRemoveSelected";
            buttonRemoveSelected.Size = new Size(156, 23);
            buttonRemoveSelected.TabIndex = 13;
            buttonRemoveSelected.Text = "Remove Selected Block";
            buttonRemoveSelected.UseVisualStyleBackColor = true;
            buttonRemoveSelected.Click += buttonRemoveSelected_Click;
            // 
            // buttonMoveUp
            // 
            buttonMoveUp.Location = new Point(6, 80);
            buttonMoveUp.Name = "buttonMoveUp";
            buttonMoveUp.Size = new Size(75, 23);
            buttonMoveUp.TabIndex = 14;
            buttonMoveUp.Text = "Up";
            buttonMoveUp.UseVisualStyleBackColor = true;
            buttonMoveUp.Click += buttonMoveUp_Click;
            // 
            // buttonMoveDown
            // 
            buttonMoveDown.Location = new Point(87, 80);
            buttonMoveDown.Name = "buttonMoveDown";
            buttonMoveDown.Size = new Size(75, 23);
            buttonMoveDown.TabIndex = 15;
            buttonMoveDown.Text = "Down";
            buttonMoveDown.UseVisualStyleBackColor = true;
            buttonMoveDown.Click += buttonMoveDown_Click;
            // 
            // buttonEditSelected
            // 
            buttonEditSelected.Location = new Point(6, 22);
            buttonEditSelected.Name = "buttonEditSelected";
            buttonEditSelected.Size = new Size(156, 23);
            buttonEditSelected.TabIndex = 16;
            buttonEditSelected.Text = "Edit Selected Block";
            buttonEditSelected.UseVisualStyleBackColor = true;
            buttonEditSelected.Click += buttonEditSelected_Click;
            // 
            // groupBoxSelected
            // 
            groupBoxSelected.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBoxSelected.Controls.Add(buttonEditSelected);
            groupBoxSelected.Controls.Add(buttonRemoveSelected);
            groupBoxSelected.Controls.Add(buttonMoveDown);
            groupBoxSelected.Controls.Add(buttonMoveUp);
            groupBoxSelected.Enabled = false;
            groupBoxSelected.Location = new Point(513, 40);
            groupBoxSelected.Name = "groupBoxSelected";
            groupBoxSelected.Size = new Size(169, 107);
            groupBoxSelected.TabIndex = 17;
            groupBoxSelected.TabStop = false;
            groupBoxSelected.Text = "Selection Controls";
            // 
            // groupBoxAddBlock
            // 
            groupBoxAddBlock.Controls.Add(labelBlockType);
            groupBoxAddBlock.Controls.Add(comboBoxBlockType);
            groupBoxAddBlock.Controls.Add(buttonAdd);
            groupBoxAddBlock.Controls.Add(textBoxBlockData);
            groupBoxAddBlock.Controls.Add(labelBlockData);
            groupBoxAddBlock.Location = new Point(12, 76);
            groupBoxAddBlock.Name = "groupBoxAddBlock";
            groupBoxAddBlock.Size = new Size(322, 71);
            groupBoxAddBlock.TabIndex = 18;
            groupBoxAddBlock.TabStop = false;
            groupBoxAddBlock.Text = "Add/Update Blocks";
            // 
            // buttonValidate
            // 
            buttonValidate.Location = new Point(385, 90);
            buttonValidate.Name = "buttonValidate";
            buttonValidate.Size = new Size(75, 53);
            buttonValidate.TabIndex = 19;
            buttonValidate.Text = "Check Validation";
            buttonValidate.UseVisualStyleBackColor = true;
            buttonValidate.Click += buttonValidate_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 497);
            Controls.Add(buttonValidate);
            Controls.Add(groupBoxSelected);
            Controls.Add(labelList);
            Controls.Add(listBlocks);
            Controls.Add(labelProjectName);
            Controls.Add(textProjectName);
            Controls.Add(menuStrip);
            Controls.Add(groupBoxAddBlock);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            Name = "MainWindow";
            Text = "MainWindow";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            groupBoxSelected.ResumeLayout(false);
            groupBoxAddBlock.ResumeLayout(false);
            groupBoxAddBlock.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem generateCodeToolStripMenuItem;
        private TextBox textProjectName;
        private Label labelProjectName;
        private ListBox listBlocks;
        private ComboBox comboBoxBlockType;
        private Label labelBlockType;
        private Label labelList;
        private TextBox textBoxBlockData;
        private Label labelBlockData;
        private Button buttonAdd;
        private Button buttonRemoveSelected;
        private Button buttonMoveUp;
        private Button buttonMoveDown;
        private Button buttonEditSelected;
        private GroupBox groupBoxSelected;
        private GroupBox groupBoxAddBlock;
        private ToolStripMenuItem newStripMenuItem;
        private Button buttonValidate;
    }
}