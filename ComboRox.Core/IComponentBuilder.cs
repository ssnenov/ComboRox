using System.Collections.Generic;

namespace ComboRox.Core
{
    public interface IComponentBuilder<TComponent, in TComponentJsonObject>
    {
        List<TComponent> Create(IEnumerable<TComponentJsonObject> componentObjects);
    }
}