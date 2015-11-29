using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLampApp.Pasers
{
    public class HueLampParsers
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
    }
}
