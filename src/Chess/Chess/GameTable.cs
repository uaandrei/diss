﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class GameTable
    {
        private int[,] _matrix;

        public GameTable()
        {
            // TODO: add pieces here
            _matrix = new int[8, 8];
        }
    }
}
