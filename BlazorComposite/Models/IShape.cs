// Models/IShape.cs
using Microsoft.AspNetCore.Components;

namespace BlazorComposite.Models // Asegúrate de que el namespace sea correcto
{
    public interface IShape
    {
        // Propiedades para la posición
        public double X { get; set; }
        public double Y { get; set; }
        public string Id { get; } // Un ID único para identificar el elemento en el DOM

        void Move(double dx, double dy);

        // RenderFragment para dibujar el SVG
        RenderFragment Draw();
    }
}