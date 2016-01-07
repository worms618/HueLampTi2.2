using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace HueLampApp.Objects
{
    public class HueLampFactory
    {
        public static void UpdateHueLamp(HueLamp lamp, JObject jsonObject)
        {
            string lampId = "" + lamp.ID;
            lamp.Name = (string)jsonObject      [lampId]["name"];
            lamp.On = (bool)jsonObject          [lampId]["state"]["on"];
            lamp.Brightness = (int)jsonObject   [lampId]["state"]["bri"];
            lamp.Hue = (long)jsonObject         [lampId]["state"]["hue"];
            lamp.Sat = (int)jsonObject          [lampId]["state"]["sat"];
        }

        public static void SendAllPropertys(HueLamp lamp)
        {
            HttpStringContent content = new HttpStringContent
                    (
                    $"{{\"on\":{lamp.On.ToString().ToLower()},\"bri\":{lamp.Brightness.ToString().ToLower()},\"hue\":{lamp.Hue.ToString().ToLower()},\"sat\":{lamp.Sat.ToString().ToLower()}}}",
                                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                                "application/json"
                    );
            SendProperty(content, lamp.ID);
        }

        public static void SendBrightnessProperty(HueLamp lamp)
        {
            HttpStringContent content = new HttpStringContent
                (
                $"{{\"bri\":{lamp.Brightness.ToString().ToLower()}}}",
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json"
                );
            SendProperty(content, lamp.ID);
        }

        public static void SendSatProperty(HueLamp lamp)
        {
            HttpStringContent content = new HttpStringContent
                (
                $"{{\"sat\":{lamp.Sat.ToString().ToLower()}}}",
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json"
                );
            SendProperty(content, lamp.ID);
        }

        public static void SendHueProperty(HueLamp lamp)
        {
            HttpStringContent content = new HttpStringContent
                (
                $"{{\"hue\":{lamp.Hue.ToString().ToLower()}}}",
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json"
                );
            SendProperty(content, lamp.ID);
        }

        public static void SendOnProperty(HueLamp lamp)
        {
            HttpStringContent content = new HttpStringContent
                (
                $"{{\"on\":{lamp.On.ToString().ToLower()}}}",
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json"
                );
            SendProperty(content, lamp.ID);
        }
                
        private static void SendProperty(HttpStringContent content,int id)
        {
            BridgeConnector bc = BridgeConnector.Instance;            
            Uri uriLampState = new Uri($"http://{bc.Ip}:{bc.Port}/api/{bc.Username}/light/{id}/state");
            bc.PutMessage(content,uriLampState);
        }
    }
}
