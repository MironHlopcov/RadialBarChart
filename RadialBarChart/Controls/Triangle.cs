using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadialBarChart.Controls
{
    public class Triangle : GraphicsView
    {

        public Triangle()
        {
            Drawable = new TriangleDrawable(this);
           
        }
    }
}
