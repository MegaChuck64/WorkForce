using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForce.Models
{
    public class OrderModel
    {
        public WorkCoin Coin { get; set; }
        public UserModel User { get; set; }
        public SaleType Type { get; set; }
        public float Amount { get; set; }
        public float Price { get; set; }


        public enum SaleType
        {
            Buy,
            Sell
        }
    }

}
