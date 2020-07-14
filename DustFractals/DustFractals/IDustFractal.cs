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
        /// <summary>
        /// Получение левого преобразрвания вектора (точки).
        /// </summary>
        /// <param name="v">Вектор для преобразования.</param>
        /// <returns>Новый вектор.</returns>
        Vector GetL(Vector v);

        /// <summary>
        /// Получение правого преобразования точки.
        /// </summary>
        /// <param name="v">Вектор для преобразования.</param>
        /// <returns>Новый вектор.</returns>
        Vector GetR(Vector v);
    }
}
