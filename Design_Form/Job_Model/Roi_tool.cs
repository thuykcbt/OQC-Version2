using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Form.Job_Model
{
    public abstract class Roi_tool : INotifyPropertyChanged
	{
        public string type { get; set; }
		public string Type
		{
			get => type;
			set
			{
				type = value;
				OnPropertyChanged(nameof(Type));
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		//public Roi_tool(string roiName)
		//{
		//    Type = roiName;
		//}

		public class LineROI : Roi_tool
        {
            public double StartX { get; set; }
            public double StartY { get; set; }
            public double EndX { get; set; }
            public double EndY { get; set; }

            public LineROI(double startX, double startY, double endX, double endY)
            {
                Type = "Line";
                StartX = startX;
                StartY = startY;
                EndX = endX;
                EndY = endY;
            }
        }

        public class RectangleROI : Roi_tool
        {
            public double Width { get; set; }
            public double Height { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Phi { get; set; }

            public RectangleROI(double x, double y, double phi, double width, double height)
            {
                Type = "Rectangle";
                X = x;
                Y = y;
                Width = width;
                Height = height;
                Phi = phi;
            }
        }
        public class PolygonROI : Roi_tool
        {
            public List<double> StartX { get; set; }
            public List<double> StartY { get; set; }
            public PolygonROI(List<double> startX, List<double> startY)
            {
                Type = "Polygon";
                StartX = startX;
                StartY = startY;
            }
        }
        public class CircleROI : Roi_tool
        {
            public double Radius { get; set; }
            public double CenterX { get; set; }
            public double CenterY { get; set; }

            public CircleROI(double centerX, double centerY, double radius)
            {
                Type = "Circle";
                CenterX = centerX;
                CenterY = centerY;
                Radius = radius;
            }
        }

    }
}
