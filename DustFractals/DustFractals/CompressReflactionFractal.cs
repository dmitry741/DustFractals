using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    class CompressReflactionFractal : AbstarctDustFractal
    {
        public CompressReflactionFractal(float a, float b)
        {
            _L = new Matrix { X11 = a, X12 = b, X21 = b, X22 = -a };
            _R = new Matrix { X11 = a, X12 = -b, X21 = -b, X22 = -a };
            _Vr = new Vector { X = a, Y = b };
        }
    }
}
