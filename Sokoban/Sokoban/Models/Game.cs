using System;
using Sokoban.Controllers;
using static Sokoban.Controllers.Controller;
namespace Sokoban.Models
{
    class Game
    {
        public Maze Map;

        public Game(int level)
        {
            Map = Parser.Parse(level);

            for (int x = 0; x < Map.Dimensions["x"]; x++)
            {
                for (int y = 0; y < Map.Dimensions["y"]; y++)
                {
                    if (y - 1 >= 0 && x + 1 < Map.Dimensions["x"] && y + 1 < Map.Dimensions["y"] && x - 1 >= 0)
                    {
                        Map.nodes[x][y].setNeighbours(new Node[] {
                            Map.nodes[x - 1][y],
                            Map.nodes[x][y + 1],
                            Map.nodes[x + 1][y],
                            Map.nodes[x][y - 1]
                        });
                    }
                }
            }
        }

        public void Move(GameAction direction)
        {
            Node neighbour = Map.TruckNode().getNeighbour(direction);

            if (neighbour.Type != Node.NodeType.Wall && neighbour.ContainsCrate)
            {
                MoveWithCrate(direction, neighbour);
            }
            else if (neighbour.Type != Node.NodeType.Wall)
            {
                neighbour.ContainsTruck = true;

                Map.TruckNode(neighbour.x, neighbour.y);
            }
        }

        private void MoveWithCrate(GameAction direction, Node neighbour)
        {
            Node neighbour2 = neighbour.getNeighbour(direction);

            if (neighbour2.Type != Node.NodeType.Wall && !neighbour2.ContainsCrate)
            {
                neighbour.ContainsTruck = true;
                neighbour.ContainsCrate = false;
                neighbour2.ContainsCrate = true;

                Map.TruckNode(neighbour.x, neighbour.y);
            }
        }

        public bool HasWon()
        {
            foreach (int[] dest in Map.destinations)
            {
                if (!Map.nodes[dest[0]][dest[1]].ContainsCrate)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
