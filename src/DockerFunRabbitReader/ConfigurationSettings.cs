using System;
using System.IO;
using Newtonsoft.Json;

namespace DockerFunRabbitReader
{
    public class ConfigurationSettings
    {
        private readonly Configuration _configuration;

        public ConfigurationSettings()
        {
            if (File.Exists("/config/config.json"))
            {
                Console.WriteLine("Used config from /config volume");
                _configuration = JsonConvert.DeserializeObject<Configuration>(
                    File.ReadAllText("/config/config.json"));
            }
            else
            {
                Console.WriteLine("Using localconfig.json");
                var contents = File.ReadAllText("./localconfig.json");
                _configuration = JsonConvert.DeserializeObject<Configuration>(contents);
            }
        }
        
        public Configuration Configuration { get { return _configuration; } }
    }
}