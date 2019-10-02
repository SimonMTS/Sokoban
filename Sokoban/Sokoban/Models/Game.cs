using System;
using System.Collections.Generic;
using Sokoban.Controllers;
using Sokoban.Models.Movables;
using Sokoban.Models.Nodes;
using static Sokoban.Controllers.Controller;
namespace Sokoban.Models
{
    class Game
    {
        public Maze Map;
        public List<MazeState> MapHistory = new List<MazeState>();

        public Game(int level)
        {
            Map = Parser.Parse(level);

            MapHistory.Add(Map.GetState());
        }

        public void ApplyAction(GameAction action)
        {
            if (action == GameAction.Reset)
            {
                Map.SetFromState(MapHistory[0]);
                if (MapHistory.Count > 1)
                {
                    MapHistory.RemoveRange(1, MapHistory.Count - 1);
                }
            }
            else if (action == GameAction.Undo)
            {
                if (MapHistory.Count > 1)
                {
                    MapHistory.RemoveAt(MapHistory.Count - 1);

                    Map.SetFromState(MapHistory[MapHistory.Count - 1]);
                }
            }
            else
            {
                Map.Trucks[0].DoAction(action);

                for (int i = 1; i < Map.Trucks.Length; i++)
                {
                    ((AITruck)Map.Trucks[i]).DoAction();
                }

                MapHistory.Add(Map.GetState());
            }
        }

        public bool HasWon()
        {
            int cratesOnDestination = 0;

            foreach (int[] dest in Map.destinations)
            {
                if (Map.nodes[dest[0]][dest[1]].ContainsCrate)
                {
                    cratesOnDestination++;
                }
            }

            return Map.Crates.Length == cratesOnDestination;
        }
    }
}
