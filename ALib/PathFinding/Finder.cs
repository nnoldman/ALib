using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ALib {
namespace AStar {
public class Finder {
    private Node[,] mapdata_;
    private int width_ = 0;
    private int height_ = 0;
    private List<Node> openlist_;
    private List<Node> closelist_;

    private Node goal_;
    private List<Node> path_;

    public bool Invlid {
        get {
            return mapdata_ != null;
        }
    }

    public List<Node> path {
        get {
            return path_;
        }
    }


    public bool SetMap(int[,] data) {
        if (data == null)
            return false;

        width_ = data.GetLength(0);
        if (width_ == 0)
            return false;

        height_ = data.GetLength(1);
        if (height_ == 0)
            return false;

        mapdata_ = new Node[width_, height_];
        path_ = new List<Node>(width_ * height_);

        for (int i = 0; i < width_; ++i) {
            for (int j = 0; j < height_; ++j) {
                var node = new Node();
                mapdata_[i, j] = node;
                node.x = i;
                node.y = j;
                node.state = data[i, j];
            }
        }

        openlist_ = new List<Node>(width_ * height_);
        closelist_ = new List<Node>(width_ * height_);
        return true;
    }

    public bool IsBlock(int x, int y) {
        return mapdata_[x, y].state == (int)NodeFlag.Block;
    }

    bool InvalidIndex(int x, int y) {
        return x >= 0 && x < width_ && y >= 0 && y < height_;
    }

    Node GetNode(int x, int y) {
        return mapdata_[x, y];
    }

    public bool Find(int x0, int y0, int x1, int y1) {
        if (!Ready(x0, y0, x1, y1))
            return false;

        Node current = null;
        do {
            current = GetBestFromOpenList();
            if (current == null)
                break;
            ProcessNeigbhors(current);
        } while (!current.Equals(goal_));

        if (current != null && current.Equals(goal_)) {
            GeneratePath(current);
            return true;
        }
        return false;
    }

    const int G0 = 1000;
    const int G1 = 1414;

    struct Point {
        public int x;
        public int y;
        public int g;
    }

    Point[] Neigbhors = {
        new Point() {x = -1, y = -1, g = G1},
        new Point() {x = 0, y = -1, g = G0},
        new Point() {x = 1, y = -1, g = G1},

        new Point() {x = -1, y = 0, g = G0},
        new Point() {x = 1, y = 0, g = G0 },

        new Point() {x = -1, y = 1, g = G1 },
        new Point() {x = 0, y = 1, g = G0},
        new Point() {x = 1, y = 1, g = G1},
    };

    bool Ready(int x0, int y0, int x1, int y1) {
        if (!InvalidIndex(x0, y0) || !InvalidIndex(x1, y1))
            return false;
        if (IsBlock(x0, y0) || IsBlock(x1, y1))
            return false;

        for (int i = 0; i < width_; ++i) {
            for (int j = 0; j < height_; ++j) {
                var node = mapdata_[i, j];
                node.Clear();
            }
        }

        openlist_.Clear();
        closelist_.Clear();

        var start = GetNode(x0, y0);
        goal_ = GetNode(x1, y1);

        start.h = GetH(start);
        start.SetCost();
        start.viststate = VistState.Opened;
        openlist_.Add(start);

        return true;
    }

    void ProcessNeigbhors(Node current) {
        int parentx = current.x;
        int parenty = current.y;

        for(int i = 0; i < 8; ++i) {
            int x = parentx + Neigbhors[i].x;
            int y = parenty + Neigbhors[i].y;
            if (!InvalidIndex(x, y))
                continue;
            Node node = GetNode(x, y);
            if (node.closed)
                continue;
            if (node.opened)
                continue;
            if (node.IsBlock()) {
                node.viststate = VistState.Close;
                closelist_.Add(node);
                continue;
            }

            node.parent = current;
            node.g = current.g + Neigbhors[i].g;
            node.h = GetH(node);
            node.SetCost();
            node.viststate = VistState.Opened;
            openlist_.Add(node);
        }
    }

    int GetH(Node node) {
        //return 0;
        return (int)Math.Sqrt((node.x - goal_.x) * (node.x - goal_.x) + (node.y - goal_.y) * (node.y - goal_.y)) * 10;
    }

    Node GetBestFromOpenList() {
        Node ret = null;
        if (openlist_.Count > 0) {
            int mincost = int.MaxValue;
            int index = -1;
            for (int i = 0; i < openlist_.Count; ++i) {
                int cost = openlist_[i].f;
                if (cost < mincost) {
                    mincost = cost;
                    index = i;
                }
            }
            if(index != -1) {
                ret = openlist_[index];
                ret.viststate = VistState.Close;
                closelist_.Add(ret);
                openlist_.RemoveAt(index);
            }
        }

        return ret;
    }

    void GeneratePath(Node current) {
        path_.Clear();
        while (current != null) {
            path_.Add(current);
            current = current.parent;
        }
        path_.Reverse();
    }
}
}
}
