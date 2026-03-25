namespace COMP_3951_BlockForge_TechPro
{
    public partial class Form1 : Form
    {
        private const int GridCellWidth = 40;
        private const int GridCellHeight = 40;

        private readonly GridSnapService _gridSnapService;
        private readonly Dictionary<Panel, CodeBlock> _workspaceBlocks = new();

        public Form1()
        {
            InitializeComponent();
            _gridSnapService = new GridSnapService(GridCellWidth, GridCellHeight);
            SetupDragDrop();
            CreateBlockTemplates();
        }

        /// <summary>
        /// Uses the current workspace client size so future snap calls always respect the live workspace area.
        /// </summary>
        private Size WorkspaceBounds => groupBoxWorkSpace.ClientSize;

        // --- Drag/Drop wiring for the workspace ---
        private void SetupDragDrop()
        {
            // Allow the workspace groupbox to accept drops
            groupBoxWorkSpace.AllowDrop = true;
            groupBoxWorkSpace.DragEnter += WorkSpace_DragEnter;
            groupBoxWorkSpace.DragDrop += WorkSpace_DragDrop;
        }

        // Testing Templates
        private void CreateBlockTemplates()
        {
            // A container that sits at the TOP of BlockBin and auto-lays out blocks left-to-right
            var topRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 10, 10, 10), // space from the groupbox border/title
                BackColor = Color.Transparent
            };

            //templates
            var block1 = MakeTemplateBlock("If", Color.Aqua);
            var block2 = MakeTemplateBlock("While", Color.PeachPuff);
            var block3 = MakeTemplateBlock("Run", Color.Green);
            var block4 = MakeTemplateBlock("Print", Color.Khaki);
            var block5 = MakeTemplateBlock("==", Color.Plum);

            // Size 
            block1.Size = block2.Size = block3.Size = block4.Size = block5.Size = new Size(70, 60);

            // Small gap between blocks (FlowLayoutPanel uses each control's Margin)
            block1.Margin = new Padding(0, 0, 8, 0);
            block2.Margin = new Padding(0, 0, 8, 0);
            block3.Margin = new Padding(0, 0, 8, 0);
            block4.Margin = new Padding(0, 0, 8, 0);
            block5.Margin = new Padding(0, 0, 8, 0);

            // Add to the row
            topRow.Controls.Add(block1);
            topRow.Controls.Add(block2);
            topRow.Controls.Add(block3);
            topRow.Controls.Add(block4);
            topRow.Controls.Add(block5);

            // Add the row to BlockBin
            groupBoxBlockBin.Controls.Add(topRow);

            // Optional: ensure it stays above anything else added later
            topRow.BringToFront();
        }

        // Creates a template block panel with MouseDown -> DoDragDrop
        private Panel MakeTemplateBlock(string text, Color color)
        {
            var p = new Panel
            {
                Size = new Size(140, 45),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = text // store block "type/name" (handy later)
            };

            var lbl = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            p.Controls.Add(lbl);

            // clicking the label should also drag
            p.MouseDown += TemplateBlock_MouseDown;
            lbl.MouseDown += (s, e) => TemplateBlock_MouseDown(p, e);

            return p;
        }

        // When you click a template in BlockBin, start a drag with COPY effect
        private void TemplateBlock_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var template = sender as Panel;
            if (template == null) return;

            // Pass the template panel as the drag data
            template.DoDragDrop(template, DragDropEffects.Copy);
        }

        // Workspace DragEnter 
        private void WorkSpace_DragEnter(object sender, DragEventArgs e)
        {
            // We only accept Panel data
            if (e.Data != null && e.Data.GetDataPresent(typeof(Panel)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // Workspace DragDrop -> clone template into workspace 
        private void WorkSpace_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null) return;

            var template = e.Data.GetData(typeof(Panel)) as Panel;
            if (template == null) return;

            // Clone a new block based on the template
            var newBlock = CloneAsWorkspaceBlock(template);

            // Convert screen mouse coords to workspace client coords
            Point dropPoint = groupBoxWorkSpace.PointToClient(new Point(e.X, e.Y));

            // groupBox has a border/title; keep it inside a bit
            dropPoint.Offset(-newBlock.Width / 2, -newBlock.Height / 2);

            SnappedPlacement snappedPlacement = _gridSnapService.Snap(dropPoint, newBlock.Size, WorkspaceBounds);
            newBlock.Location = snappedPlacement.Location;
            groupBoxWorkSpace.Controls.Add(newBlock);
            newBlock.BringToFront();

            RegisterWorkspaceBlock(newBlock, snappedPlacement);
        }

        // Clone template into a draggable workspace block 
        private Panel CloneAsWorkspaceBlock(Panel template)
        {
            // Pull text/type from Tag, and color from BackColor
            string text = template.Tag?.ToString() ?? "Block";
            Color color = template.BackColor;

            var p = new Panel
            {
                Size = template.Size,
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.SizeAll,
                Tag = text
            };

            var lbl = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            p.Controls.Add(lbl);

            // Make it draggable INSIDE the workspace
            p.MouseDown += WorkspaceBlock_MouseDown;
            p.MouseMove += WorkspaceBlock_MouseMove;
            p.MouseUp += WorkspaceBlock_MouseUp;

            // If they click label, still drag the parent panel
            lbl.MouseDown += (s, e) => WorkspaceBlock_MouseDown(p, e);
            lbl.MouseMove += (s, e) => WorkspaceBlock_MouseMove(p, e);
            lbl.MouseUp += (s, e) => WorkspaceBlock_MouseUp(p, e);

            return p;
        }

        // Drag blocks around inside WorkSpace 
        private bool _dragging = false;
        private Point _dragOffset; // mouse offset within the panel being dragged
        private Panel _activeBlock;

        private void WorkspaceBlock_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _activeBlock = sender as Panel;
            if (_activeBlock == null) return;

            _dragging = true;
            _dragOffset = e.Location; // where in the panel the mouse grabbed it
            _activeBlock.BringToFront();
        }

        private void WorkspaceBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging || _activeBlock == null) return;

            // Mouse position relative to workspace
            Point mouseInWorkspace = groupBoxWorkSpace.PointToClient(MousePosition);

            // New top-left = mouse minus grab offset
            Point newLoc = new Point(mouseInWorkspace.X - _dragOffset.X,
                                     mouseInWorkspace.Y - _dragOffset.Y);

            newLoc = ClampToBounds(newLoc, _activeBlock.Size, groupBoxWorkSpace.ClientSize);
            _activeBlock.Location = newLoc;
        }

        private void WorkspaceBlock_MouseUp(object sender, MouseEventArgs e)
        {
            if (_activeBlock != null)
            {
                SnappedPlacement snappedPlacement = _gridSnapService.Snap(_activeBlock.Location, _activeBlock.Size, WorkspaceBounds);
                _activeBlock.Location = snappedPlacement.Location;
                UpdateStoredBlockPosition(_activeBlock, snappedPlacement);
            }

            _dragging = false;
            _activeBlock = null;
        }

        private void RegisterWorkspaceBlock(Panel blockPanel, SnappedPlacement snappedPlacement)
        {
            string blockName = blockPanel.Tag?.ToString() ?? "Block";
            string uid = $"{blockName}-{Guid.NewGuid():N}";
            var codeBlock = new CodeBlock(
                snappedPlacement.Location.X,
                snappedPlacement.Location.Y,
                uid,
                snappedPlacement.GridPosition.Column,
                snappedPlacement.GridPosition.Row);

            _workspaceBlocks[blockPanel] = codeBlock;
        }

        private void UpdateStoredBlockPosition(Panel blockPanel, SnappedPlacement snappedPlacement)
        {
            if (!_workspaceBlocks.TryGetValue(blockPanel, out CodeBlock? codeBlock))
            {
                return;
            }

            codeBlock.UpdatePosition(snappedPlacement.Location.X, snappedPlacement.Location.Y);
            codeBlock.UpdateGridPosition(snappedPlacement.GridPosition.Column, snappedPlacement.GridPosition.Row);
        }

        // keep blocks inside workspace bounds
        private Point ClampToBounds(Point loc, Size blockSize, Size containerSize)
        {
            int x = loc.X;
            int y = loc.Y;

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            int maxX = containerSize.Width - blockSize.Width;
            int maxY = containerSize.Height - blockSize.Height;

            if (x > maxX) x = maxX;
            if (y > maxY) y = maxY;

            return new Point(x, y);
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SaveWorkspaceLayout();
        }
    }
}
