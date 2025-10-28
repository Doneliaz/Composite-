namespace BlazorComposite.Models // Asegúrate que el namespace sea correcto
{
    // Esta clase sirve para mapear el objeto "DOMRect" que devuelve JS
    public class BoundingClientRect
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }
    }
}