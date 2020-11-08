using System.IO;

namespace OpenWith.Filters
{
    public interface IFilter
    {
        bool Accepts(string path);
    }
}
