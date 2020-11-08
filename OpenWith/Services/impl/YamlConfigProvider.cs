using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OpenWith.Model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace OpenWith.Services.impl
{
    public class YamlConfigProvider : IConfigProvider
    {
        private readonly string _fileName;
        
        public YamlConfigProvider(string fileName) {
            _fileName = fileName;
        }

        public Config GetConfig() {
            var deserializer = new DeserializerBuilder()
                               .WithNamingConvention(CamelCaseNamingConvention.Instance)
                               .Build();
            using (var yaml = File.OpenText(_fileName)) {
                return deserializer.Deserialize<Config>(yaml);
            }
        }
    }
}
