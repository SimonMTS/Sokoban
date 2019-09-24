﻿using Sokoban.Models.Nodes;
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
        public int numberOfCrates;

        public Dictionary<string, int> Truck;
        public Node TruckNode()
        {
            return this.nodes[this.Truck["x"]][this.Truck["y"]];
        }

        public void TruckNode(int x, int y)
        {
            this.nodes[this.Truck["x"]][this.Truck["y"]].ContainsTruck = false;

            this.nodes[x][y].ContainsTruck = true;
            this.Truck["x"] = x;
            this.Truck["y"] = y;
        }

        public MazeState GetState()
        {
            var stateTruck = new Dictionary<string, int>() {
                { "x", this.Truck["x"] },
                { "y", this.Truck["y"] }
            };

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
                            { "TimesWalked", ((BrokenFloorNode)this.nodes[x][y]).TimesWalked}
                        });
                    }
                }
            }

            return new MazeState(stateTruck, stateCrates, stateBrokenFloors);
        }

        public void SetFromState(MazeState state)
        {
            for (int x = 0; x < this.Dimensions["x"]; x++)
            {
                for (int y = 0; y < this.Dimensions["y"]; y++)
                {
                    nodes[x][y].ContainsCrate = false;
                    nodes[x][y].ContainsTruck = false;

                    if (state.Crates.Exists(crate => crate["x"] == x && crate["y"] == y))
                    {
                        nodes[x][y].ContainsCrate = true;
                    }

                    if (x == state.Truck["x"] && y == state.Truck["y"])
                    {
                        nodes[x][y].ContainsTruck = true;
                    }

                    if (state.BrokenFloors.Exists(BrokenFloor => BrokenFloor["x"] == x && BrokenFloor["y"] == y))
                    {
                        ((BrokenFloorNode)nodes[x][y]).TimesWalked = state.BrokenFloors.First(
                            BrokenFloor => BrokenFloor["x"] == x && BrokenFloor["y"] == y
                        )["TimesWalked"];
                    }
                }
            }

            this.Truck["x"] = state.Truck["x"];
            this.Truck["y"] = state.Truck["y"];
        }
    }

    public struct MazeState
    {
        public readonly Dictionary<string, int> Truck;
        public readonly List<Dictionary<string, int>> Crates;
        public readonly List<Dictionary<string, int>> BrokenFloors;

        public MazeState(Dictionary<string, int> _truck, List<Dictionary<string, int>> _crates, List<Dictionary<string, int>> _brokenFloors)
        {
            this.Truck = _truck;
            this.Crates = _crates;
            this.BrokenFloors = _brokenFloors;
        }
    }
}
