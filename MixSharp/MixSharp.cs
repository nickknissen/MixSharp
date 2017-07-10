using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace MixSharp
{
    public class MixSharp
    {
        public static Dictionary<string, string> manifest;
        public static string Mix(string path, string manifestDirectory = "")
        {
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            if (manifestDirectory != string.Empty && !manifestDirectory.StartsWith("/"))
            {
                manifestDirectory = "/" + manifestDirectory;
            }
            if (File.Exists(PublicPath(manifestDirectory + "hot")))
            {
                return string.Format("//localhost:8080{0}", path);
            }

            if (manifest == null)
            {
                var manifestPath = PublicPath(manifestDirectory + "mix-manifest.json");
                if (!File.Exists(manifestPath))
                {
                    throw new FileNotFoundException();
                }

                manifest = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@"" + manifestPath));
            }

            return manifestDirectory + manifest[path];
        }

        public static string PublicPath(string path)
        {
            var appSettings = ConfigurationManager.AppSettings;

            string realtivePath = ConfigurationManager.AppSettings["publicPath"];
            if (realtivePath == null)
            {
                throw new ConfigurationMissingException();
            }
            string publicPath = AppDomain.CurrentDomain.BaseDirectory + realtivePath;

            if (path == string.Empty)
            {
                return publicPath;
            }
            return string.Format(@"{0}{1}", publicPath, path);
        }

    }
    [Serializable]
    public class ConfigurationMissingException : ConfigurationErrorsException
    { }
}
