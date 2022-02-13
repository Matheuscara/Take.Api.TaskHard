using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Newtonsoft.Json;

namespace Take.Api.TaskHard.Models.Models
{
    public class Repository
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        [Required]
        [JsonProperty("description")]
        public string Description { get; set; }
        [Required]
        [JsonProperty("owner")]
        public Owner Owner { get; set; }
    }
}
