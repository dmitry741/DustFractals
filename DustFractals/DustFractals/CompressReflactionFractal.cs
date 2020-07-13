using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    class CompressReflactionFractal : AbstarctDustFractal
    {
        float _a, _b;

        public CompressReflactionFractal(float a, float b)
        {
            _a = a;
            _b = b;

            _L = new Matrix { X11 = _a, X12 = _b, X21 = _b, X22 = -_a };
            _R = new Matrix { X11 = _a, X12 = -_b, X21 = -_b, X22 = -_a };
            _Vr = new Vector { X = _a, Y = _b };
        }
    }
}
