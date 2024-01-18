using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VScoutCommon.Models;

namespace VScoutCommon.Common
{
    public static class ConfigParser
    {
        public static Config GetConfig()
        {
            if (!File.Exists("config.txt"))
            {
                throw new InvalidOperationException("Config file is missing.");
            }

            Config config = new Config();
            foreach (string line in File.ReadLines("config.txt"))
            {
                string[] lineParts = line.Split(new[] { ':' });
                switch (lineParts[0].ToLower())
                {
                    case "station":
                        config.Station = lineParts[1];
                        break;
                    default:
                        throw new InvalidOperationException("Unknown config setting.");
                }
            }

            return config;
        }
    }
}
