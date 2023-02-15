using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadialBarChart.Controls
{
    public class TriangleDrawable : IDrawable
    {
        private readonly Triangle _triangl;

        public TriangleDrawable(Triangle triangl) => _triangl = triangl;
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var stY = dirtyRect.Y;
            var stX = dirtyRect.X;

            PathF path = new PathF();
            path.MoveTo(0, 0);
            path.LineTo(10, 250);
            path.LineTo(20, 20);
          //  path.LineTo(10, 110);

            canvas.StrokeSize = 2;
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(path);
        }
    }
}
