﻿using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.Controllers.Controller;

namespace Sokoban.Models.Movables
{
    class Crate : Movable
    {
        public override char CharRepresentation {
            get {
                if (GetNode() is DestinationNode)
                {
                    return '0';
                }
                else
                {
                    return 'o';
                }
            }
        }
    }
}