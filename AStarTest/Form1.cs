using AStar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarTest {
public class Point2 {
    public int col;
    public int row;

    public Point2(int col, int row) {
        this.col = col;
        this.row = row;
    }
}
public partial class Form1 : Form {
    Finder finder = new Finder();
    int width = 60;
    int height = 60;
    int block_rate = 30;
    Random random = new Random();
    public Form1() {
        InitializeComponent();
        finder = new Finder();
        GeneratePath();
    }
    public void FindPath() {
        Stopwatch watcher = Stopwatch.StartNew();
        //finder.optimized = this.checkBox1.CheckState == CheckState.Checked;

        if (this.findWindow1.start == null || this.findWindow1.end == null)
            return;

        bool ret = finder.Find(this.findWindow1.start.col, this.findWindow1.start.row
                               , this.findWindow1.end.col, this.findWindow1.end.row);
        if(ret) {
            this.findWindow1.SetResult(finder.path);
        } else {
            this.findWindow1.DrawMap();
            MessageBox.Show("Find Field!");
        }
        watcher.Stop();
        Debug.WriteLine("Time:" + watcher.Elapsed.ToString());
    }
    //int[,] mapdata_ = new int[10, 10] {
    //    {1, 1, 0, 0, 0, 0, 0, 0, 0,0, },
    //    {0, 0, 0, 0, 0, 0, 0, 0, 0,0, },
    //    {0, 0, 0, 0, 0, 0, 0, 0, 0,0, },
    //    {0, 0, 0, 0, 1, 1, 1, 1, 0,1, },
    //    {0, 0, 0, 0, 1, 0, 0, 0, 0,1, },
    //    {0, 0, 0, 0, 1, 0, 0, 0, 0,1, },
    //    {0, 0, 0, 0, 1, 0, 1, 1, 1,1, },
    //    {0, 0, 0, 0, 1, 0, 1, 0, 0,1, },
    //    {0, 0, 0, 0, 1, 0, 1, 0, 0,1, },
    //    {0, 0, 0, 0, 1, 0, 1, 0, 0,1, },
    //};
    //int[,] mapdata_ = new int[20, 20] {
    //    {1, 1, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 1, 1, 1, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 1, 1, 1,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 1, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 1, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 1, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 1, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 1, 1, 1, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //    {0, 0, 0, 0, 0, 1, 0, 0, 0, 0,1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //};
    public void GeneratePath() {
        finder.InitMap(width, height);
        for (int i = 0; i < width; ++i) {
            for (int j = 0; j < height; ++j) {
                finder.SetState(i, j, random.Next(0, 100) < block_rate ? NodeFlag.Block : (int)NodeFlag.Walkable);
            }
        }
        var nodes = finder.nodes;
        int row = nodes.GetLength(0);
        int col = nodes.GetLength(1);
        int[,] rawDatas = new int[row, col];
        for(int i = 0; i < col; ++i) {
            for (int j = 0; j < row; ++j) {
                rawDatas[i, j] = nodes[i, j].state;
            }
        }
        this.findWindow1.finder = finder;
        this.findWindow1.SetMap(rawDatas);
    }


    private void generate_map_Click(object sender, EventArgs e) {
        GeneratePath();
    }

    private void find_Click(object sender, EventArgs e) {
        this.FindPath();
    }
}
}
