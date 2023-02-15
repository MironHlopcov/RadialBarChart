using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadialBarChart.Controls
{
    public class ClassicGraphic : GraphicsView
    {
        public int scaleX { get; private set; } = 3; //множители для регулировки масштаба
        public int scaleY { get; private set; } = 1; //1 для автоматической подстройки под высоту элемента

        #region Ancor
        public static readonly BindableProperty AncorProperty =
            BindableProperty.Create(nameof(Ancor), typeof(Point), typeof(ClassicGraphic), new Point(0,0), propertyChanged: AncorPropertyChanged);
        private static void AncorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var p = (Point)newValue;
            (bindable as ClassicGraphic).Ancor = p;
            (bindable as ClassicGraphic).Invalidate();
        }
        public Point Ancor
        {
            get => (Point)GetValue(AncorProperty);
            set => SetValue(AncorProperty, value);
        }
        #endregion

        #region Points
        public static readonly BindableProperty PointsProperty =
           BindableProperty.Create(nameof(Points), typeof(List<Point>), typeof(ClassicGraphic), new List<Point> { new Point() }, propertyChanged: PointsPropertyChanged);
        private static void PointsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ClassicGraphic).Invalidate();
        }
        public List<Point> Points
        {
            get => (List<Point>)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }
        #endregion

        #region GraphicColor
        public static readonly BindableProperty GraphicColorProperty =
           BindableProperty.Create(nameof(GraphicColor), typeof(Color), typeof(ClassicGraphic), Colors.Green, propertyChanged: GraphicColorPropertyChanged);
        private static void GraphicColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ClassicGraphic).Invalidate();
        }
        public Color GraphicColor
        {
            get => (Color)GetValue(GraphicColorProperty);
            set => SetValue(GraphicColorProperty, value);
        }
        #endregion

        #region TextColor
        public static readonly BindableProperty TextColorProperty =
           BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ClassicGraphic), Colors.Green, propertyChanged: TextColorPropertyChanged);
        private static void TextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ClassicGraphic).Invalidate();
        }
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        #endregion

        #region СoordinateLineColor
        public static readonly BindableProperty СoordinateLineColorProperty =
           BindableProperty.Create(nameof(СoordinateLineColor), typeof(Color), typeof(ClassicGraphic), Colors.Green, propertyChanged: СoordinateLineColorPropertyChanged);
        private static void СoordinateLineColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ClassicGraphic).Invalidate();
        }
        public Color СoordinateLineColor
        {
            get => (Color)GetValue(СoordinateLineColorProperty);
            set => SetValue(СoordinateLineColorProperty, value);
        }
        #endregion

        public List<Point> _Points { get; private set; } = new List<Point>();
        public List<int> PointY { get; private set; } = new List<int>();
        public string LabelFormat { get; internal set; }

        public ClassicGraphic()
        {
            this.SizeChanged += ClassicGraphic_SizeChanged;
            Drawable = new ClassicGraphicDrawable(this);
            this.Invalidate();
        }
        private void ClassicGraphic_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        public void DrawClickPoint()
        {
            this.Invalidate();
        }
    }
}
