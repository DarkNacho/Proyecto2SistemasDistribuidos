using System;
using System.Collections.Generic;
using System.Text;

namespace EmpresaServidor
{
    class ServerInfoModel
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public override string ToString() => $"{Ip}:{Port}";
    }

    class ConfigurationModel
    {

        public ServerInfoModel ServerInfo { get; set; }
        public bool Autoconnection { get; set; }
        public string[] Combustibles { get; set; }
    }
}
