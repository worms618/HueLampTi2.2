using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLampApp.MainpageObjects
{
    public class UriRouter
    {
        public string IP { get; set; }
        public string Username { get; set; }
        public int Port { get; set; }

        private UriRouter() { }

        public UriRouter(string username = "",int port = 80,string Ip = "Localhost")
        {
            IP = Ip;
            Username = username;
            Port = port;
        }


    }
}
