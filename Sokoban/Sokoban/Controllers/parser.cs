using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sokoban.Models;

namespace Sokoban.Controllers
{
    class Parser
    {
        private const string mazeFileLocation = "../../Doolhof/doolhof";

        public static Maze Parse(int level)
        {
            List<List<Node>> nodesList = new List<List<Node>>();
            List<int[]> destinations = new List<int[]>();
            int numberOfCrates = 0;

            int x = 0;
            int y = 0;

            int truckX = 0;
            int truckY = 0;

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
                            nodesRow.Add(new Node(x, y)
                            {
                                Type = Node.NodeType.Wall
                            });
                            break;
                        case '.':
                            nodesRow.Add(new Node(x, y)
                            {
                                Type = Node.NodeType.Floor
                            });
                            break;
                        case 'o':
                            nodesRow.Add(new Node(x, y)
                            {
                                Type = Node.NodeType.Floor,
                                ContainsCrate = true
                            });
                            numberOfCrates++;
                            break;
                        case 'x':
                            nodesRow.Add(new Node(x, y)
                            {
                                Type = Node.NodeType.Destination
                            });
                            destinations.Add(new int[2] { x, y });
                            break;
                        case '@':
                            nodesRow.Add(new Node(x, y)
                            {
                                Type = Node.NodeType.Floor,
                                ContainsTruck = true
                            });

                            truckX = x;
                            truckY = y;
                            break;
                        default:
                            nodesRow.Add(new Node(x, y));
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

            Maze map = new Maze
            {
                mapNumber = level,

                nodes = nodesList.Select(l => l.ToArray()).ToArray(),
                Dimensions = new Dictionary<string, int>() {
                    { "x", x },
                    { "y", y }
                },

                Truck = new Dictionary<string, int>() {
                    { "x", truckX },
                    { "y", truckY }
                },

                destinations = destinations.ToArray(),
                numberOfCrates = numberOfCrates
            };

            return map;
        }

    }
}
