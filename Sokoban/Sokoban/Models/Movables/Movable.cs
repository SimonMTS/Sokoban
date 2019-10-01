using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.Controllers.Controller;

namespace Sokoban.Models.Movables
{
    abstract class Movable
    {
        public virtual char CharRepresentation { get { return ' '; } }

        public int x;
        public int y;

        public Maze Map;

        public Node GetNode()
        {
            return Map.nodes[x][y];
        }

        public void Move(GameAction direction)
        {
            switch ((int)direction)
            {
                case 20: x--; break;
                case 21: y++; break;
                case 22: x++; break;
                case 23: y--; break;
            }

            if (GetNode() is BrokenFloorNode)
            {
                ((BrokenFloorNode)GetNode()).TimesWalked++;
            }
        }
    }
}
