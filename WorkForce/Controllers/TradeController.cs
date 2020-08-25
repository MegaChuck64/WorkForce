using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkForce.Models;

namespace WorkForce.Controllers
{
    public class TradeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Order(OrderModel order)
        {


            order.User = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("User"));

            return Json(order);
        }
    }
}