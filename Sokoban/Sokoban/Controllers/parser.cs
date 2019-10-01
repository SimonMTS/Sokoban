using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sokoban.Models;
using Sokoban.Models.Movables;
using Sokoban.Models.Nodes;

namespace Sokoban.Controllers
{
    class Parser
    {
        private const string mazeFileLocation = "../../Doolhof/doolhof";

        public static Maze Parse(int level)
        {
            List<List<Node>> nodesList = new List<List<Node>>();
            List<int[]> destinations = new List<int[]>();

            int x = 0;
            int y = 0;

            Truck PlayerTruck = null;
            List<Truck> Trucks = new List<Truck>();
            List<Crate> Crates = new List<Crate>();

            {
                List<Node> nodesRow = new List<Node>();
                StreamReader reader;
                reader = new StreamReader(mazeFileLocation + level + ".txt");

                while (!reader.EndOfStream)
                {
                    char ch = (char)reader.Read();
                    switch (ch)
                    {
                        case '#':
                            nodesRow.Add(new WallNode(x, y));
                            break;
                        case '.':
                            nodesRow.Add(new FloorNode(x, y));
                            break;
                        case 'o':
                            nodesRow.Add(new FloorNode(x, y));

                            Crates.Add(new Crate()
                            {
                                x = x,
                                y = y
                            });
                            break;
                        case 'x':
                            nodesRow.Add(new DestinationNode(x, y));
                            destinations.Add(new int[2] { x, y });
                            break;
                        case '~':
                            nodesRow.Add(new BrokenFloorNode(x, y));
                            break;
                        case '@':
                            nodesRow.Add(new FloorNode(x, y));

                            PlayerTruck = new Truck()
                            {
                                x = x,
                                y = y
                            };
                            break;
                        case '$':
                            nodesRow.Add(new FloorNode(x, y));

                            Trucks.Add(new AITruck()
                            {
                                x = x,
                                y = y
                            });
                            break;
                        default:
                            nodesRow.Add(new WallNode(x, y));
                            break;
                    }

                    y++;
                    if (ch.Equals('\n'))
                    {
                        nodesList.Add(nodesRow);
                        nodesRow = new List<Node>();

                        x++;
                        y = 0;
                    }
                }
                nodesList.Add(nodesRow); x++;

                reader.Close();
                reader.Dispose();
            }

            Trucks.Insert(0, PlayerTruck);

            Maze map = new Maze
            {
                mapNumber = level,

                nodes = nodesList.Select(l => l.ToArray()).ToArray(),
                Dimensions = new Dictionary<string, int>() {
                    { "x", x },
                    { "y", y }
                },

                Trucks = Trucks.ToArray(),
                Crates = Crates.ToArray(),
                destinations = destinations.ToArray()
            };

            return map;
        }

    }
}
