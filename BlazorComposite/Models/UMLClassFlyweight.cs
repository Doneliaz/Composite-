// Models/UmlClassFlyweight.cs
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorComposite.Models
{
    // Este es el Flyweight Concreto. Solo existirá UNA instancia de esta clase.
    public class UmlClassFlyweight : IShapeFlyweight
    {
        // --- Estado Intrínseco (Compartido) ---
        private const double HeaderHeight = 30;
        private const double TextLineHeight = 18;
        private const double Padding = 5;
        private const string FillColor = "#FFFFE0";
        private const string StrokeColor = "black";
        private const string FontBold = "Arial, sans-serif";
        private const string FontMono = "Consolas, 'Courier New', monospace";

        // El método Draw ahora usa el 'context' para obtener los datos
        public RenderFragment Draw(ShapeContext context) => builder =>
        {
            // --- Calcular alturas (usando el contexto) ---
            double attributesHeight = context.Attributes.Any() ? (context.Attributes.Count * TextLineHeight) + Padding * 2 : 0;
            double methodsHeight = context.Methods.Any() ? (context.Methods.Count * TextLineHeight) + Padding * 2 : 0;
            double totalHeight = HeaderHeight + attributesHeight + methodsHeight;
            
            if (attributesHeight > 0) totalHeight += 1;
            if (methodsHeight > 0) totalHeight += 1;

            // 1. Grupo principal (<g>)
            builder.OpenElement(0, "g");
            builder.AddAttribute(1, "id", context.Id); // Usa el ID del contexto
            builder.AddAttribute(2, "transform", $"translate({context.X}, {context.Y})"); // Usa X, Y
            builder.AddAttribute(3, "class", "uml-class-group");

            // 2. Rectángulo principal (usa el estado intrínseco/constantes)
            builder.OpenElement(4, "rect");
            builder.AddAttribute(5, "width", context.Width); // Usa el Width del contexto
            builder.AddAttribute(6, "height", totalHeight);
            builder.AddAttribute(7, "fill", FillColor); // <-- Estado Intrínseco
            builder.AddAttribute(8, "stroke", StrokeColor); // <-- Estado Intrínseco
            builder.AddAttribute(9, "stroke-width", 1.5);
            builder.CloseElement();

            // 3. Texto del Nombre (usa el contexto)
            builder.OpenElement(10, "text");
            builder.AddAttribute(11, "x", context.Width / 2);
            builder.AddAttribute(12, "y", HeaderHeight / 2 + 5);
            builder.AddAttribute(13, "text-anchor", "middle");
            builder.AddAttribute(14, "font-weight", "bold");
            builder.AddAttribute(15, "font-size", "14px");
            builder.AddAttribute(16, "font-family", FontBold); // <-- Estado Intrínseco
            builder.AddAttribute(17, "font-family", FontBold);
            builder.AddContent(18, context.ClassName); // <-- Estado Extrínseco
            builder.CloseElement();

            // ... (Lógica similar para Atributos y Métodos, usando context.Attributes y context.Methods) ...
            
            // (El resto del código de dibujo va aquí, adaptado para usar 'context.')

            builder.CloseElement(); // </g>
        };
    }
}