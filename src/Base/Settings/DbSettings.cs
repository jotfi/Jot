using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace jotfi.Jot.Base.Settings
{
    public class DbSettings
    {
        public int ConnectionType { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string ConnectionString { get; set; }
    }
}
