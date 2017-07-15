using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStar {
public enum VistState {
    None,
    Opened = 1 << 0,
    Close = 1 << 1 | Opened,
    Optimized = 1 << 2 | Close,
}

public enum NodeFlag {
    Walkable = 0,
    Block = 1,
}

public class Node: IComparable {
    public int customState;
    public int state {
        get;
        internal set;
    }
    public int x {
        get;
        internal set;
    }
    public int y {
        get;
        internal set;
    }
    public int g {
        get;
        internal set;
    }
    public int h {
        get;
        internal set;
    }
    public int f {
        get;
        internal set;
    }
    public int index {
        get {
            return index_;
        }
        internal set {
            index_ = value;
        }
    }

    private int index_;

    internal VistState viststate = VistState.None;

    public bool closed {
        get {
            return ((int)viststate & (int)VistState.Close) > 0;
            //return viststate.HasFlag(VistState.Close);
        }
    }
    public bool opened {
        get {
            //return viststate.HasFlag(VistState.Opened);
            return ((int)viststate & (int)VistState.Opened) > 0;
        }
    }

    public bool blocked {
        get {
            return state == (int)NodeFlag.Block;
        }
    }

    internal Node parent;

    public void Clear() {
        g = 0;
        h = 0;
        f = int.MaxValue;
        viststate = VistState.None;
        parent = null;
    }


    public bool Is(int x, int y) {
        return this.x == x && this.y == y;
    }

    public  bool Equals(Node obj) {
        return Is(obj.x, obj.y);
    }

    internal void SetCost() {
        f = g + h;
    }

    public override string ToString() {
        return string.Format("state:{0},x:{1},y:{2}", state, x, y);
    }

    public int CompareTo(object obj) {
        return CompareTo((Node)obj);
    }

    public int CompareTo(Node other) {
        if (f > other.f)
            return 1;
        else if (f < other.f)
            return -1;
        else
            return 0;
    }
}
}
