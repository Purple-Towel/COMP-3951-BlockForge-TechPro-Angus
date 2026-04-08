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
            saveToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            generateCodeToolStripMenuItem = new ToolStripMenuItem();
            textProjectName = new TextBox();
            labelProjectName = new Label();
            listBlocks = new ListBox();
            comboBoxBlockType = new ComboBox();
            labelBlockType = new Label();
            labelList = new Label();
            textBoxBlockName = new TextBox();
            labelBlockName = new Label();
            buttonAdd = new Button();
            buttonRemoveLast = new Button();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, openToolStripMenuItem, generateCodeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(152, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(152, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // generateCodeToolStripMenuItem
            // 
            generateCodeToolStripMenuItem.Name = "generateCodeToolStripMenuItem";
            generateCodeToolStripMenuItem.Size = new Size(152, 22);
            generateCodeToolStripMenuItem.Text = "Generate Code";
            // 
            // textProjectName
            // 
            textProjectName.Location = new Point(12, 46);
            textProjectName.Name = "textProjectName";
            textProjectName.Size = new Size(100, 23);
            textProjectName.TabIndex = 1;
            // 
            // labelProjectName
            // 
            labelProjectName.AutoSize = true;
            labelProjectName.Location = new Point(12, 28);
            labelProjectName.Name = "labelProjectName";
            labelProjectName.Size = new Size(82, 15);
            labelProjectName.TabIndex = 2;
            labelProjectName.Text = "Project Name:";
            // 
            // listBlocks
            // 
            listBlocks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBlocks.FormattingEnabled = true;
            listBlocks.Location = new Point(12, 97);
            listBlocks.Name = "listBlocks";
            listBlocks.Size = new Size(776, 334);
            listBlocks.TabIndex = 3;
            // 
            // comboBoxBlockType
            // 
            comboBoxBlockType.FormattingEnabled = true;
            comboBoxBlockType.Location = new Point(135, 46);
            comboBoxBlockType.Name = "comboBoxBlockType";
            comboBoxBlockType.Size = new Size(121, 23);
            comboBoxBlockType.TabIndex = 4;
            // 
            // labelBlockType
            // 
            labelBlockType.AutoSize = true;
            labelBlockType.Location = new Point(135, 28);
            labelBlockType.Name = "labelBlockType";
            labelBlockType.Size = new Size(66, 15);
            labelBlockType.TabIndex = 5;
            labelBlockType.Text = "Block Type:";
            // 
            // labelList
            // 
            labelList.AutoSize = true;
            labelList.Location = new Point(12, 79);
            labelList.Name = "labelList";
            labelList.Size = new Size(143, 15);
            labelList.TabIndex = 6;
            labelList.Text = "Blocks (Execution Order): ";
            // 
            // textBoxBlockName
            // 
            textBoxBlockName.Location = new Point(281, 46);
            textBoxBlockName.Name = "textBoxBlockName";
            textBoxBlockName.Size = new Size(100, 23);
            textBoxBlockName.TabIndex = 7;
            // 
            // labelBlockName
            // 
            labelBlockName.AutoSize = true;
            labelBlockName.Location = new Point(281, 28);
            labelBlockName.Name = "labelBlockName";
            labelBlockName.Size = new Size(103, 15);
            labelBlockName.TabIndex = 8;
            labelBlockName.Text = "Block Name/Data:";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(558, 46);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 11;
            buttonAdd.Text = "Add Block";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonRemoveLast
            // 
            buttonRemoveLast.Location = new Point(639, 46);
            buttonRemoveLast.Name = "buttonRemoveLast";
            buttonRemoveLast.Size = new Size(149, 23);
            buttonRemoveLast.TabIndex = 12;
            buttonRemoveLast.Text = "Remove Last Block";
            buttonRemoveLast.UseVisualStyleBackColor = true;
            buttonRemoveLast.Click += buttonRemoveLast_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonRemoveLast);
            Controls.Add(buttonAdd);
            Controls.Add(labelBlockName);
            Controls.Add(textBoxBlockName);
            Controls.Add(labelList);
            Controls.Add(labelBlockType);
            Controls.Add(comboBoxBlockType);
            Controls.Add(listBlocks);
            Controls.Add(labelProjectName);
            Controls.Add(textProjectName);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainWindow";
            Text = "MainWindow";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
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
        private TextBox textBoxBlockName;
        private Label labelBlockName;
        private Button buttonAdd;
        private Button buttonRemoveLast;
    }
}