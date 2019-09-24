using Sokoban.Models;
using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;

namespace Sokoban.Views
{
    class OutputView
    {
        //new Dictionary<string, int>() {
        //    { "x", x },
        //    { "y", y }
        //}

        private static readonly Dictionary<string, string> displayElements = new Dictionary<string, string>()
        {
            { "wall", "█" },
            { "floor", "." },
            { "truck", "@" },
            { "crate", "#" },
            { "destination", "x" },
            { "crateOnDestination", "0" },
            { "brokenFloor", "~" },
            { "brokenThroughFloor", " " }
        };

        public static void DrawLevel(Maze map, bool won)
        {
            Console.Clear();
            Console.Write("┌──────────┐\n| Sokoban  |\n└──────────┘\n─────────────────────────────────────────────────────────────────────────\n");

            for (int x = 0; x < map.Dimensions["x"]; x++)
            {
                for (int y = 0; y < map.Dimensions["y"]; y++)
                {
                    if (map.nodes[x][y] is WallNode)
                    {
                        Console.Write(displayElements["wall"]);
                    }
                    else if (map.nodes[x][y] is FloorNode)
                    {
                        if (map.nodes[x][y].ContainsTruck)
                        {
                            Console.Write(displayElements["truck"]);
                        }
                        else if (map.nodes[x][y] is DestinationNode && map.nodes[x][y].ContainsCrate)
                        {
                            Console.Write(displayElements["crateOnDestination"]);
                        }
                        else if (map.nodes[x][y].ContainsCrate)
                        {
                            Console.Write(displayElements["crate"]);
                        }
                        else if (map.nodes[x][y] is BrokenFloorNode)
                        {
                            if (((BrokenFloorNode)map.nodes[x][y]).IsBroken)
                            {
                                Console.Write(displayElements["brokenThroughFloor"]);
                            }
                            else
                            {
                                Console.Write(displayElements["brokenFloor"]);
                            }
                        }
                        else if (map.nodes[x][y] is DestinationNode)
                        {
                            Console.Write(displayElements["destination"]);
                        }
                        else
                        {
                            Console.Write(displayElements["floor"]);
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }

            Console.Write("─────────────────────────────────────────────────────────────────────────\n");

            if (!won)
            {
                Console.Write("> gebruik pijljestoetsen (s = stop, r = reset, u = undo)\n");
            } else
            {
                Console.WriteLine("\n=== HOERA OPGELOST ===\n> press key to continue");
            }
        }

        public static void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine("┌────────────────────────────────────────────────────┐\n| Welkom bij Sokoban                                 |\n|                                                    |\n| betekenis van de symbolen   |   doel van het spel  |\n|                             |                      |\n| spatie : outerspace         |   duw met de truck   |\n|      █ : muur               |   de krat(ten)       |\n|      · : vloer              |   naar de bestemming |\n|      O : krat               |                      |\n|      0 : krat op bestemming |                      |\n|      x : bestemming         |                      |\n|      @ : truck              |                      |\n└────────────────────────────────────────────────────┘\n> Kies een doolhof (1 - 6), s = stop");
        }
    }
}
