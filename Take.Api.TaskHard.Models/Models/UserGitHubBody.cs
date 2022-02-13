using System;
using System.Collections.Generic;
using System.Text;

namespace Take.Api.TaskHard.Models.Models
{
    public class UserGitHubBody
    {
        public string org { get; set; }
        public string language { get; set; }
        public string direction { get; set; }
        public string type { get; set; }
        public int perPage { get; set; }
    }
}
