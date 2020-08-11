using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkForce.Models;

namespace WorkForce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string connStr = "Server=DESKTOP-C4KM7DE;Database=WorkForce;Integrated Security=SSPI";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(UserModel user)
        {

            var response = new AsyncResponse
            {
                Failed = true,
                Message = string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) ? "Missing Data..." : "Started registering..." ,
                Result = user
            };

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return Json(response);
            }
            try
            {
                //see if username exists
                var sql =
                      "SELECT 1 " +
                      "FROM Users " +
                      $"WHERE UserName = '{user.Username}' ";

                using (var conn = new SqlConnection(connStr))
                using (var comm = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (var rdr = comm.ExecuteReader())
                    {
                        response.Failed = false;
                        while (rdr.Read())
                        {
                            response.Failed = true;
                            response.Message = "Username already taken...";
                            response.Result = null;
                        }
                    }

                    conn.Close();
                }


                if (!response.Failed)
                {

                    //sql = "SET @uid = 1 " +
                    //    "INSERT INTO Users (Username, Password) " +
                    //    "OUTPUT inserted.UserID INTO @uid " +
                    //    $"VALUES ('{user.Username}', '{user.Password}') " +
                    //    $"INSERT INTO Coins (UserID, OwnerID) " +
                    //    $"VALUES(@uid, @uid);";


                    sql = $"INSERT INTO Users (Username, Password) " +
                            $"VALUES ('{user.Username}', '{user.Password}');";

                    using (var conn = new SqlConnection(connStr))
                    using (var comm = new SqlCommand(sql, conn))
                    {
                        conn.Open();

                        var rowsAffected = comm.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            response.Failed = false;
                            response.Message = "Successfully registered...";
                            response.Result = user;
                        }
                        else
                        {
                            response.Failed = true;
                            response.Message = "Registration unsuccesful.";
                            response.Result = null;
                        }

                        conn.Close();
                    }
                }


            }

            catch (Exception e)
            {
                response.Failed = true;
                response.Message = e.Message;
                response.Result = "Exception thrown while attempting to register...";
            }


            return Json(response);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(UserModel user)
        {
            var response = new AsyncResponse
            {
                Failed = true,
                Message = "Started logging in...",
                Result = user
            };

            try
            {

                var sql =
                        "SELECT 1 " +
                        "FROM Users " +
                        $"WHERE UserName = '{user.Username}' " + // username cap insensitive        
                        $"AND Password = '{user.Password}' COLLATE Latin1_General_CS_AS"; // password cap sensitive  

                using var conn = new SqlConnection(connStr);
                using var comm = new SqlCommand(sql, conn);
                conn.Open();

                using (var rdr = comm.ExecuteReader())
                    while (rdr.Read())
                    {
                        response.Failed = false;
                        response.Message = "Successful login attempt...";
                        
                    }


                conn.Close();


                if (response.Failed)
                {
                    response.Message = "Invalid login attempt...";
                    response.Result = null;
                }
  
            }

            catch (Exception e)
            {
                response.Failed = true;
                response.Message = e.Message;
                response.Result = "Exception thrown while attempting to login...";
            }

            return Json(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
