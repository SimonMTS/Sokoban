using Sokoban.Models.Movables;
using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sokoban.Models
{
    class Maze
    {
        public int mapNumber;

        public Node[][] nodes;
        public Dictionary<string, int> Dimensions;

        public int[][] destinations;

        public Truck[] Trucks;
        public Crate[] Crates;

        public MazeState GetState()
        {
            var stateTrucks = new List<Dictionary<string, int>>();
            for (int i = 0; i < this.Trucks.Length; i++)
            {
                stateTrucks.Add(new Dictionary<string, int>() {
                    { "x", this.Trucks[i].x },
                    { "y", this.Trucks[i].y }
                });
            }

            var stateCrates = new List<Dictionary<string, int>>();
            for (int x = 0; x < this.Dimensions["x"]; x++)
            {
                for (int y = 0; y < this.Dimensions["y"]; y++)
                {
                    if (this.nodes[x][y].ContainsCrate)
                    {
                        stateCrates.Add(new Dictionary<string, int>() {
                            { "x", x },
                            { "y", y }
                        });
                    }
                }
            }

            var stateBrokenFloors = new List<Dictionary<string, int>>();
            for (int x = 0; x < this.Dimensions["x"]; x++)
            {
                for (int y = 0; y < this.Dimensions["y"]; y++)
                {
                    if (this.nodes[x][y] is BrokenFloorNode)
                    {
                        stateBrokenFloors.Add(new Dictionary<string, int>() {
                            { "x", x },
                            { "y", y },
                            { "TimesWalked", ((BrokenFloorNode)this.nodes[x][y]).TimesWalked }
                        });
                    }
                }
            }

            return new MazeState(stateTrucks.ToArray(), stateCrates.ToArray(), stateBrokenFloors);
        }

        public void SetFromState(MazeState state)
        {
            for (int x = 0; x < this.Dimensions["x"]; x++)
            {
                for (int y = 0; y < this.Dimensions["y"]; y++)
                {
                    if (state.BrokenFloors.Exists(BrokenFloor => BrokenFloor["x"] == x && BrokenFloor["y"] == y))
                    {
                        ((BrokenFloorNode)nodes[x][y]).TimesWalked = state.BrokenFloors.First(
                            BrokenFloor => BrokenFloor["x"] == x && BrokenFloor["y"] == y
                        )["TimesWalked"];
                    }
                }
            }

            for (int i = 0; i < state.Trucks.Length; i++)
            {
                this.Trucks[i].x = state.Trucks[i]["x"];
                this.Trucks[i].y = state.Trucks[i]["y"];
            }

            for (int i = 0; i < state.Crates.Length; i++)
            {
                this.Crates[i].x = state.Crates[i]["x"];
                this.Crates[i].y = state.Crates[i]["y"];
            }
        }
    }

    public struct MazeState
    {
        public readonly Dictionary<string, int>[] Trucks;
        public readonly Dictionary<string, int>[] Crates;
        public readonly List<Dictionary<string, int>> BrokenFloors;

        public MazeState(Dictionary<string, int>[] _trucks, Dictionary<string, int>[] _crates, List<Dictionary<string, int>> _brokenFloors)
        {
            this.Trucks = _trucks;
            this.Crates = _crates;
            this.BrokenFloors = _brokenFloors;
        }
    }
}
