using System.Collections;

namespace ComboRox.Models
{
    public interface IResultData
    {
        IEnumerable Data { get; set; }

        int Total { get; set; }
    }
}