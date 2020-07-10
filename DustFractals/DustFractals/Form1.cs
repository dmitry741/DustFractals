using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DustFractals
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region memebers

        Bitmap _bitmap = null;
        byte[] _rgbValues = null;
        IEnumerable<PointF> _points = null;

        float _a = 0;
        float _b = 0.7f;
        float _c = 0.7f;
        float _d = 0;

        #endregion

        #region private

        IEnumerable<PointF> CreateFractal()
        {
            const int cStep = 13;
            Matrix L = new Matrix { X11 = _a, X12 = -_b, X21 = _b, X22 = _a };
            Matrix R = new Matrix { X11 = _c, X12 = -_d, X21 = _d, X22 = _c };
            Vector vr = new Vector { X = 1 - _c, Y = -_d };

            List<Vector> vectors = new List<Vector>
            {
                new Vector { X = 1, Y = 0 }
            };

            for (int i = 0; i < cStep; i++)
            {
                List<Vector> temps = new List<Vector>();

                foreach (Vector v in vectors)
                {
                    temps.Add(L * v);
                    temps.Add(R * v + vr);
                }

                vectors = temps;
            }

            return vectors.Select(v => new PointF(v.X, v.Y));
        }

        void Render()
        {
            if (_bitmap == null)
                return;

            Rectangle rect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
            System.Drawing.Imaging.BitmapData bitmapData = _bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, _bitmap.PixelFormat);
            IntPtr ptr = bitmapData.Scan0;

            int bytes = Math.Abs(bitmapData.Stride) * _bitmap.Height;
            Array.Clear(_rgbValues, 0, bytes);

            float xmin = -1.2f;
            float xmax = 1.2f;
            float ymin = -1.2f;
            float ymax = 1.2f;

            float kx = pictureBox1.Width / (xmax - xmin);
            float ky = pictureBox1.Height / (ymin - ymax);

            foreach (PointF point in _points)
            {
                int X = Convert.ToInt32(kx * (point.X - xmin));
                int Y = Convert.ToInt32(ky * (point.Y - ymax));
                int index = Y * Math.Abs(bitmapData.Stride) + X * 3;

                if (index < 0 || index >= bytes)
                    continue;

                _rgbValues[index + 1] = 255; // зеленый цвет
            }

            System.Runtime.InteropServices.Marshal.Copy(_rgbValues, 0, ptr, bytes);
            _bitmap.UnlockBits(bitmapData);

            pictureBox1.Image = _bitmap;
        }

        Bitmap CreateBackground(int width, int height)
        {
            return (width > 0 || height > 0) ? new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb) : null;
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            _bitmap = CreateBackground(pictureBox1.Width, pictureBox1.Height);

            if (_bitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
                System.Drawing.Imaging.BitmapData bitmapData = _bitmap.LockBits(rect, 
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, 
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                int bytes = Math.Abs(bitmapData.Stride) * _bitmap.Height;
                _rgbValues = new byte[bytes];
                _bitmap.UnlockBits(bitmapData);

                _points = CreateFractal();
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            _bitmap = CreateBackground(pictureBox1.Width, pictureBox1.Height);

            if (_bitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
                System.Drawing.Imaging.BitmapData bitmapData = _bitmap.LockBits(rect,
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                int bytes = Math.Abs(bitmapData.Stride) * _bitmap.Height;
                _rgbValues = new byte[bytes];
                _bitmap.UnlockBits(bitmapData);

                _points = CreateFractal();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

