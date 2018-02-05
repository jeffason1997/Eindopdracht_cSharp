using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht_cSharp.Game
{
    public class Coin
    {
        public int radius { get; set; }
        public Point midPoint { get; set; }
        public Point realMidpoint { get; set; }
        public int column { get; set; }
        public Color color { get; set; }

        public Coin(int rad, Point mid, Color col, int colm)
        {
            radius = rad;
            midPoint = mid;
            color = col;
            column = colm;
            realMidpoint = new Point(midPoint.X + (radius/2), midPoint.Y + (radius / 2));
        }

        public Point getTempMidPoint()
        {
            return new Point(realMidpoint.X,realMidpoint.Y);
        }

        public void draw(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);

            int x, y;
            x = midPoint.X;
            y = midPoint.Y;
            g.FillEllipse(brush, x, y, radius, radius);
        }

        public void draw2(Graphics g, int num)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush);
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               16,
               FontStyle.Regular,
               GraphicsUnit.Pixel);
            int x, y;
            x = realMidpoint.X;
            y = realMidpoint.Y;
            g.DrawString(num.ToString(),font,brush, new Point(x, y));
            
        }
    }
}
