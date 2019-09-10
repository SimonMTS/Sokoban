using Sokoban.Models;
using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.Controllers.Parser;

namespace Sokoban.Views
{
    class OutputView
    {
        public static void draw(Map map)
        {
            Console.Clear();

            for (int x = 0; x < map.x; x++)
            {
                for (int y = 0; y < map.y; y++)
                {
                    if (map.nodes[x, y] == null)
                    {
                        continue;
                    }

                    if (map.nodes[x, y].GetType() == typeof(WallNode))
                    {
                        Console.Write("█");
                    }
                    else if (map.nodes[x, y].GetType() == typeof(FloorNode))
                    {
                        if (map.nodes[x, y].ContainsTruck)
                        {
                            Console.Write("@");
                        }
                        else if (map.nodes[x, y].ContainsCrate)
                        {
                            Console.Write("o");
                        }
                        else
                        {
                            Console.Write("·");
                        }
                    }
                    else if (map.nodes[x, y].GetType() == typeof(DestinationNode))
                    {
                        if (map.nodes[x, y].ContainsTruck)
                        {
                            Console.Write("@");
                        }
                        else
                        {
                            if (map.nodes[x, y].ContainsCrate)
                            {
                                Console.Write("0");
                            }
                            else
                            {
                                Console.Write("x");
                            }
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }
        }
    }
}
