using System.Collections.Generic;

namespace ComboRox.Models
{
    public class ModulesSettings : IModulesSettings
    {
        public List<Filter> Filters { get; set; }
    }
}