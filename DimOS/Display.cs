using System;
using System.Collections.Generic;
using System.Text;

using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Point = Cosmos.System.Graphics.Point;

namespace DimOS
{
    public class Display
    {
        public Canvas canvas;
        Color color = new Color();

        public Display() => canvas = FullScreenCanvas.GetFullScreenCanvas();

        public void SetDisplayColor(Color color)
        {
            this.color = color;
        }

        

        public Color GetDisplayColor()
        {
            return this.color;
        }

        public void DrawFilledRectangle(Point position, int sizeX, int sizeY, Color rectColor)
        {
            canvas.DrawFilledRectangle(new Pen(rectColor), position, sizeX, sizeY);
        }

        public void DrawRectangle(Point position, int sizeX, int sizeY, Color rectColor)
        {
            canvas.DrawRectangle(new Pen(rectColor), position, sizeX, sizeY);
        }

        public void ClearBox(Point position, int sizeX, int sizeY)
        {
            canvas.DrawFilledRectangle(new Pen(this.color), position, sizeX, sizeY);
        }

        public void ShowDisplay()
        {
            canvas.Clear(color);
        }

        public void ApplyColor()
        {
            canvas.Clear(color);
        }
    }
}

