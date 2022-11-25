using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Common
{
   
    public class ConfigurationReader
    {

        //读取文件
        [Obsolete]
        public static string GetConfigFile(string fileName)
        {
            string url;
#if UNITY_EDITOR || UNITY_STANDALONE
            url = Application.streamingAssetsPath;
            //如果在iphone下
#elif UNITY_IPHONE
                url = "file://"+Application.dataPath+"/Raw/"+ fileName;
#elif UNITY_ANDROID
                url = Application.streamingAssetsPath;
#endif
            WWW www = new WWW(url);
            while(true)
            {
                if (www.isDone) 
                    return www.text;
            }
        }
        
        public static void Reader(string fileContent,Action<string> handler)
        {
            using(StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine())!=null)
                {
                    handler(line);
                }
            }
        }

    }
}
