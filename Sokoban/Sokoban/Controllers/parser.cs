using Sokoban.Models;
using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Controllers
{
    class Parser
    {
        public struct Map
        {
            public Node[,] nodes;
            public int x;
            public int y;

            public int truckX;
            public int truckY;

            public List<int[]> destinations;
        }

        public static Map Parse()
        {
            int _x = 100, _y = 100;

            Node[,] nodes = new Node[_x, _y];
            List<int[]> destinations = new List<int[]>();


            int x = 0;
            int y = 0;
            int truckX = 0;
            int truckY = 0;
            {
                StreamReader reader;
                reader = new StreamReader("../../Doolhof/doolhof1.txt");
                while (!reader.EndOfStream)
                {
                    char ch = (char)reader.Read();
                    Console.Write(ch);

                    switch (ch)
                    {
                        case '#':
                            nodes[x, y] = new WallNode(x, y);
                            break;
                        case '.':
                            nodes[x, y] = new FloorNode(x, y);
                            break;
                        case 'o':
                            nodes[x, y] = new FloorNode(x, y);
                            nodes[x, y].ContainsCrate = true;
                            break;
                        case 'x':
                            nodes[x, y] = new DestinationNode(x, y);
                            destinations.Add(new int[2] { x, y });
                            break;
                        case '@':
                            nodes[x, y] = new FloorNode(x, y);
                            nodes[x, y].ContainsTruck = true;

                            truckX = x;
                            truckY = y;
                            break;
                        default:
                            nodes[x, y] = new Node(x, y);
                            break;
                    }

                    y++;
                    if (ch.Equals('\n'))
                    {
                        x++;
                        y = 0;
                        Console.WriteLine("");
                    }
                }
                reader.Close();
                reader.Dispose();
                Console.ReadKey();
            }

            Map map = new Map
            {
                x = x+1,
                y = y,
                nodes = nodes,

                truckX = truckX,
                truckY = truckY,

                destinations = destinations
            };

            return map;
        }

    }
}
