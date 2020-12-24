using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora
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
        public ServerInfoModel ServerInfo { get; set; }
        public string DataBaseSource { get; set; }
        public string BackUpDataBaseSource { get; set; }
        public bool Autoconnection { get; set; }
        public int DistribuidoraID { get; set; }
    }
}
