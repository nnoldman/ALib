namespace AStarTest {
partial class FindWindow {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.setStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.setEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.contextMenuStrip1.SuspendLayout();
        this.SuspendLayout();
        //
        // contextMenuStrip1
        //
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.setStartToolStripMenuItem,
            this.setEndToolStripMenuItem
        });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(122, 54);
        //
        // toolStripSeparator1
        //
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(118, 6);
        //
        // setStartToolStripMenuItem
        //
        this.setStartToolStripMenuItem.Name = "setStartToolStripMenuItem";
        this.setStartToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
        this.setStartToolStripMenuItem.Text = "SetStart";
        this.setStartToolStripMenuItem.Click += new System.EventHandler(this.setStartToolStripMenuItem_Click);
        //
        // setEndToolStripMenuItem
        //
        this.setEndToolStripMenuItem.Name = "setEndToolStripMenuItem";
        this.setEndToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
        this.setEndToolStripMenuItem.Text = "SetEnd";
        this.setEndToolStripMenuItem.Click += new System.EventHandler(this.setEndToolStripMenuItem_Click);
        //
        // FindWindow
        //
        this.AutoSize = true;
        this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.DoubleBuffered = true;
        this.Name = "FindWindow";
        this.Size = new System.Drawing.Size(1031, 474);
        this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FindWindow_MouseUp);
        this.Resize += new System.EventHandler(this.FindWindow_Resize);
        this.Paint += FindWindow_Paint;
        this.contextMenuStrip1.ResumeLayout(false);
        this.ResumeLayout(false);

    }



    #endregion

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem setStartToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem setEndToolStripMenuItem;
}
}