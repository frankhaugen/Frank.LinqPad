using System.Collections;
using VarDump.Visitor.Descriptors;

namespace LINQPadQuery;

internal class PropertySortingMiddleware : IObjectDescriptorMiddleware
{
    public IObjectDescription GetObjectDescription(object @object, Type objectType, Func<IObjectDescription> prev)
    {
        var objectDescription = prev();

        return new ObjectDescription
        {
            Type = objectDescription.Type,
            ConstructorArguments = objectDescription.ConstructorArguments,
            Properties = objectDescription.Properties.OrderBy(x => x.Value is not IEnumerable && x.Value is not string ? 0 : x.Value is IEnumerable enumerable && enumerable.OfType<object>().Any() ? 1 : 2),
            Fields = objectDescription.Fields
        };
    }
}