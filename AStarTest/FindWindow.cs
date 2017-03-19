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
    private Pen font_pen_;
    private Font f_font_;
    private float cell_sizex = 40f;
    private float cell_sizey = 40f;

    private float space_ = 1f;



    public Point2 start ;
    public Point2 end;
    public Finder finder;
    Point2 ret_;

    public FindWindow() {
        InitializeComponent();
        InitGraph();
    }

    void InitGraph() {
        graph = CreateGraphics();

        //block_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.Green, Color.Gray));
        //walked_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.White, Color.Gray));

        //walkable_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.Gray, Color.Gray));
        //start_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.Red, Color.Gray));
        //end_pen_ = new Pen(new HatchBrush(HatchStyle.DiagonalCross, Color.SkyBlue, Color.Gray));

        block_pen_ = new Pen(new SolidBrush(Color.Green));
        walked_pen_ = new Pen(new SolidBrush(Color.DarkKhaki));
        walkable_pen_ = new Pen(new SolidBrush(Color.Gray));
        start_pen_ = new Pen(new SolidBrush(Color.Red));
        end_pen_ = new Pen(new SolidBrush(Color.SkyBlue));
        font_pen_ = new Pen(new SolidBrush(Color.White));

        f_font_ = DefaultFont;
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

    void SetMapState(int col,int row,int state) {
        mapdata_[row, col] = state;
    }

    int GetMapState(int col, int row) {
        return mapdata_[row, col];
    }

    void ClearWalkstate() {
        for (int i = 0; i < w_; ++i) {
            for (int j = 0; j < h_; ++j) {
                var state = GetMapState(i, j);
                if (state == (int)Flag.Walked)
                    SetMapState(i, j, (int)Flag.Walkable);
            }
        }
    }
    public void SetResult(List<Node> path) {
        if (path == null)
            return;
        ClearWalkstate();
        foreach (var node in path) {
            SetMapState(node.x, node.y, (int)Flag.Walked);
        }
        DrawMap();
    }


    public void DrawMap() {

        if (!MapInvalid)
            return;
        for (int i = 0; i < w_; ++i) {
            for (int j = 0; j < h_; ++j) {
                DrawCell(i, j);
            }
        }
    }

    bool Is(int state, NodeFlag flag) {
        return (state & (int)flag) > 0;
    }

    void DrawCell(  int col, int row) {
        int state = GetState(col, row);
        Node node = finder.GetNode(col, row);
        if (state == (int)Flag.StartPoint)
            DrawCellWithPen(col, row, start_pen_);
        else if (state == (int)Flag.GoalPoint)
            DrawCellWithPen(col, row, end_pen_);
        else if (state == (int)Flag.Walked) {
            DrawCellWithPen(col, row, walked_pen_);
        } else if (state == (int)Flag.Block)
            DrawCellWithPen(col, row, block_pen_);
        else
            DrawCellWithPen(col, row, walkable_pen_);

        if (node.opened || node.closed && !node.blocked) {
            DrawString(col, row, font_pen_, node.f.ToString(), 0);
            DrawString(col, row, font_pen_, node.g.ToString(), 0, 10);
        }
    }

    void DrawCellWithPen(int col, int row,  Pen pen) {
        graph.FillRectangle(pen.Brush, col * cell_sizex, (row) * cell_sizey, cell_sizex - space_, cell_sizey - space_);
    }

    void DrawString(int col, int row,  Pen pen,string text,int offsetx=0,int offsety=0) {
        graph.DrawString(text, f_font_, pen.Brush, col * cell_sizex+ offsetx, (row) * cell_sizey+ offsety);
    }

    bool GetPoint(int x, int y, ref Point2 ret) {
        int px = (int)(x / cell_sizex);
        int py = (int)(y / cell_sizey);
        if (px >= w_)
            return false;
        if (py >= h_)
            return false;
        ret = new Point2(px, py);
        return true;
    }


    private void FindWindow_MouseUp(object sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Right) {
            if(GetPoint(e.X, e.Y, ref ret_)) {
                if (GetState(ret_.col, ret_.row) == (int)Flag.Block)
                    return;
                this.contextMenuStrip1.Show(this, e.Location);
            }
        }
    }
    private void FindWindow_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
        DrawMap();
    }
    private void FindWindow_Resize(object sender, EventArgs e) {
        //OnResize();
        //DrawMap();
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
        float min_size = Math.Min(cell_sizex, cell_sizey);
        cell_sizex = min_size;
        cell_sizey = min_size;
        space_ = cell_sizex * 0.05f;
        if (space_ < 1f)
            space_ = 1f;
    }

    int GetState(int col, int row) {
        if (col < 0 || col > w_ || row < 0 || row > h_)
            return (int)Flag.Invlid;
        return mapdata_[row, col];
    }

    private void setStartToolStripMenuItem_Click(object sender, EventArgs e) {
        int state = GetState(ret_.col, ret_.row);
        if (state == (int)NodeFlag.Block)
            return;

        state = GetState(ret_.col, ret_.row);
        if (state == (int)Flag.StartPoint) {
            SetMapState(ret_.col,ret_.row, (int)NodeFlag.Walkable);
        }
        start = new Point2(ret_.col, ret_.row);
        SetMapState(ret_.col, ret_.row, (int)Flag.StartPoint);

        DrawMap();
    }

    private void setEndToolStripMenuItem_Click(object sender, EventArgs e) {
        int state = GetState(ret_.col, ret_.row);
        if (state == (int)NodeFlag.Block)
            return;

        state = GetState(ret_.col, ret_.row);
        if (state == (int)Flag.GoalPoint) {
            SetMapState(ret_.col, ret_.row, (int)NodeFlag.Walkable);
        }
        end = new Point2(ret_.col, ret_.row);
        SetMapState(ret_.col, ret_.row, (int)Flag.GoalPoint);

        DrawMap();
    }
}
}
