using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models.Nodes
{
    class BrokenFloorNode : FloorNode
    {
        public override bool Walkable { get { return true; } }

        private bool _ContainsTruck = false;
        public override bool ContainsTruck
        {
            get { return _ContainsTruck; }
            set
            {
                if (value)
                {
                    TimesWalked++;
                }
                _ContainsTruck = value;
            }
        }

        private bool _ContainsCrate = false;
        public override bool ContainsCrate
        {
            get { return _ContainsCrate; }
            set
            {
                if (IsBroken)
                {
                    _ContainsCrate = false;
                    return;
                }

                if (value)
                {
                    TimesWalked++;
                }
                _ContainsCrate = value;
            }
        }

        public int TimesWalked = 0;
        public bool IsBroken { get { return TimesWalked >= 3; } }

        public BrokenFloorNode(int _x, int _y) : base(_x, _y)
        {
        }
    }
}
