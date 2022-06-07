using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace OOP1
{
    public class Figures
    {
        public Color color = Color.Black;
        public int points_count;
        public virtual int PointsCount { get; }

        internal Point[] points;


        public void SetPoints(Point[] points)
        {
            this.points = points;
        }
    }
}
