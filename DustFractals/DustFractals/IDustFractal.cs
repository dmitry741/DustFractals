using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    /// <summary>
    /// Декларация методов для получения правого и левого преобразований.
    /// </summary>
    interface IDustFractal
    {
        Vector GetL(Vector v);

        Vector GetR(Vector v);
    }
}
