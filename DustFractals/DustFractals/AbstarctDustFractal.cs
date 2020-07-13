using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    class AbstarctDustFractal : IDustFractal
    {
        protected Matrix _L = null;
        protected Matrix _R = null;
        protected Vector _Vr = null;

        public Vector GetL(Vector v)
        {
            return _L * v;
        }

        public Vector GetR(Vector v)
        {
            return _R * v + _Vr;
        }
    }
}
