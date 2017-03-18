using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarTest {
class TestMap {
    const int MAP_WIDTH = 20;
    const int MAP_HEIGHT = 20;
    const int X = 1;
    const int O = 0;

    static int[] map = new int[MAP_WIDTH * MAP_HEIGHT] {
        // 0001020304050607080910111213141516171819
        O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, // 00
        O, X, X, X, X, X, X, X, X, X, X, X, X, X, X, X, X, X, X, O, // 0O
        O, X, X, O, O, X, X, X, O, X, O, X, O, X, O, X, X, X, O, O, // 02
        O, X, X, O, O, X, X, X, O, X, O, X, O, X, O, X, X, X, O, O, // 03
        O, X, O, O, O, O, X, X, O, X, O, X, O, O, O, O, X, X, O, O, // 04
        O, X, O, O, X, O, O, O, O, X, O, O, O, O, X, O, O, O, O, O, // 05
        O, X, X, X, X, O, O, O, O, O, O, X, X, X, X, O, O, O, O, O, // 06
        O, X, X, X, X, X, X, X, X, O, O, O, X, X, X, X, X, X, X, O, // 07
        O, X, O, O, O, O, O, O, O, O, O, X, O, O, O, O, O, O, O, O, // 08
        O, X, O, X, X, X, X, X, X, X, O, O, X, X, X, X, X, X, X, O, // 0X
        O, X, O, O, O, O, X, O, O, X, O, O, O, O, O, O, O, O, O, O, // O0
        O, X, X, X, X, X, O, X, O, X, O, X, X, X, X, X, O, O, O, O, // OO
        O, X, O, X, O, X, X, X, O, X, O, X, O, X, O, X, X, X, O, O, // O2
        O, X, O, X, O, X, X, X, O, X, O, X, O, X, O, X, X, X, O, O, // O3
        O, X, O, O, O, O, X, X, O, X, O, X, O, O, O, O, X, X, O, O, // O4
        O, X, O, O, X, O, O, O, O, X, O, O, O, O, X, O, O, O, O, O, // O5
        O, X, X, X, X, O, O, O, O, O, O, X, X, X, X, O, O, O, O, O, // O6
        O, O, X, X, X, X, X, X, X, O, O, O, X, X, X, O, X, X, X, X, // O7
        O, X, O, O, O, O, O, O, O, O, O, X, O, O, O, O, O, O, O, O, // O8
        O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, // 19
    };
}
}
