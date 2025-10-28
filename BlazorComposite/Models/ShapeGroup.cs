// Models/ShapeGroup.cs
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq; // Para usar .Min(), .Max()

namespace BlazorComposite.Models
{
    public class ShapeGroup : IShape
    {
        public string Id { get; } = Guid.NewGuid().ToString(); // ID único para el grupo

        private List<IShape> _children = new List<IShape>();

        // Las coordenadas X, Y de un grupo podrían ser el punto superior izquierdo de su bounding box
        public double X
        {
            get => _children.Any() ? _children.Min(c => c.X) : 0;
            set { /* La lógica de setting X/Y en un grupo es compleja, mejor usar Move */ }
        }
        public double Y
        {
            get => _children.Any() ? _children.Min(c => c.Y) : 0;
            set { /* La lógica de setting X/Y en un grupo es compleja, mejor usar Move */ }
        }

        public void Add(IShape shape)
        {
            _children.Add(shape);
        }

        public void Remove(IShape shape)
        {
            _children.Remove(shape);
        }

        public void Move(double dx, double dy)
        {
            foreach (var child in _children)
            {
                child.Move(dx, dy);
            }
        }

        public RenderFragment Draw() => builder =>
        {
            builder.OpenElement(0, "g");
            builder.AddAttribute(1, "id", Id); // ID para el grupo SVG
            
            int i = 2; // Iniciar desde un índice diferente para los contenidos
            foreach (var child in _children)
            {
                builder.AddContent(i++, child.Draw());
            }
            
            builder.CloseElement();
        };
    }
}