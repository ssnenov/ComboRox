using System.Collections.Generic;

namespace ComboRox.Core
{
    public interface IComponentFactory<TComponent, in TComponentJsonObject>
    {
        List<TComponent> Create(IEnumerable<TComponentJsonObject> componentObjects);
    }
}