using System;
using System.Collections.Generic;
using System.Text;

namespace wt.basic.service.Common
{
    public class wtJsonResult
    {
        public bool success { get; set; } = true;
        public int code { get; set; }
        public string msg { get; set; }
        public object obj { get; set; } = null;
        public object list { get; set; } = null;
    }
}
