using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForce.Models
{
    public class TransactionModel
    {
        public WorkCoin Coin { get; set; }

        public UserModel Buyer { get; set; }

        public UserModel Seller { get; set; }

        public float Amount { get; set; }

        public float Price { get; set; }

        public DateTime Date { get; set; }
    }
}
