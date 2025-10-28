// Models/Rectangle.cs (Modificado)
using Microsoft.AspNetCore.Components;

namespace BlazorComposite.Models
{
    // Rectangle ahora es solo un "Contexto" ligero
    public class Rectangle : IShape
    {
        // --- 1. Estado Extrínseco (Único) ---
        public string Id { get; } = Guid.NewGuid().ToString();
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; } = 80;
        public int Height { get; set; } = 50;
        public string Fill { get; set; } = "blue";
        public string Stroke { get; set; } = "black";
        public int StrokeWidth { get; set; } = 2;

        // --- 2. Referencia al Flyweight (Compartido) ---
        private readonly IShapeFlyweight _flyweight;

        public Rectangle(double x, double y)
        {
            X = x;
            Y = y;
            // (Esto asume que crearás un 'RectangleFlyweight' 
            // y lo añadirás a tu 'ShapeFlyweightFactory')
            _flyweight = ShapeFlyweightFactory.GetFlyweight("Rectangle");
        }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        // --- 3. El método Draw() delega ---
        public RenderFragment Draw()
        {
            // Creamos el contexto
            var context = new ShapeContext
            {
                Id = this.Id,
                X = this.X,
                Y = this.Y,
                Width = this.Width,
                Height = this.Height,
                Fill = this.Fill,
                Stroke = this.Stroke,
                StrokeWidth = this.StrokeWidth
            };

            // Pasamos el contexto al flyweight compartido
            return _flyweight.Draw(context);
        }
    }
}