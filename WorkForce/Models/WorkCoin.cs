using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForce.Models
{
    public class WorkCoin
    {
        public UserModel User { get; set; }
        
        public UserModel Owner { get; set; }

        public TransactionModel LastTransaction { get; set; }

    }
}
