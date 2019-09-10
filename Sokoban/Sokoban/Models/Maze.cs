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

        public Maze()
        {
            // init Map.nodes
            Map = Parser.Parse();

            // set node neighbours
            for (int x = 0; x < Map.x; x++)
            {
                for (int y = 0; y < Map.y; y++)
                {
                    if (y-1 >= 0 && x + 1 < Map.x && y + 1 < Map.y && x - 1 >= 0)
                    {
                        Node[] neighbours = {
                            Map.nodes[x - 1, y],
                            Map.nodes[x, y + 1],
                            Map.nodes[x + 1, y],
                            Map.nodes[x, y - 1]
                        };
                    }
                }
            }
        }

        public void ApplyAction(int direction)
        {
            Node neighbour = null;
            if (direction == Node.NORTH)
            {
                neighbour = Map.nodes[Map.truckX-1, Map.truckY];
            }
            else if (direction == Node.EAST)
            {
                neighbour = Map.nodes[Map.truckX, Map.truckY+1];
            }
            else if (direction == Node.SOUTH)
            {
                neighbour = Map.nodes[Map.truckX+1, Map.truckY];
            }
            else if (direction == Node.WEST)
            {
                neighbour = Map.nodes[Map.truckX, Map.truckY-1];
            }

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
                Node neighbour2 = null;
                if (direction == Node.NORTH)
                {
                    neighbour2 = Map.nodes[Map.truckX - 2, Map.truckY];
                }
                else if (direction == Node.EAST)
                {
                    neighbour2 = Map.nodes[Map.truckX, Map.truckY + 2];
                }
                else if (direction == Node.SOUTH)
                {
                    neighbour2 = Map.nodes[Map.truckX + 2, Map.truckY];
                }
                else if (direction == Node.WEST)
                {
                    neighbour2 = Map.nodes[Map.truckX, Map.truckY - 2];
                }

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

            HasWon();
        }

        public void HasWon()
        {
            foreach (int[] dest in Map.destinations)
            {
                if (!Map.nodes[dest[0], dest[1]].ContainsCrate)
                {
                    return;
                }
            }

            Console.Clear();
            Console.WriteLine("You did it, you stopped racism!");
            Console.ReadLine();
        }
    }
}
