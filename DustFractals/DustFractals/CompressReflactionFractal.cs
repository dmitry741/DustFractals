using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    class CompressReflactionFractal : AbstarctDustFractal
    {
        public CompressReflactionFractal(float a, float b, float c, float d)
        {
            _L = new Matrix { X11 = a, X12 = b, X21 = b, X22 = -a };
            _R = new Matrix { X11 = c, X12 = -d, X21 = -d, X22 = -c };            
            _Vr = new Vector { X = c, Y = d };
        }
    }
}
