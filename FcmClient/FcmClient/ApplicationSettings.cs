using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace FcmClient
{
    public class ApplicationSettings
    {


        private static string settingsFilePath { get; set; }

        static ApplicationSettings()
        {
            settingsFilePath =  Path.Combine
                (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "appsettings.json");

            CreateSettingsFileIfNotExist();

        }


        /// <summary>
        /// Returns user credentials, where first item value is login, second is password
        /// </summary>
        /// <returns></returns>
        public static ValueTuple<string,string> GetCredentials()
        {
            var settings = GetApplicationSettings();

            return new ValueTuple<string, string>(settings.Credentials.Login, settings.Credentials.Password);
        }

        public static void SetCredentials(string userId, string login, string password, string mobileToken)
        {
            var settings = GetApplicationSettings();
            settings.Credentials.UserId = userId;
            settings.Credentials.Login = login;
            settings.Credentials.Password = password;
            settings.Credentials.MobileToken = mobileToken;

            UpdateApplicationSettings(settings);
        }

        /// <summary>
        /// Returns mobile token, that was last saved in users settings
        /// </summary>
        /// <returns></returns>
        public static string GetMobileToken()
        {
            var settings = GetApplicationSettings();

            return settings.Credentials.MobileToken;
        }

        /// <summary>
        /// Sets mobile token.
        /// </summary>
        /// <returns></returns>
        public static void SetMobileToken(string token)
        {

            var settings = GetApplicationSettings();

            if (settings == null)
                throw new Exception("Cannot read user settings");

            if (settings.Credentials.MobileToken?.ToUpper() == token?.ToUpper())
                return;

            settings.Credentials.MobileToken = token ?? "";

            UpdateApplicationSettings(settings);

        }

        public static string GetUserId()
        {
            var settings = GetApplicationSettings();
            return settings.Credentials.UserId;
        }
        
        private static void CreateSettingsFileIfNotExist()
        {
            if (!File.Exists(settingsFilePath))
            {

                var body = new ApplicationSettingsBody();
                body.Credentials = new Credentials()
                {
                    Login="",
                    Password="",
                    MobileToken=""
                };

                string settingsFileBody = JsonConvert.SerializeObject(body);

                var stream = File.Create(settingsFilePath);
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(settingsFileBody);
                }
            }
            
        }

        private static void UpdateApplicationSettings(ApplicationSettingsBody settings)
        {
            using (StreamWriter sw = new StreamWriter(settingsFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(settings));
            }
        }


        private static ApplicationSettingsBody GetApplicationSettings()
        {
            ApplicationSettingsBody settings = null;

            try
            {
                using (StreamReader sr = new StreamReader(settingsFilePath))
                {
                    settings = JsonConvert.DeserializeObject<ApplicationSettingsBody>(sr.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return settings;
        }
    }

    class ApplicationSettingsBody
    {
        public Credentials Credentials { get; set; }
    }

    class Credentials
    {
        public string UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string MobileToken { get; set; }
    }
}
