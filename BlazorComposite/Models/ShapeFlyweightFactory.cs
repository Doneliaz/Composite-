// Models/ShapeFlyweightFactory.cs
using System.Collections.Generic;

namespace BlazorComposite.Models
{
    public static class ShapeFlyweightFactory
    {
        // El "pool" de flyweights compartidos.
        private static Dictionary<string, IShapeFlyweight> _flyweights = new Dictionary<string, IShapeFlyweight>();

        public static IShapeFlyweight GetFlyweight(string key)
        {
            // La clave podría ser "UmlClass", "Circle", etc.
            if (!_flyweights.ContainsKey(key))
            {
                // Si no existe en el pool, lo creamos
                switch (key)
                {
                    case "UmlClass":
                        _flyweights[key] = new UmlClassFlyweight();
                        break;
                    // case "Circle":
                    //    _flyweights[key] = new CircleFlyweight();
                    //    break;
                }
            }
            // Devolvemos la instancia (nueva o existente)
            return _flyweights[key];
        }
    }
}