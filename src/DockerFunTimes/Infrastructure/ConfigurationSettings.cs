using System;
using System.IO;
using Newtonsoft.Json;

namespace DockerFunTimes.Infrastructure
{
    public class ConfigurationSettings
    {
        private readonly Configuration _configuration;

        public ConfigurationSettings()
        {
            if (File.Exists("/config/config.json"))
            {
                _configuration = JsonConvert.DeserializeObject<Configuration>(
                    File.ReadAllText("/config/config.json"));
            }
            else
            {
                var contents = File.ReadAllText("./localconfig.json");
                _configuration = JsonConvert.DeserializeObject<Configuration>(contents);
            }
        }
        
        public Configuration Configuration { get { return _configuration; } }
    }
}