using Sokoban.Models.Movables;
using System;
using static Sokoban.Controllers.Controller;

namespace Sokoban.Models.Nodes
{
    abstract class Node
    {
        public virtual char CharRepresentation { get { return ' '; } }

        protected (bool Truth, char Value) CharContainsMovable()
        {
            if (ContainsTruck)
            {
                return (true, Contains().Truck.CharRepresentation);
            }
            else if (ContainsCrate)
            {
                return (true, Contains().Crate.CharRepresentation);
            }
            else
            {
                return (false, ' ');
            }
        }

        public virtual bool Walkable { get { return false; } }
        
        public int x;
        public int y;

        public Maze Map;

        public bool ContainsTruck {
            get {
                return Contains().Truck != null;
            }
        }

        public bool ContainsCrate {
            get {
                return Contains().Crate != null;
            }
        }

        public (Truck Truck, Crate Crate) Contains()
        {
            Truck t = Array.Find(Map.Trucks,
                truck => (truck.x == x && truck.y == y)
            );

            Crate c = Array.Find(Map.Crates,
                crate => (crate.x == x && crate.y == y)
            );

            return (Truck: t, Crate: c);
        }

        private Node[] neighbours = new Node[4];

        public Node getNeighbour(GameAction direction)
        {
            return neighbours[(int)direction - 20];
        }

        public void setNeighbours(Node[] _neighbours)
        {
            neighbours = _neighbours;
        }

        public Node(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
