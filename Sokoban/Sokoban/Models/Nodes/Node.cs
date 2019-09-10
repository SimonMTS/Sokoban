using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Node
    {
        public const int INVALID = -1;

        public const int NORTH = 0;
        public const int EAST = 1;
        public const int SOUTH = 2;
        public const int WEST = 3;

        public bool ContainsTruck = false;
        public bool ContainsCrate = false;

        public int x;
        public int y;

        public Node(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
