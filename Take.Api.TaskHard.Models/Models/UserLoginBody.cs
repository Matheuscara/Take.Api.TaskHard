using System;
using System.Collections.Generic;
using System.Text;

namespace Take.Api.TaskHard.Models.Models
{
    public class UserLoginBody
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }
}
