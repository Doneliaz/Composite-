// Models/UmlClass.cs (Modificado)
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComposite.Models // Asegúrate que el namespace sea correcto
{
    // UmlClass ahora es solo un "Contexto" ligero que implementa IShape
    public class UmlClass : IShape
    {
        // --- 1. Estado Extrínseco (Único) ---
        public string Id { get; } = Guid.NewGuid().ToString();
        public double X { get; set; }
        public double Y { get; set; }
        public string ClassName { get; set; } = "ClassName";
        public List<string> Attributes { get; set; } = new List<string>();
        public List<string> Methods { get; set; } = new List<string>();
        public double Width { get; set; } = 200;

        // --- 2. Referencia al Flyweight (Compartido) ---
        private readonly IShapeFlyweight _flyweight;

        public UmlClass(double x, double y)
        {
            X = x;
            Y = y;
            // Obtenemos el flyweight compartido desde la fábrica
            // (Esto asume que ya creaste UmlClassFlyweight y ShapeFlyweightFactory)
            _flyweight = ShapeFlyweightFactory.GetFlyweight("UmlClass");
        }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        // --- 3. ¡EL GRAN CAMBIO! ---
        // El método Draw() ahora delega el trabajo al Flyweight.
        public RenderFragment Draw()
        {
            // Creamos el objeto de contexto con nuestro estado único
            var context = new ShapeContext
            {
                Id = this.Id,
                X = this.X,
                Y = this.Y,
                ClassName = this.ClassName,
                Attributes = this.Attributes,
                Methods = this.Methods,
                Width = this.Width
                // El flyweight usará sus propios colores/fuentes por defecto
            };

            // Le pasamos el contexto al flyweight compartido para que dibuje
            return _flyweight.Draw(context);
        }
    }
}