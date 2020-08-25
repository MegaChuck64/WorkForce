using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkForce.Controllers
{
    public class FantasyController : Controller
    {

        public IActionResult Index(int year)
        {
            var res = "Season";
            if (year == 0)
            {
                res = "All Seasons";
            }
            else if (year < 2017)
            {
                res = "Data is unavailable for " + year;
            }
            else
            {
                res = "You chose " + year;
            }

            ViewBag.Season = res;

            return View();
        }



    }
}