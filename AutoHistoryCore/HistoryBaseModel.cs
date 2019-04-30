using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoHistoryCore
{
    

    public class HistoryBaseModel
    {
       [JsonIgnore]
       public string Hs_Change { get; set; }

        public bool IsDeleted { get; set; }
    }
}
