using System.Threading;
using System.Threading.Tasks;
using OpenWith.Model;

namespace OpenWith.Services
{
    public interface IConfigProvider
    {
        Config GetConfig();
    }
}
