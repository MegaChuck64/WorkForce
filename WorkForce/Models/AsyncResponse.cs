using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForce.Models
{
    public class AsyncResponse
    {
        public bool Failed { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
