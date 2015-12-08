using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HueLampApp.Pasers
{
    public class HueLampParser
    {
        public static string GetUsernameFromJson(string jsonString)
        {
            char[] splitArray = { '{', '}', ':', '[', ']', '"' };
            string[] split = jsonString.Split(splitArray);
            int usernameIndex = 0;
            for(int i = 0; i < split.Length; i++)
            {
                //System.Diagnostics.Debug.WriteLine("Split: "+split[i]);
                if(split[i] == "username")
                {
                    usernameIndex = i;
                    break;
                }
            }
            //System.Diagnostics.Debug.WriteLine($"Index: {usernameIndex} - Item: {split[usernameIndex]}");
            usernameIndex += 3;
            //System.Diagnostics.Debug.WriteLine($"Index: {usernameIndex} - Item: {split[usernameIndex]}");
            return split[usernameIndex];
        }

        public static int GetAmountOfLights(string jsonString)
        {
            System.Diagnostics.Debug.WriteLine("All Lights data: "+jsonString);
            JObject jobject = JObject.Parse(jsonString);
            System.Diagnostics.Debug.WriteLine("Amount: " + jobject.Count);
            //bool on = (bool)jobject["1"]["state"]["on"];
            return jobject.Count;
        }        
    }
}
