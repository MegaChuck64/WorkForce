using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkForce.Models;

namespace WorkForce.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {            
            return View();
        }
    }
}