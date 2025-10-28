using Microsoft.AspNetCore.Components;

namespace BlazorComposite.Models
{

    public interface IShapeFlyweight
    {

        RenderFragment Draw(ShapeContext context);
    }
}
