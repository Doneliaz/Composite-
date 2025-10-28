// Models/Circle.cs (Modificado)
using Microsoft.AspNetCore.Components;

namespace BlazorComposite.Models
{
    // Circle ahora es solo un "Contexto" ligero
    public class Circle : IShape
    {
        // --- 1. Estado Extrínseco (Único) ---
        public string Id { get; } = Guid.NewGuid().ToString();
        public double X { get; set; }
        public double Y { get; set; }
        public int Radius { get; set; } = 20;
        public string Fill { get; set; } = "red";
        public string Stroke { get; set; } = "black";
        public int StrokeWidth { get; set; } = 2;

        // --- 2. Referencia al Flyweight (Compartido) ---
        private readonly IShapeFlyweight _flyweight;

        public Circle(double x, double y)
        {
            X = x;
            Y = y;
            // (Esto asume que crearás un 'CircleFlyweight' 
            // y lo añadirás a tu 'ShapeFlyweightFactory')
            _flyweight = ShapeFlyweightFactory.GetFlyweight("Circle");
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
                Radius = this.Radius,
                Fill = this.Fill,
                Stroke = this.Stroke,
                StrokeWidth = this.StrokeWidth
            };

            // Pasamos el contexto al flyweight compartido
            return _flyweight.Draw(context);
        }
    }
}