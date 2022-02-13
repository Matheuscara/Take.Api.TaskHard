using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Take.Api.TaskHard.Models.Models
{
    public class GitResponseList
    {
        [JsonProperty("items")]
        public List<Repository> items;
    }
}
