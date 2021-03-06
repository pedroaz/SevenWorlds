﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsEqual(int x, int y)
        {
            return (x == X && y == Y);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }
}
