using System;
using System.Collections.Generic;

namespace Sokoban.Models
{
    struct Maze
    {
        public int mapNumber;

        public Node[][] nodes;
        public Dictionary<string, int> Dimensions;

        public Dictionary<string, int> Truck;
        public Node TruckNode()
        {
            return this.nodes[this.Truck["x"]][this.Truck["y"]];
        }

        public void TruckNode(int x, int y)
        {
            this.nodes[this.Truck["x"]][this.Truck["y"]].ContainsTruck = false;

            this.nodes[x][y].ContainsTruck = true;
            this.Truck["x"] = x;
            this.Truck["y"] = y;
        }

        public int[][] destinations;
    }
}
