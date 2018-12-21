using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHistoryCore
{
   public class HistoryBaseModel
    {
        public DateTime? CrearedDateTime { get; set; }
        public DateTime DeletedDatTime { get; set; }
        public DateTime LastEditedDateTime { get; set; }
        public string LastAccessIp { get; set; }
        public string LastAccesDevice { get; set; }
        public bool IsDeleted { get; set; }
    }
}
