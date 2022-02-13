using System;
using System.Collections.Generic;
using System.Text;

namespace Take.Api.TaskHard.Models.Models
{
    public class UserLogin
    {
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}
