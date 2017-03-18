using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALib {
namespace AStar {
public enum VistState {
    None,
    Opened,
    Close,
}

public enum NodeFlag {
    Walkable = 0,
    Block = 1,
}

public class Node {
    public int x;
    public int y;
    public int state;
    public int g;
    public int h;
    public int f;

    public VistState viststate = VistState.None;

    public bool closed {
        get {
            return viststate == VistState.Close;
        }
    }
    public bool opened {
        get {
            return viststate == VistState.Opened;
        }
    }

    public Node parent;

    public void Clear() {
        g = 0;
        h = 0;
        f = int.MaxValue;
        viststate = VistState.None;
        parent = null;
    }

    public bool IsBlock() {
        return state == (int)NodeFlag.Block;
    }

    public bool Is(int x, int y) {
        return this.x == x && this.y == y;
    }

    public  bool Equals(Node obj) {
        return Is(obj.x, obj.y);
    }

    public void SetCost() {
        f = g + h;
    }

    public override string ToString() {
        return string.Format("state:{0},x:{1},y:{2}", state, x, y);
    }
}
}
}