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
            var map = CreateMaze(level);

            map = SetMazeReferences(map);

            return map;
        }

        private static Maze CreateMaze(int level)
        {
            List<List<Node>> nodesList = new List<List<Node>>();
            List<int[]> destinations = new List<int[]>();

            int DimensionX = 0;
            int DimensionY = 0;

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
                            nodesRow.Add(new WallNode(DimensionX, DimensionY));
                            break;
                        case '.':
                            nodesRow.Add(new FloorNode(DimensionX, DimensionY));
                            break;
                        case 'o':
                            nodesRow.Add(new FloorNode(DimensionX, DimensionY));

                            Crates.Add(new Crate()
                            {
                                x = DimensionX,
                                y = DimensionY
                            });
                            break;
                        case 'x':
                            nodesRow.Add(new DestinationNode(DimensionX, DimensionY));
                            destinations.Add(new int[2] { DimensionX, DimensionY });
                            break;
                        case '~':
                            nodesRow.Add(new BrokenFloorNode(DimensionX, DimensionY));
                            break;
                        case '@':
                            nodesRow.Add(new FloorNode(DimensionX, DimensionY));

                            PlayerTruck = new Truck()
                            {
                                x = DimensionX,
                                y = DimensionY
                            };
                            break;
                        case '$':
                            nodesRow.Add(new FloorNode(DimensionX, DimensionY));

                            Trucks.Add(new AITruck()
                            {
                                x = DimensionX,
                                y = DimensionY
                            });
                            break;
                        default:
                            nodesRow.Add(new WallNode(DimensionX, DimensionY));
                            break;
                    }

                    DimensionY++;
                    if (ch.Equals('\n'))
                    {
                        nodesList.Add(nodesRow);
                        nodesRow = new List<Node>();

                        DimensionX++;
                        DimensionY = 0;
                    }
                }
                nodesList.Add(nodesRow); DimensionX++;

                reader.Close();
                reader.Dispose();
            }

            Trucks.Insert(0, PlayerTruck);

            Maze map = new Maze
            {
                mapNumber = level,

                nodes = nodesList.Select(l => l.ToArray()).ToArray(),
                Dimensions = new Dictionary<string, int>() {
                    { "x", DimensionX },
                    { "y", DimensionY }
                },

                Trucks = Trucks.ToArray(),
                Crates = Crates.ToArray(),
                destinations = destinations.ToArray()
            };

            // "In een doolhof… zijn evenveel bestemmingen als kisten."
            while (map.destinations.Length > map.Crates.Length)
            {
                // there are more destinations then crates, so we'll pick one destination and remove it.
                int RandomNumber = (new Random()).Next(map.destinations.Length - 1);
                int[] dest = map.destinations[RandomNumber];

                // remove from destinations array
                map.destinations = map.destinations.Where(d => d[0] != dest[0] || d[1] != dest[1]).ToArray();

                // replace in nodes array
                map.nodes[dest[0]][dest[1]] = new FloorNode(dest[0], dest[1]);
            }

            return map;
        }

        private static Maze SetMazeReferences(Maze map)
        {
            for (int x = 0; x < map.Dimensions["x"]; x++)
            {
                for (int y = 0; y < map.Dimensions["y"]; y++)
                {
                    if (y - 1 >= 0 && x + 1 < map.Dimensions["x"] && y + 1 < map.Dimensions["y"] && x - 1 >= 0)
                    {
                        map.nodes[x][y].setNeighbours(new Node[] {
                            map.nodes[x - 1][y],
                            map.nodes[x][y + 1],
                            map.nodes[x + 1][y],
                            map.nodes[x][y - 1]
                        });
                    }
                    map.nodes[x][y].Map = map;
                }
            }

            for (int i = 0; i < map.Trucks.Length; i++)
            {
                map.Trucks[i].Map = map;
            }

            for (int i = 0; i < map.Crates.Length; i++)
            {
                map.Crates[i].Map = map;
            }

            return map;
        }
    }
}
