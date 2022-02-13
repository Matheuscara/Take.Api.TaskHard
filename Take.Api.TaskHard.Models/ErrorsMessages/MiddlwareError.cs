using System;
using System.Collections.Generic;
using System.Text;

namespace Take.Api.TaskHard.Models.ErrorsMessages
{
    public class MiddlwareError
    {
        public string ErrorMessage { get; set; }
        public string TraceId { get; set; }
    }
}
