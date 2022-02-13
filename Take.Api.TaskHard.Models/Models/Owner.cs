using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Take.Api.TaskHard.Models.Models
{
    public class Owner
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
