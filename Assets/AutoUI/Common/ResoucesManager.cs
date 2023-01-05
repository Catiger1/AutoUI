using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class ResourcesManager
    {
        private static Dictionary<string, string> configMap;

        [System.Obsolete]
        static ResourcesManager()
        {
            configMap = new Dictionary<string, string>();
            string context = ConfigurationReader.GetConfigFile("ConfigMap.txt");
            ConfigurationReader.Reader(context, BulidMap);
        }

        public static T Load<T>(string prefabName)where T: Object
        {
            if (!configMap.ContainsKey(prefabName)) return null;
            return Resources.Load<T>(configMap[prefabName]);
        }
        private static void BulidMap(string line)
        {
            string[] keyValue = line.Split('=');
            configMap.Add(keyValue[0], keyValue[1]);
        }
    }
}
