using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jotfi.Jot.Base.Settings
{
    public class BaseSettings
    {
        public bool IsClient { get; set; }
        public bool IsConsole { get; set; }
        [Display(Name = "Server URL")]
        public string ServerUrl { get; set; } = "";
        public int DbDialect { get; set; }
        public string DbDirectory { get; set; }
    }
}
