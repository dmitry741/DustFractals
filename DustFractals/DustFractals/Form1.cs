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
        IDustFractal _fractal = null;

        #endregion

        #region private

        IEnumerable<PointF> CreateFractal(IDustFractal fractal)
        {
            List<Vector> vectors = new List<Vector>
            {
                new Vector { X = 1, Y = 0 }
            };

            for (int i = 0; i < 15; i++)
            {
                List<Vector> temps = new List<Vector>();

                temps.AddRange(vectors.Select(x => fractal.GetL(x)));
                temps.AddRange(vectors.Select(x => fractal.GetR(x)));

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
            int bytes = Math.Abs(bitmapData.Stride) * _bitmap.Height;
            Array.Clear(_rgbValues, 0, bytes);

            float xmin = _points.Min(z => z.X);
            float xmax = _points.Max(z => z.X);
            float ymin = _points.Min(z => z.Y);
            float ymax = _points.Max(z => z.Y);

            RectangleF winRect = RectangleF.Empty;
            float k = (xmax - xmin) / (ymax - ymin);

            if (k > 1)
            {
                winRect.X = 0;
                winRect.Width = pictureBox1.Width;
                winRect.Y = pictureBox1.Height / 2 - pictureBox1.Height / (2 * k);
                winRect.Height = pictureBox1.Height / k;
            }
            else
            {
                winRect.X = pictureBox1.Width / 2 - pictureBox1.Width / (2 * k);
                winRect.Width = pictureBox1.Width / k;
                winRect.Y = 0;
                winRect.Height = pictureBox1.Height;
            }

            float kx = winRect.Width / (xmax - xmin);
            float ky = winRect.Height / (ymin - ymax);

            foreach (PointF point in _points)
            {
                int X = Convert.ToInt32(kx * (point.X - xmin) + winRect.X);
                int Y = Convert.ToInt32(ky * (point.Y - ymax) + winRect.Y);
                int index = Y * Math.Abs(bitmapData.Stride) + X * 3;

                if (index < 0 || index >= bytes)
                    continue;

                _rgbValues[index + 1] = 255; // зеленый цвет
            }

            System.Runtime.InteropServices.Marshal.Copy(_rgbValues, 0, bitmapData.Scan0, bytes);
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

                _fractal = FactoryFractals.GetFractal(0);
                _points = CreateFractal(_fractal);

                comboBox1.BeginUpdate();
                comboBox1.Items.Add("Лист");
                comboBox1.Items.Add("Ель");
                comboBox1.Items.Add("Кружево");
                comboBox1.Items.Add("Дракон");
                comboBox1.Items.Add("Кривая Коха");
                comboBox1.Items.Add("Дендрид");
                comboBox1.SelectedIndex = 0;
                comboBox1.EndUpdate();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fractal = FactoryFractals.GetFractal(comboBox1.SelectedIndex);
            _points = CreateFractal(_fractal);
            Render();
        }
    }
}

