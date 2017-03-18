using ALib;
using ALib.AStar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarTest {
public partial class FindWindow : Panel {
    enum Flag {
        Walkable = 0,
        Block = 1,
        StartPoint = 1 << 1,
        GoalPoint = 1 << 2,
        Walked = 1 << 3,
        Invlid = 1 << 4 | Block,
    }

    private Graphics graph;
    private int[,] mapdata_;
    private int w_;
    private int h_;
    private Pen block_pen_;
    private Pen walkable_pen_;
    private Pen start_pen_;
    private Pen end_pen_;
    private Pen walked_pen_;
    private float cell_sizex = 40f;
    private float cell_sizey = 40f;

    private float space_ = 1f;
    public Point start ;
    public Point end;

    public FindWindow() {
        InitializeComponent();
        InitGraph();
    }

    void InitGraph() {
        graph = CreateGraphics();

        block_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.Green, Color.Gray));
        walked_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.White, Color.Gray));

        walkable_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.Gray, Color.Gray));
        start_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.Red, Color.Gray));
        end_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.SkyBlue, Color.Gray));
    }

    public void SetMap(int[,] mapdata) {
        mapdata_ = mapdata;
        if (!MapInvalid)
            return;
        OnResize();
        DrawMap();
    }

    bool MapInvalid {
        get {
            return mapdata_ != null;
        }
    }
    void ClearWalkstate() {
        for (int i = 0; i < w_; ++i) {
            for (int j = 0; j < h_; ++j) {
                var state = mapdata_[i, j];
                if (state == (int)Flag.Walked)
                    mapdata_[i, j] = (int)Flag.Walkable;
            }
        }
    }
    public void SetResult(List<Node> path) {
        if (path == null)
            return;
        ClearWalkstate();
        foreach (var node in path) {
            mapdata_[node.x, node.y] = (int)Flag.Walked;
        }
        DrawMap();
    }


    void DrawMap() {
        graph.Flush();

        if (!MapInvalid)
            return;
        for (int i = 0; i < w_; ++i) {
            for (int j = 0; j < h_; ++j) {
                DrawCell(i, j, mapdata_[i, j]);
            }
        }
    }

    bool Is(int state, NodeFlag flag) {
        return (state & (int)flag) > 0;
    }

    void DrawCell(int x, int y, int state) {
        if (state == (int)Flag.StartPoint)
            DrawCellWithPen(x, y, start_pen_);
        else if (state == (int)Flag.GoalPoint)
            DrawCellWithPen(x, y, end_pen_);
        else if (state == (int)Flag.Walked)
            DrawCellWithPen(x, y, walked_pen_);
        else if (state == (int)Flag.Block)
            DrawCellWithPen(x, y, block_pen_);
        else
            DrawCellWithPen(x, y, walkable_pen_);
    }

    void DrawCellWithPen(int x, int y, Pen pen) {
        graph.FillRectangle(pen.Brush, x * cell_sizex, (y) * cell_sizey, cell_sizex - space_, cell_sizey - space_);
    }

    bool GetPoint(int x, int y, ref Point ret) {
        int px = (int)(x / cell_sizex);
        int py = (int)(y / cell_sizey);
        if (px >= w_)
            return false;
        if (py >= h_)
            return false;
        ret = new Point(px, py);
        return true;
    }

    Point ret_;

    private void FindWindow_MouseUp(object sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Right) {
            if(GetPoint(e.X, e.Y, ref ret_)) {
                this.contextMenuStrip1.Show(this, e.Location);
            }
        }
    }
    private void FindWindow_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
        DrawMap();
    }
    private void FindWindow_Resize(object sender, EventArgs e) {
        OnResize();
        DrawMap();
    }

    void OnResize() {
        if (graph != null) {
            graph.Clear(this.BackColor);
            graph.Dispose();
        }
        graph = CreateGraphics();
        if (!MapInvalid)
            return;
        w_ = mapdata_.GetLength(0);
        h_ = mapdata_.GetLength(1);

        cell_sizex = (ClientSize.Width) / w_;
        cell_sizey = (ClientSize.Height) / h_;
        space_ = cell_sizex * 0.05f;
        if (space_ < 1f)
            space_ = 1f;
    }

    int GetState(int x, int y) {
        if (x < 0 || x > w_ || y < 0 || y > h_)
            return (int)Flag.Invlid;
        return mapdata_[x, y];
    }

    private void setStartToolStripMenuItem_Click(object sender, EventArgs e) {
        int state = GetState(ret_.X, ret_.Y);
        if (state == (int)NodeFlag.Block)
            return;

        state = GetState(start.X, start.Y);
        if (state == (int)Flag.StartPoint) {
            mapdata_[start.X, start.Y] = (int)NodeFlag.Walkable;
        }
        start = new Point(ret_.X, ret_.Y);
        mapdata_[ret_.X, ret_.Y] = (int)Flag.StartPoint;

        DrawMap();
    }

    private void setEndToolStripMenuItem_Click(object sender, EventArgs e) {
        int state = GetState(ret_.X, ret_.Y);
        if (state == (int)NodeFlag.Block)
            return;

        state = GetState(end.X, end.Y);
        if (state == (int)Flag.GoalPoint) {
            mapdata_[end.X, end.Y] = (int)Flag.Walkable;
        }
        end = new Point(ret_.X, ret_.Y);
        mapdata_[ret_.X, ret_.Y] = (int)Flag.GoalPoint;

        DrawMap();
    }
}
}
