using System;
using System.IO;
using System.Text.Json;
using OpenWith.Model;

namespace OpenWith.Services.impl
{
    [Obsolete("Use YamlConfigReader instead")]
    public class JsonConfigProvider : IConfigProvider
    {
        private readonly string _fileName;
        
        public JsonConfigProvider(string fileName) {
            _fileName = fileName;
        }

        public Config GetConfig() {
            return JsonSerializer.Deserialize<Config>(
                File.ReadAllBytes(_fileName), 
                new JsonSerializerOptions() {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    AllowTrailingCommas = true
                } 
            );
        }
    }
}
