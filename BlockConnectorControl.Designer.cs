namespace COMP_3951_BlockForge_TechPro
{
    partial class BlockConnectorControl
    {
        private System.ComponentModel.IContainer components = null;
        private Panel connectorBodyPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            connectorBodyPanel = new Panel();
            SuspendLayout();
            // 
            // connectorBodyPanel
            // 
            connectorBodyPanel.BackColor = Color.DarkOrange;
            connectorBodyPanel.Dock = DockStyle.Fill;
            connectorBodyPanel.Location = new Point(0, 0);
            connectorBodyPanel.Margin = new Padding(0);
            connectorBodyPanel.Name = "connectorBodyPanel";
            connectorBodyPanel.Size = new Size(18, 12);
            connectorBodyPanel.TabIndex = 0;
            // 
            // BlockConnectorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(connectorBodyPanel);
            Margin = new Padding(0);
            Name = "BlockConnectorControl";
            Size = new Size(18, 12);
            ResumeLayout(false);
        }
    }
}
