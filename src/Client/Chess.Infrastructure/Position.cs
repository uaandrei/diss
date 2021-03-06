﻿using System;
using System.Collections.Generic;

namespace Chess.Infrastructure
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char File { get { return GetFile(); } }
        public int Rank { get { return GetRank(); } }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(int rank, char file)
        {
            X = file - 'a';
            Y = rank - 1;
        }

        public Position(Position p)
            : this(p.X, p.Y)
        {

        }

        public Position(string algebraic) :
            this(Convert.ToInt32(algebraic[1].ToString()), algebraic[0])
        {
        }

        public Position()
        {
            X = 0;
            Y = 0;
        }

        public bool IsInBounds()
        {
            return X >= 0 && X <= 7
                && Y >= 0 && Y <= 7;
        }

        public override bool Equals(object obj)
        {
            var position = obj as Position;
            if (position == null)
                return base.Equals(obj);

            return position.X == X
                && position.Y == Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }

        public static bool operator ==(Position p1, Position p2)
        {
            if (p1 is Position && p2 is Position)
                return p1.Equals(p2);
            if (((object)p1) == null && ((object)p2) == null)
                return true;
            return false;
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !(p1 == p2);
        }

        private int GetRank()
        {
            return Y + 1;
        }

        private char GetFile()
        {
            return (char)(X + 'a');
        }

        public string ToAlgebraic()
        {
            return string.Format("{0}{1}", File, Rank);
        }
    }

    public static class PositionExtensions
    {
        public static void Add(this List<Position> list, int x, int y)
        {
            list.Add(new Position(x, y));
        }
    }
}
