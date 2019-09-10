using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models
{
    class Maze
    {
        private Node[][] nodes;

        public Maze(int _x, int _y)
        {
            // init nodes
            for (int x = 0; x < _x; x++)
            {
                for (int y = 0; y < _y; y++)
                {
                    nodes[x][y] = new Node();
                }
            }

            // set node neighbours
            for (int x = 0; x < _x; x++)
            {
                for (int y = 0; y < _y; y++)
                {
                    //nodes[x][y];
                }
            }
        }
    }
}
