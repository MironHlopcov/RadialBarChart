
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Font = Microsoft.Maui.Graphics.Font;



namespace RadialBarChart.Controls
{
    public class ClassicGraphicDrawable : IDrawable, ICommand
    {
        ICanvas _canvas;

        Color lineColor = Colors.Green;
        Color coordinateLineColor = Colors.White;
        Color textColor = Colors.White;
        //смещение по вертикали и диагонали от начала координат (необходимо для рисования коардинатных прямых и подписей)
        static int ancorX = 20;
        static int ancorY = 40;

        List<Point> points = new List<Point>(); //точки для отображения преобразованные к виду с началом координат XY в левом верхнем углу dirtyRect

        Point clickPoint = new Point(ancorX, 0);
        string clickPointLabel;

        private readonly ClassicGraphic _graphic;
        public ClassicGraphicDrawable(ClassicGraphic graphic)
        {
            _graphic = graphic;
            _graphic.StartInteraction += _graphic_StartInteraction;
            _graphic.EndInteraction += _graphic_EndInteraction;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {

        }

        private void _graphic_StartInteraction(object sender, TouchEventArgs e)
        {
            if (e.Touches.Last().X > points.Max(x=>x.X))
                return;
            var endP = _graphic.Points[points.IndexOf(points.FirstOrDefault(p => p.X > e.Touches.Last().X))];
            var stP = _graphic.Points.IndexOf(endP) - 1 < 0 ? new Point(0, 0) : _graphic.Points[_graphic.Points.IndexOf(endP) - 1];

            var H = endP.Y >= stP.Y ? endP.Y - stP.Y : stP.Y - endP.Y;
            var A = endP.X - stP.X;
            var tanA = H / A;

            var a = endP.Y >= stP.Y ? (e.Touches.Last().X - ancorX) / _graphic.scaleX - stP.X : endP.X - (e.Touches.Last().X - ancorX) / _graphic.scaleX;
            var h = endP.Y >= stP.Y ? tanA * a + stP.Y : endP.Y + tanA * a;

            clickPoint.Y = h;
            clickPoint.X = e.Touches.Last().X;
            clickPointLabel = $"{(int)h}";

            _graphic.DrawClickPoint();
        }
        private void _graphic_EndInteraction(object sender, TouchEventArgs e)
        {
             clickPoint = new Point(ancorX, 0);
            _graphic.DrawClickPoint();
        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            ancorX = (int)_graphic.Ancor.X;
            ancorY = (int)_graphic.Ancor.Y;

            lineColor = _graphic.GraphicColor;
            coordinateLineColor = _graphic.СoordinateLineColor;
            textColor = _graphic.TextColor;

            rect.Size = new Size((float)(_graphic.Points.Max(x=>x.X) * _graphic.scaleX + ancorX),
                                 (float)(rect.Height));
            _canvas = canvas;
            RectF dirtyRect = new RectF(ancorX, ancorY, rect.Width - ancorX, rect.Height - ancorY); //рабочая область для рисования графиков с учетом смещения
            double scaleY = dirtyRect.Height / _graphic.Points.Max(x => x.Y); //масштаб на оси Y для размещения всех значений в пределах высоты элемента

            Point starPoint = new Point(); //левый верхний угол поля для рисования графиков
            starPoint.X = dirtyRect.X;
            starPoint.Y = dirtyRect.Size.Height;

            points = new List<Point>(); //точки для отображения преобразованные к виду с началом координат XY в левом верхнем углу dirtyRect
            var sortedPoints = _graphic.Points.OrderBy(x => x.X).ToList();
            sortedPoints.Add(new Point(_graphic.Points.Last().X, 0) ); //добавляем последнюю точку в с темже значением X и значением Y=0 для корректрой отрисовки заливки графика
            foreach (var p in sortedPoints)
            {
                var pointR = new Point(p.X * _graphic.scaleX + ancorX, dirtyRect.Size.Height - (p.Y * _graphic.scaleY * scaleY));
                points.Add(pointR);
                if (p.X != 0)
                    DrawLabel(canvas, p.X.ToString(), (float)p.X * _graphic.scaleX + ancorX, dirtyRect.Size.Height, 100, 100); //рисует подписи абсцисс
                if(p.Y!=0)  
                    DrawLabel(canvas, p.Y.ToString(), 0, (float)(dirtyRect.Size.Height - (p.Y * _graphic.scaleY * scaleY)), 100, 100); //рисует подписи ординат
            }
          
            #region рисование графика
            PathF path = new PathF();
            path.MoveTo(starPoint);
            foreach (var point in points)
            {
                path.LineTo(point);
            }


            //lineColor = Colors.Green;
            Color fillColor = lineColor.WithAlpha(0.3F);

            canvas.StrokeSize = 1;
            canvas.StrokeColor = lineColor;
            canvas.FillColor = fillColor;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.FillPath(path);
            canvas.DrawPath(path);
            #endregion
            #region рисование коардинатных прямых
            PathF pathH = new PathF();
            pathH.MoveTo(dirtyRect.X, rect.Y);
            pathH.LineTo(dirtyRect.X, dirtyRect.Size.Height);
            pathH.LineTo(rect.Size.Width, dirtyRect.Size.Height);

            Color planeColor = coordinateLineColor;
            Color planeAlpha = planeColor.WithAlpha(0.3F);

            canvas.StrokeSize = 2;
            canvas.StrokeColor = planeColor;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.DrawPath(pathH);
            #endregion
            #region рисование выбранной точки и подписи
           
            var newClickPoint = new Point(clickPoint.X, dirtyRect.Size.Height - (clickPoint.Y * _graphic.scaleY * scaleY));
            DrawPoint(canvas, newClickPoint);
            if (clickPoint.X != ancorX)
                DrawLabel(canvas, clickPointLabel, (float)newClickPoint.X, (float)newClickPoint.Y, 100, 100);
            #endregion

            _graphic.MinimumWidthRequest = sortedPoints.Select(x => x.X).Max() * _graphic.scaleX + ancorX*2;
        }
        private void DrawLabel(ICanvas canvas, string text, float x, float y, float width, float heigth)
        {
            if (y > 0)
            {
                canvas.StrokeLineJoin = LineJoin.Round;
                canvas.StrokeLineCap = LineCap.Round;
                canvas.FontColor = textColor;
                canvas.FontSize = 10;
                canvas.Font = Font.Default;
                canvas.DrawString(text, x, y, width, heigth, HorizontalAlignment.Left, VerticalAlignment.Top);
            }
        }
        private void DrawPoint(ICanvas canvas, Point point)
        {
            canvas.StrokeColor = lineColor;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse((float)point.X, (float)point.Y, 1, 1);
        }
    }
}
