using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector { X = v1.X + v2.X, Y = v1.Y + v2.Y };
        }
    }

    class Matrix
    {
        public float X11 { get; set; }
        public float X12 { get; set; }
        public float X21 { get; set; }
        public float X22 { get; set; }

        public static Vector operator *(Matrix m, Vector v)
        {
            return new Vector { X = m.X11 * v.X + m.X12 * v.Y, Y = m.X21 * v.X + m.X22 * v.Y };
        }
    }
}
