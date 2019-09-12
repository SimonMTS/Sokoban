using static Sokoban.Controllers.Controller;

namespace Sokoban.Models
{
    class Node
    {
        public enum NodeType { Wall, Floor, Destination };
        public NodeType Type;

        public bool ContainsTruck = false;
        public bool ContainsCrate = false;

        public int x;
        public int y;

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
