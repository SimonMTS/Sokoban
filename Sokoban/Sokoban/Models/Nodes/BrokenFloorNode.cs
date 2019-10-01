using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models.Nodes
{
    class BrokenFloorNode : FloorNode
    {
        public override char CharRepresentation
        {
            get
            {
                if (ContainsTruck)
                {
                    return '@';
                }
                else if (ContainsCrate)
                {
                    return '#';
                }
                else
                {
                    if (IsBroken)
                    {
                        return ' ';
                    }
                    else
                    {
                        return '~';
                    }
                }
            }
        }

        public int TimesWalked = 0;
        public bool IsBroken { get { return TimesWalked >= 3; } }

        public BrokenFloorNode(int _x, int _y) : base(_x, _y)
        {
        }
    }
}
