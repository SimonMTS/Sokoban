using static Sokoban.Controllers.Controller;

namespace Sokoban.Models
{
    abstract class Node
    {
        public virtual char CharRepresentation { get { return ' '; } }

        public virtual bool Walkable { get { return false; } }
        
        public int x;
        public int y;

        public virtual bool ContainsTruck { get; set; } = false;
        public virtual bool ContainsCrate { get; set; } = false;

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
