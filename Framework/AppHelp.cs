using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Xml;

namespace Framework
{
    public class AppHelp
    {
        static System.Configuration.Configuration config = null;
        static AppHelp()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        /// <summary>  
        /// //添加键值  
        /// </summary>  
        /// <param name="key"></param>  
        /// <param name="value"></param>  
        public static void AddAppSetting(string key, string value)
        {
            config.AppSettings.Settings.Add(key, value);
            config.Save();
        }

        /// <summary>  
        /// //修改键值  
        /// </summary>  
        /// <param name="key"></param>  
        /// <param name="value"></param>  
        public static void SaveAppSetting(string key, string value)
        {
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);

            config.Save();
        }

        /// <summary>  
        /// //获得键值  
        /// </summary>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static string GetAppSetting(string key)
        {
            return config.AppSettings.Settings[key].Value;
        }

        /// <summary>  
        /// //移除键值  
        /// </summary>  
        /// <param name="key"></param>  
        public static void DelAppSetting(string key)
        {
            config.AppSettings.Settings.Remove(key);
            config.Save();
        }
    }
}
