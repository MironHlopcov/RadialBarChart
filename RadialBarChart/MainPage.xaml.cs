using RadialBarChart.Controls;

namespace RadialBarChart;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        InitializeComponent();
        var points = new List<Point>();
        points.Add(new Point(10, 90));
        points.Add(new Point(20, 1.80));
        points.Add(new Point(30, 90));
        points.Add(new Point(40, 180));
        points.Add(new Point(50, 150));
        points.Add(new Point(60, 40));
        points.Add(new Point(70, 2.60));
        points.Add(new Point(80, 140));
        points.Add(new Point(90, 20));
        points.Add(new Point(100, 5.90));
        points.Add(new Point(110, 120));
        points.Add(new Point(120, 40));
        points.Add(new Point(130, 290));
        points.Add(new Point(140, 3.10));
        points.Add(new Point(150, 0));
        points.Add(new Point(160, 90));
        points.Add(new Point(170, 10));
        points.Add(new Point(180, 0));
        points.Add(new Point(190, 90));
        points.Add(new Point(200, 10));
        points.Add(new Point(210, 0));
        points.Add(new Point(220, 90));
        points.Add(new Point(230, 10));
        points.Add(new Point(240, 0));
        points.Add(new Point(250, 90));
        points.Add(new Point(260, 180));
        points.Add(new Point(970, 90));

        ClassicGrapchic.Points.AddRange(points);



        var entris = new ChartEntry[]
       {
            new ChartEntry
            {
                Value = 10,
                Color = Color.FromArgb("#6023FF"),
                Text = "Visual Studio Code"
            },
            new ChartEntry
            {
                Value = 3,
                Color = Color.FromArgb("#3059FE"),
                Text = "Visual Studio"
            },
            new ChartEntry
            {
                Value = 120,
                Color = Color.FromArgb("#79F1D2"),
                Text = "Notepad++"
            },
            new ChartEntry
            {
                Value = 56,
                Color = Color.FromArgb("#68486E"),
                Text = "IntelliJ1"
            },
            new ChartEntry
            {
                Value = 45,
                Color = Color.FromArgb("#28476E"),
                Text = "IntelliJ2"
            },
            new ChartEntry
            {
                Value = 999,
                Color = Color.FromArgb("#78426E"),
                Text = "IntelliJ3"
            }
       };
        BindingContext = entris.OrderBy(entris => entris.Value).Reverse();
        // RadialBar.Entries = entris;

    }
}

