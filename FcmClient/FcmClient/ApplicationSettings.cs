using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FcmClient
{
    public class ApplicationSettings
    {
        /// <summary>
        /// Returns user credentials, where first item value is login, second is password
        /// </summary>
        /// <returns></returns>
        public static ValueTuple<string,string> GetCredentials()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ApplicationSettings)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("FcmClient.appsettings.json");

            StreamReader sr = new StreamReader(stream);
            
            var text = sr.ReadToEnd();

            var jObject = JsonConvert.DeserializeObject(text) as JObject;

            var login = (string) jObject["Credentials"]["Login"];
            var password = (string)jObject["Credentials"]["Password"];

            return new ValueTuple<string, string>(login, password);
        }

        /// <summary>
        /// Returns mobile token, that was last saved in users settings
        /// </summary>
        /// <returns></returns>
        public static string GetMobileToken()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ApplicationSettings)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("FcmClient.appsettings.json");

            //var settingsFile = Path
            //       .Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "appsettings.json");

            StreamReader sr = new StreamReader(stream);

            var text = sr.ReadToEnd();

            var jObject = JsonConvert.DeserializeObject(text) as JObject;

            var token = (string)jObject["Credentials"]["MobileToken"];

            return token;
        }

        /// <summary>
        /// Sets mobile token.
        /// </summary>
        /// <returns></returns>
        public static void SetMobileToken(string token)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ApplicationSettings)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("FcmClient.appsettings.json");

            StreamReader sr = new StreamReader(stream);

            var text = sr.ReadToEnd();

            var jObject = JsonConvert.DeserializeObject(text) as JObject;

            var jToken = (JToken)jObject["Credentials"]["MobileToken"];
            jToken.Replace(token);

            if (File.Exists("FcmClient.appsettings.json"))
            { 
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("FcmClient.appsettings.json", output);
            }
        }
    }
}
