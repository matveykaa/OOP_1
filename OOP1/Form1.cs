using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP1;

namespace OOP1
{
    public partial class Form1 : Form
    {
       

        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;
         
        }

        public List<Point> points_list = new List<Point>();
        public int counter = 0;
        public delegate void Draw(Figures figure);
        public Draw draw;

        Bitmap bm;
        public Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black,1);
        Pen erase = new Pen(Color.White, 10);
        int index;
        int x, y, sX, sY, cX, cY;

        ColorDialog cd = new ColorDialog();
        Color new_color;

        Figures myfig;

        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            index = 0;
            textBox1.Enabled = false;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pic_color.BackColor = cd.Color;
            p.Color = cd.Color;
            textBox1.Enabled = false;
        }

        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            points_list.Clear();
            counter = 0;
            myfig = new Ellipse();
            index = 4;
            textBox1.Enabled = false;

            draw = myfig =>
            {
                if (myfig.points[0].X <= myfig.points[1].X)
                {
                    if (myfig.points[0].Y <= myfig.points[1].Y)
                    {
                        g.DrawEllipse(p, myfig.points[0].X, myfig.points[0].Y,
                           Math.Abs(myfig.points[1].X - myfig.points[0].X), Math.Abs(myfig.points[1].Y - myfig.points[0].Y));
                    }
                    else
                    {
                        g.DrawEllipse(p, myfig.points[0].X, myfig.points[1].Y,
                          Math.Abs(myfig.points[1].X - myfig.points[0].X), Math.Abs(myfig.points[0].Y - myfig.points[1].Y));
                    }
                }
                else
                {
                    if (myfig.points[1].Y <= myfig.points[0].Y) {
                        g.DrawEllipse(p, myfig.points[1].X, myfig.points[1].Y,
                       Math.Abs(myfig.points[0].X - myfig.points[1].X), Math.Abs(myfig.points[0].Y - myfig.points[1].Y));
                    }
                    else
                    {
                        g.DrawEllipse(p, myfig.points[1].X, myfig.points[0].Y,
                       Math.Abs(myfig.points[0].X - myfig.points[1].X), Math.Abs(myfig.points[1].Y - myfig.points[0].Y));
                    }
                }
            };
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;
            cX = e.X;
            cY = e.Y;
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (index == 4)
            {
                g.DrawLine(p, e.X,e.Y, e.X+1, e.Y+1);
                points_list.Add(new Point(e.X, e.Y));
                counter++;
                if (counter >= myfig.PointsCount)
                {
                    Point[] points = new Point[points_list.Count];

                    for (int i = 0; i < points_list.Count; i++)
                    {
                        points[i] = points_list[i];
                    }

                    myfig.SetPoints(points);
                    counter = 0;
                    draw(myfig);
                    points_list.Clear();
                    pic.Image = bm;
                }
            }

        
        }


        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;
                }
                if (index == 3)
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py);
                    py = px;
                }
            }
            pic.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }


        private void btn_line_Click(object sender, EventArgs e)
        {
            points_list.Clear();
            counter = 0;
            myfig = new Line();
            index = 4;
            textBox1.Enabled = false;

            draw = myfig =>
            {
                g.DrawLine(p, myfig.points[0].X, myfig.points[0].Y,
                     myfig.points[1].X, myfig.points[1].Y);

            };
        }

        private void btn_circle_Click(object sender, EventArgs e)
        {
            points_list.Clear();
            counter = 0;
            myfig = new Circle();
            index = 4;
            textBox1.Enabled = false;

            draw = myfig =>
            {
                int r2;
                r2 = (int)Math.Round(Math.Sqrt(Math.Pow(Math.Abs(myfig.points[1].Y - myfig.points[0].Y), 2) + Math.Pow(Math.Abs(myfig.points[1].X - myfig.points[0].X), 2)));
               
                g.DrawEllipse(p, myfig.points[0].X - r2, myfig.points[0].Y - r2, 2*r2, 2*r2);
            };
        }

        private void btn_polygon_Click(object sender, EventArgs e)
        {
            points_list.Clear();
            counter = 0;
            myfig = new Polygon();
            index = 4;
            myfig.points_count = Convert.ToInt32(textBox1.Text);
            textBox1.Enabled = true;

            draw = myfig =>
            {
                g.DrawPolygon(p, myfig.points);
            };
        }

        private void btn_rectangle_Click(object sender, EventArgs e)
        {
            points_list.Clear();
            counter = 0;
            myfig = new Rectangle();
            index = 4;
            textBox1.Enabled = false;

            draw = myfig =>
            {

                if (myfig.points[0].X <= myfig.points[1].X)
                {
                    if (myfig.points[0].Y <= myfig.points[1].Y)
                    {
                        g.DrawRectangle(p, myfig.points[0].X, myfig.points[0].Y,
                   Math.Abs(myfig.points[1].X - myfig.points[0].X), Math.Abs(myfig.points[1].Y - myfig.points[0].Y));
                    }
                    else
                    {
                        g.DrawRectangle(p, myfig.points[0].X, myfig.points[1].Y,
                  Math.Abs(myfig.points[1].X - myfig.points[0].X), Math.Abs(myfig.points[0].Y - myfig.points[1].Y));
                    }
                }
                else
                {
                    if (myfig.points[1].Y <= myfig.points[0].Y)
                    {
                        g.DrawRectangle(p, myfig.points[1].X, myfig.points[1].Y,
                   Math.Abs(myfig.points[0].X - myfig.points[1].X), Math.Abs(myfig.points[0].Y - myfig.points[1].Y));
                    }
                    else
                    {
                        g.DrawRectangle(p, myfig.points[1].X, myfig.points[0].Y,
                                          Math.Abs(myfig.points[0].X - myfig.points[1].X), Math.Abs(myfig.points[1].Y - myfig.points[0].Y));
                    }
                }
            };
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_square_Click(object sender, EventArgs e)
        {
            points_list.Clear();
            counter = 0;
            myfig = new Square();
            index = 4;
            textBox1.Enabled = false;

            draw = myfig =>
            {
                g.DrawRectangle(p, myfig.points[0].X, myfig.points[0].Y,
                   Math.Abs(myfig.points[1].X - myfig.points[0].X), Math.Abs(myfig.points[1].X - myfig.points[0].X));

            };
        }

        private void btn_triangle_Click(object sender, EventArgs e)
        {
            points_list.Clear();
            counter = 0;
            myfig = new Triangle();
            index = 4;
            textBox1.Enabled = false;

            draw = myfig =>
            {
                g.DrawPolygon(p, myfig.points);

            };
        }

      
        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 3;
            textBox1.Enabled = false;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            sX = x - cX;
            sY = y - cY;
        }

        static Point set_point(PictureBox pb,Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }
    }
}
