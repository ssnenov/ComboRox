using System.Collections.Generic;

namespace ComboRox.Models
{
    public interface IModulesSettings
    {
        List<Filter> Filters { get; set; }
    }
}