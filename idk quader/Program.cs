using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using static System.Console;
using static Yann.wc;

namespace OxyPlotExample

{
    class Program
    {
        static void Main(string[] args)
        {
            var today = DateTime.Now;
            var filename = $"New-Graph_{today:dd-MM-yyyy_HH-mm-ss}.pdf";
            double[] yValues = new double[19];
            for (int i = 1; i < 20; i++)
            {
                double erg = (60 - 2 * i) * (40 - 2 * i) * i;
                WriteLineColor($"<*yellow*>{i,2}<*/*> : <*green*>{erg,4}<*/*>");
                yValues[i - 1] = erg;
            }

            // Create the plot model
            var plotModel = new PlotModel { Title = "Graph", Background = OxyColor.FromRgb(255, 255, 255) };

            // Create the X and Y axes
            var xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "X Value" };
            var yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Y Value" };
            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            // Create the data series
            var series = new LineSeries { Title = "My Curve", MarkerType = MarkerType.Triangle };
            for (int i = 0; i < yValues.Length; i++)
            {
                series.Points.Add(new DataPoint(i + 1, yValues[i]));
            }
            plotModel.Series.Add(series);

            using (var stream = File.Create(filename))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(plotModel, stream);
            }
            var thisfolder = Directory.GetCurrentDirectory();
            WriteLine("Click here to open the graph.");
            WriteLine($"\x1B]8;;{thisfolder}\\{filename}\a{filename}\x1B]8;;\a");
            ReadKey();
        }
    }
}

