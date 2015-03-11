using ParticleFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleFilterDemo
{
    public partial class Form1 : Form
    {
        private IParticleFilter<FeatureParticle> _filter;
        private Point _previousPosition = new Point(0,0);

        public Form1()
        {
            InitializeComponent();

            _filter = new FeatureParticleFilter();
            _filter.GenerateParticles(100,  //particles' count
                                           FeatureParticle.FromArray, //convert arr => position (create from array)
                                           new List<IDistribution>()  //position range
                                           { 
                                               new UniformDistribution(pictureBox1.Height),
                                               new UniformDistribution(pictureBox1.Height)
                                           });
        }

        private void _drawParticles(Graphics g)
        {

            for(int i = 0; i < _filter.Particles.Count; i++)
            {
                var position = _filter.Particles[i].Position;
                var X = _previousPosition.X + (int)position.X;
                var Y = _previousPosition.Y + (int)position.Y;
                g.DrawLine(Pens.Blue, new Point(X - 1, Y - 1), new Point(X + 1, Y + 1));
                g.DrawLine(Pens.Blue, new Point(X - 1, Y + 1), new Point(X + 1, Y - 1));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_previousPosition.X == 0 && _previousPosition.Y == 0)
                _previousPosition = e.Location;

            if (pictureBox1.Image == null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                }
                pictureBox1.Image = bmp;
            }
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);

                g.DrawLine(Pens.Red, new Point(e.Location.X - 3, e.Location.Y - 3),
                    new Point(e.Location.X + 3, e.Location.Y + 3));
                g.DrawLine(Pens.Red, new Point(e.Location.X - 3, e.Location.Y + 3),
                    new Point(e.Location.X + 3, e.Location.Y - 3));

                _filter.Predict(0.9f);
                _filter.Update(new FeatureParticle() { Position = new PointF((e.X - _previousPosition.X)*10, (e.Y - _previousPosition.Y)*10) });
                _drawParticles(g);
            }
            pictureBox1.Invalidate();
            _previousPosition = e.Location;
        }
    }
}
