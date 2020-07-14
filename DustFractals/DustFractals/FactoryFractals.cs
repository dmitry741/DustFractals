using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    class FactoryFractals
    {
        public static IDustFractal GetFractal(int index)
        {
            IDustFractal fractal;
            if (index == 0)
                fractal = new RotationCompressFractal(0.6f, 0.6f, 0.53f, 0);
            else if (index == 1)
                fractal = new RotationCompressFractal(0, 0.7f, 0.7f, 0);
            else if (index == 2)
                fractal = new RotationCompressFractal(0, Convert.ToSingle(Math.Sqrt(2) / 2), 0.5f, -0.5f);
            else if (index == 3)
                fractal = new RotationCompressFractal(0.5f, 0.5f, 0.5f, 0.5f);
            else if (index == 4)
                fractal = new CompressReflactionFractal(0.5f, Convert.ToSingle(1.0f / (2 * Math.Sqrt(3))), 0.5f, Convert.ToSingle(1.0f / (2 * Math.Sqrt(3))));
            else
                fractal = new CompressReflactionFractal(0.5f, Convert.ToSingle(1.0f / (2 * Math.Sqrt(3))), 0.6666f, 0);

            return fractal;
        }
    }
}
