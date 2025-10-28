// Models/ShapeContext.cs
using System.Collections.Generic;

namespace BlazorComposite.Models 
{
    public class ShapeContext
    {
        // Propiedades comunes
        public string Id { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
        public string Fill { get; set; } = "white";
        public string Stroke { get; set; } = "black";
        public int StrokeWidth { get; set; } = 2;

        // Propiedades de Rectángulo/UmlClass
        public double Width { get; set; } = 100;
        public double Height { get; set; } = 50;

        // Propiedades de Círculo
        public int Radius { get; set; } = 20;

        // Propiedades de UmlClass
        public string ClassName { get; set; } = "ClassName";
        public List<string> Attributes { get; set; } = new List<string>();
        public List<string> Methods { get; set; } = new List<string>();
    }
}