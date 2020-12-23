using System;
using System.Collections.Generic;
using System.Text;

namespace Surtidor
{
    class ServerInfoModel
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public override string ToString() => $"{Ip}:{Port}";
    }

    class ConfigurationModel
    {

        public ServerInfoModel ClientServer { get; set; }
        public ServerInfoModel BackUpClientServer { get; set; }
    }
}
