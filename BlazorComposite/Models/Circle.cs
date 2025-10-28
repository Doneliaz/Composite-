// Models/Circle.cs
using Microsoft.AspNetCore.Components;

namespace BlazorComposite.Models
{
    public class Circle : IShape
    {
        public string Id { get; } = Guid.NewGuid().ToString(); // ID único
        public double X { get; set; }
        public double Y { get; set; }
        public int Radius { get; set; } = 20;
        public string Fill { get; set; } = "red";
        public string Stroke { get; set; } = "black";
        public int StrokeWidth { get; set; } = 2;

        public Circle(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        public RenderFragment Draw() => builder =>
        {
            builder.OpenElement(0, "circle");
            builder.AddAttribute(1, "id", Id); // Es importante el ID
            builder.AddAttribute(2, "cx", X);
            builder.AddAttribute(3, "cy", Y);
            builder.AddAttribute(4, "r", Radius);
            builder.AddAttribute(5, "fill", Fill);
            builder.AddAttribute(6, "stroke", Stroke);
            builder.AddAttribute(7, "stroke-width", StrokeWidth);
            builder.CloseElement();
        };
    }
}