using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ALib {
namespace AStar {
internal class OpenNodeComparer : IComparer<Node> {
    int IComparer<Node>.Compare(Node x, Node y) {
        if (x.f > y.f)
            return 1;
        if (x.f < y.f)
            return -1;
        if (x.index > y.index)
            return -1;
        if (x.index < y.index)
            return 1;
        return 0;
    }
}
public class Finder {
    private Node[,] mapdata_;
    private int width_ = 0;
    private int height_ = 0;
    private SortedSet<Node> openlist_;
    private HashSet<Node> closelist_;

    private Node goal_;
    private Node start_;
    private List<Node> path_;

    public int node_count {
        get {
            return width_ * height_;
        }
    }

    public bool invlid {
        get {
            return mapdata_ != null;
        }
    }

    public List<Node> path {
        get {
            return path_;
        }
    }

    public bool optimized = false;


    public bool SetMap(int[,] data) {
        if (data == null)
            return false;

        height_ = data.GetLength(0);
        if (height_ == 0)
            return false;

        width_ = data.GetLength(1);
        if (width_ == 0)
            return false;

        mapdata_ = new Node[height_, width_];
        path_ = new List<Node>(width_ * height_);

        for (int i = 0; i < width_; ++i) {
            for (int j = 0; j < height_; ++j) {
                var node = new Node();
                mapdata_[j, i] = node;
                node.x = i;
                node.y = j;
                node.index = j * width_ + i;
                node.state = data[j, i];
            }
        }

        openlist_ = new SortedSet<Node>(new OpenNodeComparer());
        closelist_ = new HashSet<Node>();
        return true;
    }

    public bool IsBlock(int col, int row) {
        return mapdata_[row, col].state == (int)NodeFlag.Block;
    }

    bool InvalidIndex(int col, int row) {
        return col >= 0 && col < width_ && row >= 0 && row < height_;
    }

    public Node GetNode(int col, int row) {
        return mapdata_[row, col];
    }

    public bool Find(int x0, int y0, int x1, int y1) {
        if (!Ready(x0, y0, x1, y1))
            return false;

        Node current = null;
        do {
            current = GetBestFromOpenList();
            if (current == null)
                break;
            if (current.Equals(goal_))
                break;
            if (!ProcessNeigbhors(current))
                break;
        } while (true);

        if (goal_.parent != null)
            current = goal_;

        if (current != null && current.Equals(goal_)) {
            GeneratePath(current);
            return true;
        }
        return false;
    }





    bool Ready(int x0, int y0, int x1, int y1) {
        if (!InvalidIndex(x0, y0) || !InvalidIndex(x1, y1))
            return false;
        if (IsBlock(x0, y0) || IsBlock(x1, y1))
            return false;

        for (int i = 0; i < width_; ++i) {
            for (int j = 0; j < height_; ++j) {
                var node = mapdata_[j, i];
                node.Clear();
            }
        }

        openlist_.Clear();
        closelist_.Clear();

        start_ = GetNode(x0, y0);
        goal_ = GetNode(x1, y1);

        start_.h = GetH(start_);
        start_.SetCost();
        start_.viststate = VistState.Opened;
        openlist_.Add(start_);

        return true;
    }

    const int G0 = 10;
    const int G1 = 14;

    struct Point {
        public int x;
        public int y;
        public int g;
    }

    static Point[] sNeigbhors = {
        new Point() {y = -1, x = -1, g = G1},
        new Point() {y = -1, x = 0, g = G0},
        new Point() {y = -1, x = 1, g = G1},

        new Point() {y = 0, x = -1, g = G0},
        new Point() {y = 0, x = 1, g = G0 },

        new Point() {y = 1, x = -1, g = G1 },
        new Point() {y = 1, x = 0, g = G0},
        new Point() {y = 1, x = 1, g = G1},
    };

    bool ProcessNeigbhors(Node current) {

        int parentx = current.x;
        int parenty = current.y;

        for (int i = 0; i < 8; ++i) {
            int x = parentx + sNeigbhors[i].x;
            int y = parenty + sNeigbhors[i].y;
            if (!InvalidIndex(x,y))
                continue;
            Node node = GetNode(x,y);
            if (node.closed)
                continue;
            if (node.opened)
                continue;
            if (node.blocked) {
                node.viststate = VistState.Close;
                closelist_.Add(node);
                continue;
            }

            node.parent = current;
            node.g = current.g + sNeigbhors[i].g;
            node.h = GetH(node);
            node.SetCost();
            node.viststate = VistState.Opened;
            openlist_.Add(node);

            //if (node.Equals(goal_)) {
            //    return false;
            //}
        }
        return true;
    }

    int GetH(Node node) {
        //return 0;
        return (int)(Math.Sqrt((node.x - goal_.x) * (node.x - goal_.x) + (node.y - goal_.y) * (node.y - goal_.y)) * 9.5f);
    }

    Node GetBestFromOpenList() {
        Node ret = null;
        if (openlist_.Count > 0) {
            ret = openlist_.Min;
            ret.viststate = VistState.Close;
            //Debug.Assert(!closelist_.Contains(ret));
            closelist_.Add(ret);
            openlist_.Remove(ret);
        }
        return ret;
    }


    Node GetBestNodeFromVisited(Node current) {
        int parentx = current.x;
        int parenty = current.y;
        Node min_g_node = null;
        int minG = int.MaxValue;
        int inverseG = int.MaxValue;

        for (int i = 0; i < 8; ++i) {
            int x = parentx + sNeigbhors[i].x;
            int y = parenty + sNeigbhors[i].y;
            if (!InvalidIndex(x, y))
                continue;
            Node node = GetNode(x, y);
            if(((int)node.viststate & (int)VistState.Opened) > 0 && node.viststate != VistState.Optimized&&!node.blocked) {
                inverseG = node.g + node.f+sNeigbhors[i].g;
                //;
                if (inverseG < minG) {
                    min_g_node = node;
                    minG = inverseG;
                }
            }
            node.viststate = VistState.Optimized;
        }
        return min_g_node;
    }

    void GeneratePath(Node current) {
        path_.Clear();
        if(optimized) {
            while (!path.Contains(current)) {
                path_.Add(current);
                if (current.Equals(start_))
                    break;
                current = GetBestNodeFromVisited(current);
            }
        } else {
            while (current != null && !path.Contains(current)) {
                path_.Add(current);
                current = current.parent;
            }
        }
        path_.Reverse();

        Debug.Assert(openlist_.Count < this.node_count);
        Debug.Assert(closelist_.Count < this.node_count);
    }
}
}
}
