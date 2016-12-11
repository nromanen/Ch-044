using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DB
{
    public class AppSetting
    {
        public int Id { get; set; }
        public int PullInterval { get; set; }
        public int PushInterval { get; set; }
    }
}
