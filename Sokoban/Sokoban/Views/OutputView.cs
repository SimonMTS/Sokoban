using Sokoban.Models;
using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;

namespace Sokoban.Views
{
    class OutputView
    {
        public static void DrawLevel(Maze map, bool won)
        {
            Console.Clear();
            Console.Write("┌──────────┐\n| Sokoban  |\n└──────────┘\n─────────────────────────────────────────────────────────────────────────\n");

            for (int x = 0; x < map.Dimensions["x"]; x++)
            {
                for (int y = 0; y < map.Dimensions["y"]; y++)
                {
                    Console.Write(map.nodes[x][y].CharRepresentation);
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
