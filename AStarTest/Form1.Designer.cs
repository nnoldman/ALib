namespace AStarTest {
partial class Form1 {
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent() {
        this.panel1 = new System.Windows.Forms.Panel();
        this.find = new System.Windows.Forms.Button();
        this.generate_map = new System.Windows.Forms.Button();
        this.findWindow1 = new AStarTest.FindWindow();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        //
        // panel1
        //
        this.panel1.Controls.Add(this.find);
        this.panel1.Controls.Add(this.generate_map);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
        this.panel1.Location = new System.Drawing.Point(0, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(292, 693);
        this.panel1.TabIndex = 0;
        //
        // find
        //
        this.find.Location = new System.Drawing.Point(19, 61);
        this.find.Name = "find";
        this.find.Size = new System.Drawing.Size(259, 35);
        this.find.TabIndex = 1;
        this.find.Text = "寻路";
        this.find.UseVisualStyleBackColor = true;
        this.find.Click += new System.EventHandler(this.find_Click);
        //
        // generate_map
        //
        this.generate_map.Location = new System.Drawing.Point(19, 14);
        this.generate_map.Name = "generate_map";
        this.generate_map.Size = new System.Drawing.Size(259, 31);
        this.generate_map.TabIndex = 0;
        this.generate_map.Text = "生成地图";
        this.generate_map.UseVisualStyleBackColor = true;
        this.generate_map.Click += new System.EventHandler(this.generate_map_Click);
        //
        // findWindow1
        //
        this.findWindow1.AutoSize = true;
        this.findWindow1.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.findWindow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.findWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.findWindow1.Location = new System.Drawing.Point(292, 0);
        this.findWindow1.Name = "findWindow1";
        this.findWindow1.Size = new System.Drawing.Size(736, 693);
        this.findWindow1.TabIndex = 1;
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1028, 693);
        this.Controls.Add(this.findWindow1);
        this.Controls.Add(this.panel1);
        this.Name = "Form1";
        this.Text = "Form1";
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button generate_map;
    private FindWindow findWindow1;
    private System.Windows.Forms.Button find;
}
}

