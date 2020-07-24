using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustFractals
{
    /// <summary>
    /// Абстрактный класс для пылевидного фрактала.
    /// </summary>
    class AbstarctDustFractal : IDustFractal
    {
        /// <summary>
        /// Матрица левого преобразования.
        /// </summary>
        protected Matrix _L = null;

        /// <summary>
        /// Матрица правого преобразования.
        /// </summary>
        protected Matrix _R = null;

        /// <summary>
        /// Вектор параллельного сдвига.
        /// </summary>
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
