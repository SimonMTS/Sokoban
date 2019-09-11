using Sokoban.Controllers;
using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.Controllers.Parser;

namespace Sokoban.Models
{
    class Maze
    {
        public Map Map;

        public Maze(int level)
        {
            Map = Parser.Parse("../../Doolhof/doolhof"+level+".txt");
        }

        public void ApplyAction(int direction)
        {
            Node neighbour = getNeighbour(direction, Map.nodes[Map.truckX, Map.truckY]);

            bool neighbourIsCrate = neighbour.ContainsCrate;

            if (neighbour is FloorNode && !neighbourIsCrate)
            {
                neighbour.ContainsTruck = true;

                Map.nodes[Map.truckX, Map.truckY].ContainsTruck = false;
                Map.truckX = neighbour.x;
                Map.truckY = neighbour.y;
            }
            else if (neighbour is FloorNode && neighbourIsCrate)
            {
                Node neighbour2 = getNeighbour(direction, Map.nodes[neighbour.x, neighbour.y]);

                if (neighbour2 is FloorNode && !neighbour2.ContainsCrate)
                {
                    neighbour.ContainsTruck = true;
                    neighbour.ContainsCrate = false;
                    neighbour2.ContainsCrate = true;

                    Map.nodes[Map.truckX, Map.truckY].ContainsTruck = false;
                    Map.truckX = neighbour.x;
                    Map.truckY = neighbour.y;
                }
            }
        }

        public bool HasWon()
        {
            foreach (int[] dest in Map.destinations)
            {
                if (!Map.nodes[dest[0], dest[1]].ContainsCrate)
                {
                    return false;
                }
            }

            return true;
        }

        private Node getNeighbour(int dir, Node me)
        {
            Node neighbour = null;
            if (dir == Node.NORTH)
            {
                neighbour = Map.nodes[me.x-1, me.y];
            }
            else if (dir == Node.EAST)
            {
                neighbour = Map.nodes[me.x, me.y+1];
            }
            else if (dir == Node.SOUTH)
            {
                neighbour = Map.nodes[me.x+1, me.y];
            }
            else if (dir == Node.WEST)
            {
                neighbour = Map.nodes[me.x, me.y-1];
            }

            return neighbour;
        }
    }
}
