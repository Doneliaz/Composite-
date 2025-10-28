// Models/UmlClass.cs
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BlazorComposite.Models // Asegúrate que el namespace sea correcto
{
    public class UmlClass : IShape
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public double X { get; set; }
        public double Y { get; set; }

        // --- Propiedades específicas de UML ---
        public string ClassName { get; set; } = "ClassName";
        public List<string> Attributes { get; set; } = new List<string>();
        public List<string> Methods { get; set; } = new List<string>();
        public double Width { get; set; } = 200; // Ancho fijo

        // --- Constantes para el dibujo ---
        private const double HeaderHeight = 30;
        private const double TextLineHeight = 18;
        private const double Padding = 5;

        public UmlClass(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        // --- Aquí está la magia ---
        public RenderFragment Draw() => builder =>
        {
            // --- Calcular alturas de las secciones ---
            double attributesHeight = Attributes.Any() ? (Attributes.Count * TextLineHeight) + Padding * 2 : 0;
            double methodsHeight = Methods.Any() ? (Methods.Count * TextLineHeight) + Padding * 2 : 0;
            double totalHeight = HeaderHeight + attributesHeight + methodsHeight;
            
            // Si hay atributos, añade espacio para la línea separadora
            if (attributesHeight > 0) totalHeight += 1;
            // Si hay métodos, añade espacio para la línea separadora
            if (methodsHeight > 0) totalHeight += 1;


            // 1. Grupo principal (<g>)
            // Usamos 'transform' para mover el grupo completo. 
            // El 'Id' debe ir en este grupo para que JS lo pueda arrastrar.
            builder.OpenElement(0, "g");
            builder.AddAttribute(1, "id", Id);
            builder.AddAttribute(2, "transform", $"translate({X}, {Y})");
            builder.AddAttribute(3, "class", "uml-class-group"); // Para CSS opcional

            // 2. Rectángulo principal (el contenedor)
            builder.OpenElement(4, "rect");
            builder.AddAttribute(5, "width", Width);
            builder.AddAttribute(6, "height", totalHeight);
            builder.AddAttribute(7, "fill", "#FFFFE0"); // Un color crema
            builder.AddAttribute(8, "stroke", "black");
            builder.AddAttribute(9, "stroke-width", 1.5);
            builder.CloseElement(); // </rect>

            // 3. Texto del Nombre de la Clase
            builder.OpenElement(10, "text");
            builder.AddAttribute(11, "x", Width / 2); // Centrado horizontal
            builder.AddAttribute(12, "y", HeaderHeight / 2 + 5); // Centrado vertical en cabecera
            builder.AddAttribute(13, "text-anchor", "middle"); // Centrado SVG
            builder.AddAttribute(14, "font-weight", "bold");
            builder.AddAttribute(15, "font-size", "14px");
            builder.AddAttribute(16, "font-family", "Arial, sans-serif");
            builder.AddContent(17, ClassName);
            builder.CloseElement(); // </text>

            // --- Dibujar Atributos y Métodos ---
            double currentY = HeaderHeight;

            // 4. Sección de Atributos
            if (attributesHeight > 0)
            {
                // Línea separadora
                builder.OpenElement(18, "line");
                builder.AddAttribute(19, "x1", 0);
                builder.AddAttribute(20, "y1", currentY);
                builder.AddAttribute(21, "x2", Width);
                builder.AddAttribute(22, "y2", currentY);
                builder.AddAttribute(23, "stroke", "black");
                builder.AddAttribute(24, "stroke-width", 1);
                builder.CloseElement(); // </line>
                
                currentY += Padding;

                // Escribir cada atributo
                foreach (var attr in Attributes)
                {
                    currentY += TextLineHeight;
                    builder.OpenElement(25, "text");
                    builder.AddAttribute(26, "x", Padding); // Padding izquierdo
                    builder.AddAttribute(27, "y", currentY - 4); // Ajuste de línea base
                    builder.AddAttribute(28, "font-family", "Consolas, 'Courier New', monospace");
                    builder.AddAttribute(29, "font-size", "13px");
                    builder.AddContent(30, attr);
                    builder.CloseElement(); // </text>
                }
                currentY += Padding; // Padding inferior
            }

            // 5. Sección de Métodos
            if (methodsHeight > 0)
            {
                // Línea separadora
                builder.OpenElement(31, "line");
                builder.AddAttribute(32, "x1", 0);
                builder.AddAttribute(33, "y1", currentY);
                builder.AddAttribute(34, "x2", Width);
                builder.AddAttribute(35, "y2", currentY);
                builder.AddAttribute(36, "stroke", "black");
                builder.AddAttribute(37, "stroke-width", 1);
                builder.CloseElement(); // </line>

                currentY += Padding;

                // Escribir cada método
                foreach (var method in Methods)
                {
                    currentY += TextLineHeight;
                    builder.OpenElement(38, "text");
                    builder.AddAttribute(39, "x", Padding);
                    builder.AddAttribute(40, "y", currentY - 4);
                    builder.AddAttribute(41, "font-family", "Consolas, 'Courier New', monospace");
                    builder.AddAttribute(42, "font-size", "13px");
                    builder.AddAttribute(43, "font-style", "italic"); // Los métodos a menudo van en cursiva
                    builder.AddContent(44, method);
                    builder.CloseElement(); // </text>
                }
            }

            builder.CloseElement(); // </g> (cierre del grupo principal)
        };
    }
}