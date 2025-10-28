// Models/Rectangle.cs
using Microsoft.AspNetCore.Components;

namespace BlazorComposite.Models
{
    public class Rectangle : IShape
    {
        public string Id { get; } = Guid.NewGuid().ToString(); // ID único
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; } = 80; // Aumentar para UML
        public int Height { get; set; } = 50; // Aumentar para UML
        public string Fill { get; set; } = "blue";
        public string Stroke { get; set; } = "black";
        public int StrokeWidth { get; set; } = 2;

        public Rectangle(double x, double y)
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
            builder.OpenElement(0, "rect");
            builder.AddAttribute(1, "id", Id); // Es importante el ID
            builder.AddAttribute(2, "x", X - Width / 2); // Centrar el rect al clic
            builder.AddAttribute(3, "y", Y - Height / 2); // Centrar el rect al clic
            builder.AddAttribute(4, "width", Width);
            builder.AddAttribute(5, "height", Height);
            builder.AddAttribute(6, "fill", Fill);
            builder.AddAttribute(7, "stroke", Stroke);
            builder.AddAttribute(8, "stroke-width", StrokeWidth);
            builder.CloseElement();
        };
    }
}