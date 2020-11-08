using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OpenWith.Services
{
    public interface IIconProvider
    {
        Task<ImageSource> GetIcon(string source);
    }
}
