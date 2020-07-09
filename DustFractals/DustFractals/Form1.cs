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
        float _a = 0;
        float _b = 0.7f;
        float _c = 0.7f;
        float _d = 0;

        #endregion

        #region private

        void RenderFractal(Graphics g)
        {
             // TODO:
        }

        void Render()
        {
            if (_bitmap == null)
                return;

            Graphics g = Graphics.FromImage(_bitmap);
            g.Clear(Color.White);

            // отрисовка фрактала
            RenderFractal(g);

            pictureBox1.Image = _bitmap;
        }

        Bitmap CreateBackground(int width, int height)
        {
            return (width > 0 || height > 0) ? new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb) : null;
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            _bitmap = CreateBackground(pictureBox1.Width, pictureBox1.Height);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            _bitmap = CreateBackground(pictureBox1.Width, pictureBox1.Height);
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

