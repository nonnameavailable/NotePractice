using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice
{
    public static class MyGraphics
    {
        public static GraphicsPath ArcPath(OVector start, OVector end, double radius, int steps)
        {
            GraphicsPath path = new GraphicsPath();
            double distance = start.Distance(end);
            double sweepAngle = Math.Asin(distance / 2 / radius) * 360 / Math.PI;
            OVector halfway = end.Copy().Subtract(start).Divide(2d);
            double triDist = Math.Sqrt(radius * radius - halfway.Magnitude * halfway.Magnitude);
            OVector center = halfway.Copy().Rotate(90).Normalize().Multiply(triDist).Add(start).Add(halfway);
            OVector arm = start.Copy().Subtract(center);
            double angleIncrement = sweepAngle / steps;
            for (int i = 0; i < steps; i++)
            {

                OVector lp1 = center.Copy().Add(arm);
                arm.Rotate(angleIncrement);
                OVector lp2 = center.Copy().Add(arm);
                path.AddLine(lp1.ToPoint(), lp2.ToPoint());
            }
            return path;
        }
        public static GraphicsPath FlagPath(OVector start, OVector end)
        {
            OVector direction = end.Copy().Subtract(start);
            double startPortion = 0.45;
            OVector midPoint = start.Copy().Add(direction.Copy().Multiply(startPortion));
            using GraphicsPath startArc = ArcPath(midPoint, start, direction.Magnitude * 0.6, 10);
            using GraphicsPath endArc = ArcPath(midPoint, end, direction.Magnitude * 0.6, 10);
            startArc.Reverse();
            GraphicsPath path = new GraphicsPath();
            path.AddPath(startArc, true);
            path.AddPath(endArc, true);
            return path;
        }
        public static void DrawPathInterpolatedWidths(Graphics g, GraphicsPath path, float startWidth, float endWidth)
        {
            PointF[] points = path.PathPoints;
            for(int i = 0; i < points.Length - 1; i++)
            {
                double distance = i / (double)points.Length;
                float width = Lerp(startWidth, endWidth, distance);
                using Pen pen = new Pen(Color.Black, width);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, points[i], points[i + 1]);
            }
        }
        private static float Lerp(float start, float end, double distance)
        {
            return (float)(start + (end - start) * distance);
        }
    }
}
